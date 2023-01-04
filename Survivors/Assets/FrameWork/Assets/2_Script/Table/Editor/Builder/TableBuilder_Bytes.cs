using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEditor;
using System.Reflection;
public class TableBuilder_Bytes : TableBuilder
{
    public TableBuilder_Bytes(string _path, string _className) : base(_path, _className)
    {       
    } 

    public override void Build(TableFileReader _reader, string _directory)
    {
        System.Type _systemType = GetSystemType();
        object asset = Activator.CreateInstance(_systemType);

        var _var = m_fileList.GetEnumerator();
        while (_var.MoveNext())
        {
            for (int i = 0; i < _var.Current.Value.Count; ++i)
            {
                string _sheetName = _var.Current.Value[i];
                List<Dictionary<string, string>> _data = _reader.GetData(_var.Current.Key, _sheetName);
                if (null == _data)
                    continue;

                SetData(asset, _sheetName, _data);
            }
        }

        string _saveDirectory = System.IO.Path.GetDirectoryName(_directory);
        if (System.IO.Directory.Exists(_saveDirectory) == false)
        {
            System.IO.Directory.CreateDirectory(_saveDirectory);
        }

        string _path = string.Format("{0}/Table/{1}.bytes", _saveDirectory, m_className);
        MethodInfo _method = _systemType.GetMethod("Save");
        _method.Invoke(asset, new object[] { _path });
    }
}
