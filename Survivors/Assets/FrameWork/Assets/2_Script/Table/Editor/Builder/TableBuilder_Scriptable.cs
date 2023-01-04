using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEditor;
using System.Reflection;
public class TableBuilder_Scriptable : TableBuilder
{
    public TableBuilder_Scriptable(string _path, string _className) : base(_path, _className)
    {
    }  

    public override void Build(TableFileReader _reader, string _directory)
    {
        Type _assetType = GetSystemType();
        string _assetPath = string.Format("Assets/Resources/Table/{0}.asset", m_className);
        UnityEngine.Object asset = AssetDatabase.LoadAssetAtPath(_assetPath, _assetType);
        if (asset == null)
        {
            try
            {
                asset = ScriptableObject.CreateInstance(_assetType.Name);
                AssetDatabase.CreateAsset(asset, _assetPath);
            }
            catch (System.Exception _e)
            {
                Debug.LogError(_e.ToString());
            }
        }

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
       
        EditorUtility.SetDirty(asset);
    }
}
