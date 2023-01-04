using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using System.Reflection;


public class TableBuilder
{
    protected string m_path;
    protected string m_className;
    protected Dictionary<string, List<string>> m_fileList = new Dictionary<string, List<string>>();
    public TableBuilder(string _path, string _className)
    {
        m_path = _path;
        m_className = _className;
    }

    public virtual void Build(TableFileReader _reader, string _directory)
    {

    }

    public virtual TableBuilder Add(string _fileName, string _sheetName)
    {
        _fileName = _fileName.ToLower();
        _sheetName = _sheetName.ToLower();

        if (m_fileList.ContainsKey(_fileName) == false)
        {
            m_fileList.Add(_fileName, new List<string>());
        }

        List<string> _list = m_fileList[_fileName];
        if (true == _list.Contains(_sheetName))
        {
            Debug.LogErrorFormat("have sheet : {0} : {1}", _fileName, _sheetName);
            return this;
        }

        _list.Add(_sheetName);

        return this;
    }

    public virtual void SetData(object asset, string _sheet, List<Dictionary<string, string>> _data)
    {
        System.Type _systemType = GetSystemType();
        var _classFields = _systemType.GetFields();

        for (int _fieldCnt = 0; _fieldCnt < _classFields.Length; ++_fieldCnt)
        {
            var _classField = _classFields[_fieldCnt];
            Type fieldType = _classField.FieldType;
            if (!fieldType.IsGenericType || (fieldType.GetGenericTypeDefinition() != typeof(List<>)))
                continue;

            Type[] _argument = fieldType.GetGenericArguments();
            if (_argument.Length >= 2)
            {
                Debug.LogError("_argument.Length >= 2");
                continue;
            }
            Type _recordType = _argument[0];
            var _recordFields = _recordType.GetFields();
            Type listType = typeof(List<>).MakeGenericType(_recordType);
            MethodInfo listAddMethod = listType.GetMethod("Add", new Type[] { _recordType });
            object list = Activator.CreateInstance(listType);

            for (int i = 0; i < _data.Count; ++i)
            {
                var entity = CreateEntityFromRow(_data[i], _recordType);
                listAddMethod.Invoke(list, new object[] { entity });
            }
            _classField.SetValue(asset, list);
        }
    }

    public bool GetFieldObject(ref object _recordValue, string _showClassName, FieldInfo _fieldInfo, string _sheetName, Dictionary<string, string> _data)
    {
        if (_fieldInfo.FieldType == typeof(int))
        {
            _recordValue = Get<int>(_showClassName, _data, _sheetName);
        }
        else if (_fieldInfo.FieldType == typeof(string))
        {
            _recordValue = Get<string>(_showClassName, _data, _sheetName);
        }
        else if (_fieldInfo.FieldType == typeof(float))
        {
            _recordValue = Get<float>(_showClassName, _data, _sheetName);
        }
        else if (_fieldInfo.FieldType == typeof(long))
        {
            _recordValue = Get<long>(_showClassName, _data, _sheetName);
        }
        else if (_fieldInfo.FieldType == typeof(bool))
        {
            _recordValue = Get<bool>(_showClassName, _data, _sheetName);
        }
        else if (_fieldInfo.FieldType == typeof(double))
        {
            _recordValue = Get<double>(_showClassName, _data, _sheetName);
        }
        else if (_fieldInfo.FieldType.IsEnum)
        {
            try
            {
                string _parser = Get<string>(_showClassName, _data, _sheetName);
                if (false == string.IsNullOrEmpty(_parser))
                {
                    _recordValue = System.Enum.Parse(_fieldInfo.FieldType, _parser);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("error : " + _fieldInfo.FieldType + ", sheetname : " + _sheetName + " : " + e.ToString() ); 
            }
        }
        else
        {
            return false;
        }
        return true;
    }

    public object GetFieldObjectList(string _showClassName, Type _type, string _sheetName, Dictionary<string, string> _data)
    {
        ArrayList _list = null;

        if (_type == typeof(int))
        {
            _list = new ArrayList();
            _list.AddRange(GetList<int>(_showClassName, _data, _sheetName));
        }
        else if (_type == typeof(string))
        {
            _list = new ArrayList();
            _list.AddRange(GetList<string>(_showClassName, _data, _sheetName));
        }
        else if (_type == typeof(float))
        {
            _list = new ArrayList();
            _list.AddRange(GetList<float>(_showClassName, _data, _sheetName));
        }
        else if (_type == typeof(long))
        {
            _list = new ArrayList();
            _list.AddRange(GetList<long>(_showClassName, _data, _sheetName));
        }
        else if (_type == typeof(bool))
        {
            _list = new ArrayList();
            _list.AddRange(GetList<bool>(_showClassName, _data, _sheetName));
        }
        else if (_type == typeof(double))
        {
            _list = new ArrayList();
            _list.AddRange(GetList<double>(_showClassName, _data, _sheetName));
        }

        return _list;
    }

    object GetClassFieldObject(string _showClassName, Type _classType, string _excelFieldName, Dictionary<string, string> _data)
    {
        FieldInfo[] _classFields = _classType.GetFields();
        object _recordValue = Activator.CreateInstance(_classType);
        for (int _classFieldIdx = 0; _classFieldIdx < _classFields.Length; ++_classFieldIdx)
        {
            var _classField = _classFields[_classFieldIdx];
            string _excelName = string.Format("{0}_{1}", _excelFieldName, _classField.Name);
            object _clasValue = null;
            if (_classField.FieldType.IsGenericType && (_classField.FieldType.GetGenericTypeDefinition() == typeof(List<>)))
            {
                Type[] _argument = _classField.FieldType.GetGenericArguments();
                Type _argumentType = _argument[0];
                _clasValue = GetFieldObjectList(_showClassName, _argumentType, _excelName, _data);
                if (_clasValue == null)
                {
                    Debug.LogError("null == _clasValue " + _showClassName + " : " + _classField.Name);
                }
                else
                {
                    _classField.SetValue(_recordValue, _clasValue);
                }
            }
            else
            {
                if (false == GetFieldObject(ref _clasValue, _showClassName, _classField, _excelName, _data))
                {
                    Debug.LogError("null == _clasValue " + _showClassName + " : " + _classField.Name);
                }
                else
                {
                    _classField.SetValue(_recordValue, _clasValue);
                }
            }
        }

        return _recordValue;
    }
    object GetClassListFieldObject(string _showClassName, Type _classType, string _excelClassName, Dictionary<string, string> _data)
    {
        FieldInfo[] _classFields = _classType.GetFields();
        object _recordValue = Activator.CreateInstance(_classType);
        for (int _classFieldIdx = 0; _classFieldIdx < _classFields.Length; ++_classFieldIdx)
        {
            var _classField = _classFields[_classFieldIdx];
            string _excelName = string.Format("{0}_{1}", _excelClassName, _classField.Name).ToLower();

            if (_data.ContainsKey(_excelName) == false)
            {
                return null;
            }

            object _clasValue = null;
            if (false == GetFieldObject(ref _clasValue, _showClassName, _classField, _excelName, _data))
            {
                return null;
            }

            if (_classFieldIdx == 0 && _clasValue == null)
            {
                return null;
            }

            _classField.SetValue(_recordValue, _clasValue);
        }

        return _recordValue;
    }
    object CreateEntityFromRow(Dictionary<string, string> _data, Type entityType)
    {
        var entity = Activator.CreateInstance(entityType);
        var _recordFields = entityType.GetFields();
        string _showClassName = entityType.ToString();

        for (int _recordFieldCnt = 0; _recordFieldCnt < _recordFields.Length; ++_recordFieldCnt)
        {
            var _recordField = _recordFields[_recordFieldCnt];

            object _recordValue = null;
            if (false == GetFieldObject(ref _recordValue, _showClassName, _recordField, _recordField.Name, _data))
            {
                Type _classType = _recordField.FieldType;

                if (_classType.IsGenericType && (_classType.GetGenericTypeDefinition() == typeof(List<>)))
                {                   
                    Type[] _argument = _classType.GetGenericArguments();
                    if (_argument.Length >= 2)
                    {
                        Debug.LogError("_argument.Length >= 2");
                    }
                    else
                    {
                        Type _argumentType = _argument[0];
                        var _argumentFields = _argumentType.GetFields();
                        Type listType = typeof(List<>).MakeGenericType(_argumentType);
                        MethodInfo listAddMethod = listType.GetMethod("Add", new Type[] { _argumentType });
                        _recordValue = Activator.CreateInstance(listType);

                       
                        object _argumentObject = GetFieldObjectList(_showClassName, _argumentType, _recordField.Name, _data);
                        if( null != _argumentObject)
                        {
                            ArrayList _temp = (ArrayList)_argumentObject;
                            for( int i=0; i< _temp.Count; ++i )
                            {
                                listAddMethod.Invoke(_recordValue, new object[] { _temp[i] });
                            }
                        }
                        else
                        {
                            int _index = 1;
                            while (true)
                            {
                                string _excelName = string.Format("{0}_{1}", _recordField.Name, _index);

                                //_argumentObject = GetFieldObjectList(_showClassName, _argumentType, _excelName, _data);
                                //if (null != _argumentObject)                                    
                                _argumentObject = GetClassListFieldObject(_showClassName, _argumentType, _excelName, _data);
                                if (null == _argumentObject)
                                    break;
                                listAddMethod.Invoke(_recordValue, new object[] { _argumentObject });
                                ++_index;
                            }
                        }
                    }
                }
                else
                {
                    _recordValue = GetClassFieldObject(_showClassName, _classType, _recordField.Name, _data);
                }
            }
            _recordField.SetValue(entity, _recordValue);
        }
        return entity;
    }

    public virtual System.Type GetSystemType()
    {
        return GetTypeFromAssemblies(m_className);
    }

    public virtual void GetFileList(TableFileReader _reader)
    {
        var _var = m_fileList.GetEnumerator();
        while (_var.MoveNext())
        {
            for (int i = 0; i < _var.Current.Value.Count; ++i)
            {
                _reader.AddFile(_var.Current.Key, _var.Current.Value[i]);
            }
        }
    }

    public T Get<T>(string _class, string _key, string _value, T def = default(T))
    {
        if (string.IsNullOrEmpty(_value) == true)
        {
            return def;
        }

        try
        {
            if (true == typeof(T).IsEnum)
            {
                return (T)Enum.Parse(typeof(T), _value, true);
            }
            else if (typeof(int) == typeof(T))
            {
                return (T)((object)int.Parse(_value));
            }
            else if (typeof(long) == typeof(T))
            {
                return (T)((object)long.Parse(_value));
            }
            else if (typeof(float) == typeof(T))
            {
                return (T)((object)float.Parse(_value));
            }
            else if (typeof(bool) == typeof(T))
            {
                return (T)((object)bool.Parse(_value));
            }
            else if (typeof(double) == typeof(T))
            {
                return (T)((object)double.Parse(_value));
            }
            else if (typeof(string) == typeof(T))
            {
                return (T)((object)_value);
            }
            else
            {
                Debug.LogErrorFormat("Get<{0}> - [{1}] : {2}", typeof(T).ToString(), _key, _value);
            }
        }
        catch
        {
            Debug.LogErrorFormat("Get catch <{0}> - [{1}] : {2}", typeof(T).ToString(), _key, _value);
        }

        return def;
    }

    public T Get<T>(string _class, Dictionary<string, string> _data, string _key, T def = default(T))
    {
        _key = _key.ToLower();

        if (false == _data.ContainsKey(_key))
        {
            Debug.LogErrorFormat(_class + "::Get<{0}>[not find] : {1}", typeof(T).ToString(), _key);
            return def;
        }
        return Get<T>(_class, _key, _data[_key], def);
    }

    public List<T> GetList<T>(string _class, Dictionary<string, string> _data, string _key)
    {
        List<T> _list = new List<T>();

        string _value = Get<string>(_class, _data, _key);
        if (string.IsNullOrEmpty(_value) == true)
            return _list;

        int _idx_firset = Mathf.Clamp(_value.IndexOf('[') + 1, 0, _value.Length - 1);
        int _idx_end = Mathf.Clamp(_value.IndexOf(']') - 1, 0, _value.Length - 1);
        _value = _value.Substring(_idx_firset, _idx_end);
        _value = _value.Replace(" ", "");
        string[] _valueList = _value.Split(',');

        for (int i = 0; i < _valueList.Length; i++)
        {
            if (true == string.IsNullOrEmpty(_valueList[i]))
            {
                continue;
            }

            _list.Add(Get<T>(_class, _key, _valueList[i]));
        }
        return _list;
    }

    public System.Type GetTypeFromAssemblies(string TypeName)
    {
        var type = System.Type.GetType(TypeName);
        if (type != null)
            return type;

        var currentAssembly = System.Reflection.Assembly.GetExecutingAssembly();
        var referencedAssemblies = currentAssembly.GetReferencedAssemblies();
        foreach (var assemblyName in referencedAssemblies)
        {
            var assembly = System.Reflection.Assembly.Load(assemblyName);
            if (assembly != null)
            {
                type = assembly.GetType(TypeName);
                if (type != null)
                    return type;
            }
        }
        return null;
    }
}