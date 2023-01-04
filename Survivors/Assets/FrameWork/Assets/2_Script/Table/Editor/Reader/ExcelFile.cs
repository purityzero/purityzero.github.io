using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
public class ExcelFile
{
    private string m_fileName;
    private List<ExcelSheet> sheetList = new List<ExcelSheet>();

    public ExcelFile(string _filename)
    {
        m_fileName = _filename.ToLower();
    }

    public List<Dictionary<string, string>> GetData(string _sheetName)
    {
        ExcelSheet _sheet = GetSheet(_sheetName);
        if (null == _sheet)
        {
            Debug.LogError("GetData() no sheet " + m_fileName + ", " + _sheetName);
            return null;
        }

        return _sheet.getData;
    }

    public Dictionary<string, string> GetTypeData(string _sheetName)
    {
        ExcelSheet _sheet = GetSheet(_sheetName);
        if (null == _sheet)
        {
            Debug.LogError("GetData() no sheet " + m_fileName + ", " + _sheetName);
            return null;
        }

        return _sheet.getTypeData;
    }

    public ExcelSheet GetSheet(string _sheetName)
    {
        _sheetName = _sheetName.ToLower();
        return sheetList.Find(item => item.sheetName == _sheetName);
    }

    public void Add(string _sheetname)
    {
        if (GetSheet(_sheetname) != null)
        {
            //Debug.LogErrorFormat("have excel [ {0} : {1}]" + m_fileName, _sheetname);
            return;
        }

        sheetList.Add(new ExcelSheet(m_fileName, _sheetname));
    }

    public void Load(string _directory, int _rowIdx_name, int _rowIdx_data)
    {
        string _path = string.Format("{0}/{1}", _directory, m_fileName);
        using (FileStream stream = File.Open(_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            IWorkbook book = null;
            if (Path.GetExtension(_path) == ".xls")
            {
                book = new HSSFWorkbook(stream);
            }
            else
            {
                book = new XSSFWorkbook(stream);
            }

            for (int i = 0; i < book.NumberOfSheets; ++i)
            {
                ISheet _sheet = book.GetSheetAt(i);
                string _sheetName = _sheet.SheetName.ToLower();
                ExcelSheet _excelSheet = sheetList.Find(item => item.sheetName == _sheetName);
                if (null == _excelSheet)
                    continue;
                _excelSheet.Load(_sheet, _rowIdx_name, _rowIdx_data);
            }
        }
    }
}