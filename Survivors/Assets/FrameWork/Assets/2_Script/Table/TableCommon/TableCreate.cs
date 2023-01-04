using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * TableCreate
 * 
 */
public abstract class TableCreate
{
    /**
     * Create
     * 게임에서 사용할 테이블리스트를 리턴한다.
     */
    public abstract Dictionary<System.Type, ITable> CreateTableList();

    /**
     * CreateEditorTableList
     * Dictionary<클래스 이름, Dictionary<엑셀파일이름,List<시트이름>>>
     * 엑셀데이터 -> 게임데이터 변환 리스트
     */
    public abstract Dictionary<string, Dictionary<string,List<string>>> CreateEditorTableList();

    /**
    * AddEditorTable
    * 
    */
    protected virtual Dictionary<string, Dictionary<string, List<string>>> AddEditorTable(Dictionary<string, Dictionary<string, List<string>>> _list, string _className, string _excelFileName, string _sheetName )
    {
        Dictionary<string, List<string>> _excelFileList = null;
        if (_list.ContainsKey(_className) == false )
        {
            _list.Add(_className, _excelFileList = new Dictionary<string, List<string>>());         
        }
        else
        {
            _excelFileList = _list[_className];
        }

        List<string> _sheetList = null;
        if (_excelFileList.ContainsKey(_excelFileName) == false)
        {
            _excelFileList.Add(_excelFileName, _sheetList = new List<string>());            
        }
        else
        {
            _sheetList = _excelFileList[_excelFileName];
        }

        if(_sheetList.Contains(_sheetName) == true )
        {
            Debug.LogError("have sheet : " + _className + ", " + _excelFileName + ", " + _sheetName);
            return _list;
        }

        _sheetList.Add(_sheetName);
        return _list;
    }

    /**
     * AddTable
     *  
     */
    protected virtual void AddTable<T>(Dictionary<System.Type, ITable> _list) where T : ITable, new()
    {
        System.Type _type = typeof(T);
        if( _list.ContainsKey(_type) == true )
        {
            Debug.LogError("have : " + _type.ToString());
            return;
        }

        _list.Add(_type, new T());
    }
}

