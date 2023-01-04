using UnityEditor;
using UnityEngine;
using System.Collections.Generic;


/** 
 * TableTool
 * 
 */
public class TableTool : EditorWindow
{
    #region - static
    public static string import_file_directory
    {
        get
        {
            return string.Format("{0}/../Table/", Application.dataPath);
        }
    }
    public static string export_file_directory
    {
        get
        {
            return string.Format("{0}/Resources/Table", Application.dataPath);
        }
    }
    #endregion

    TableEditor_List m_list;
    TableEditor_Info m_info;
    TableEditor_Toolbar m_toolbar;

    List<TableBuilder> m_builderList = new List<TableBuilder>();
    TableFileReader m_reader = new TableFileReader_excel();


    public TableEditor_Toolbar getToolbar { get { return m_toolbar; } }
    public TableEditor_List getList { get { return m_list; } }
    public TableEditor_Info getInfo { get { return m_info; } }    


    [MenuItem("Tools/TableTool")]
	static void Init () 
    {
        EditorWindow.GetWindow<TableTool>(false, "TableTool");
    }
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(TableTool));
    }  
    private void OnEnable()
    {
        if (null == m_list)
            m_list = new TableEditor_List(this);
        if (null == m_info)
            m_info = new TableEditor_Info(this);
        if (null == m_toolbar)
            m_toolbar = new TableEditor_Toolbar(this);

        m_toolbar.OnEnable();
        m_list.OnEnable();
        m_info.OnEnable();      
    }
    private void OnDisable()
    {
        m_toolbar?.OnDisable();
        m_list?.OnDisable();
        m_info?.OnDisable();
    }

    void OnGUI ()
    {
        m_toolbar.Draw();
        EditorGUILayout.BeginHorizontal();
        m_list.Draw();
        m_info.Draw();
        EditorGUILayout.EndHorizontal();
    }

    public TableBuilder Add(TableBuilder _builder)
    {
        m_builderList.Add(_builder);
        return _builder;
    }

    public void Bake()
    {
        string _writePath = TableTool.export_file_directory;
        Create();
        m_reader.fileDirectory = TableTool.import_file_directory;
        m_reader.Clear();     
        for (int i = 0; i < m_builderList.Count; ++i)
        {
            m_builderList[i].GetFileList(m_reader);
        }

        m_reader.LoadFile();
        for( int i=0; i<m_builderList.Count; ++i )
        {
            m_builderList[i].Build( m_reader, _writePath);
        }


    }

    public void Create()
    {
        m_builderList.Clear();
        Dictionary<string, Dictionary<string, List<string>>> _list = TableMgr.Instance.CreateFactory().CreateEditorTableList();
        var _varClass = _list.GetEnumerator();
        while(_varClass.MoveNext() )
        {
            var _varExcelFile = _varClass.Current.Value.GetEnumerator();
            while (_varExcelFile.MoveNext())
            {
                TableBuilder _builder = AddTable(_varClass.Current.Key);
                for ( int i=0; i<_varExcelFile.Current.Value.Count; ++ i)
                {
                    _builder.Add(_varExcelFile.Current.Key, _varExcelFile.Current.Value[i]);
                }
            }
        }
    }    

   TableBuilder AddTable(string _className )
    {
        TableBuilder _builder = new TableBuilder_Bytes(_className, _className);
        m_builderList.Add(_builder);
        return _builder;
    }
}
