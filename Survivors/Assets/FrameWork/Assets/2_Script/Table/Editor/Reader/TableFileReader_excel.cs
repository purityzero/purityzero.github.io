using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableFileReader_excel : TableFileReader
{
    Dictionary<string, ExcelFile> m_fileList = new Dictionary<string, ExcelFile>();

    public override void Clear()
    {
        m_fileList.Clear();
    }
    public override void AddFile(string _fileName, string _sheetName)
    {
        _fileName = _fileName.ToLower();
        _sheetName = _sheetName.ToLower();

        if (m_fileList.ContainsKey(_fileName) == false )
        {
            m_fileList.Add(_fileName, new ExcelFile(_fileName));
        }
     
        m_fileList[_fileName].Add(_sheetName);
    }

    public override void LoadFile()
    {
        var _var = m_fileList.GetEnumerator();
        while(_var.MoveNext() )
        {
            _var.Current.Value.Load(fileDirectory, rowIdx_name, rowIdx_data );
        }
    }

    public override List<Dictionary<string, string>> GetData( string _filename, string _sheetName )
    {
        _filename = _filename.ToLower();
        if (m_fileList.ContainsKey(_filename) == false)
            return null;

        return m_fileList[_filename].GetData(_sheetName);
    }

    public override Dictionary<string, string> GetTypeData(string _fileName, string _sheetName)
    {
        _fileName = _fileName.ToLower();
        if (m_fileList.ContainsKey(_fileName) == false)
            return null;

        return m_fileList[_fileName].GetTypeData(_sheetName);
    }
}
