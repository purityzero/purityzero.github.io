using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NPOI.SS.UserModel;


public class ExcelSheet
{
    protected string m_fileName;
    protected string m_sheetName;
    protected List<Dictionary<string, string>> m_data;
    protected Dictionary<string, string> m_typeData;

    public string fileName { get { return m_fileName; } }
    public string sheetName { get { return m_sheetName; } }
    public List<Dictionary<string, string>> getData { get { return m_data; } }
    public Dictionary<string, string> getTypeData { get { return m_typeData; } }

    public ExcelSheet(string _fileName, string _sheetName)
    {
        m_fileName = _fileName;
        m_sheetName = _sheetName;
    }

    private Dictionary<string, string> Parser(IRow _nameRow, IRow _dataRow)
    {
        Dictionary<string, string> _parserData = new Dictionary<string, string>();

        for (int i = 0; i < _nameRow.LastCellNum; ++i)
        {
            ICell _nameCell = _nameRow.GetCell(i);
            if (null == _nameCell)
                continue;

            string _key = _nameCell.StringCellValue.ToLower();
            if (string.IsNullOrEmpty(_key) == true)
                continue;

            ICell _dataCell = _dataRow.GetCell(i);
            if (_dataCell == null && i == 0)
                return null;
            if (null == _dataCell)
            {
                _parserData.Add(_key, "");
                continue;
            }

            CellType _cellType = _dataCell.CellType;
            if (_cellType == CellType.Formula)
            {
                _cellType = _dataCell.CachedFormulaResultType;
            }

            switch (_cellType)
            {
                case CellType.Boolean:
                    _parserData.Add(_key, _dataCell.BooleanCellValue.ToString());
                    break;
                case CellType.Numeric:
                    _parserData.Add(_key, _dataCell.NumericCellValue.ToString());
                    break;
                case CellType.String:
                    _parserData.Add(_key, _dataCell.StringCellValue.ToString());
                    break;
                case CellType.Blank:
                    _parserData.Add(_key, "");
                    break;
                default:
                    Debug.LogErrorFormat("Excel File Load Error {0}, {1} : {2}, Key : {3}, idx : {4}", fileName, sheetName, _cellType, _key, _dataCell.RowIndex);
                    break;
            }
        }
        return _parserData;
    }

    private Dictionary<string, string> GetTypeData(IRow _nameRow, IRow _typeRow)
    {
        Dictionary<string, string> _dataList = new Dictionary<string, string>();

        for (int i = 0; i < _nameRow.LastCellNum; ++i)
        {
            ICell _nameCell = _nameRow.GetCell(i);
            if (null == _nameCell)
                continue;

            string _key = _nameCell.StringCellValue;
            if (string.IsNullOrEmpty(_key) == true)
                continue;

            ICell _dataCell = _typeRow.GetCell(i);
            if (null == _dataCell)
            {
                _dataList.Add(_key, "");
                continue;
            }

            _dataList.Add(_key, _dataCell.StringCellValue);
        }
        return _dataList;
    }

    public bool Load(ISheet _sheet, int _rowIdx_name, int _rowIdx_data)
    {
        m_data = new List<Dictionary<string, string>>();

        IRow _nameRow = _sheet.GetRow(_rowIdx_name);
        if (_nameRow == null)
        {
            Debug.LogError("cont find title : " + _rowIdx_name);
            return false;
        }

        var _var = _sheet.GetRowEnumerator();
        while (_var.MoveNext())
        {
            IRow _dataRow = (IRow)_var.Current;
            if (_dataRow.RowNum < _rowIdx_data)
                continue;

            Dictionary<string, string> _parserData = Parser(_nameRow, _dataRow);
            if (null == _parserData)
                continue;

            m_data.Add(_parserData);
        }
        return true;
    }
}