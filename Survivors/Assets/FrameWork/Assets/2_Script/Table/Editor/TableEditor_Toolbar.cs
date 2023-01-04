using UnityEngine;
using UnityEditor;

/**
 * TableEditor_Toolbar
 * 
 */
public class TableEditor_Toolbar : TableEditorDraw
{
    public bool isOepnPath = false;

    public TableEditor_Toolbar(TableTool _tool) : base(_tool)
    {
    }

    public override void Draw()
    {
        base.Draw();
        GUILayout.BeginHorizontal(EditorStyles.toolbar, GUILayout.ExpandWidth(true));
        GUILayout.FlexibleSpace();

        if (GUILayout.Button("ImportFile", EditorStyles.toolbarButton, GUILayout.Width(100)))
        {
            if (System.IO.Directory.Exists(TableTool.import_file_directory) == false)
            {
                System.IO.Directory.CreateDirectory(TableTool.import_file_directory);
            }
            Application.OpenURL(TableTool.import_file_directory);
        }

        if (GUILayout.Button("ExportFile", EditorStyles.toolbarButton, GUILayout.Width(100)))
        {
            if (System.IO.Directory.Exists(TableTool.export_file_directory) == false)
            {
                System.IO.Directory.CreateDirectory(TableTool.export_file_directory);
            }           
            Application.OpenURL(TableTool.export_file_directory);
        }
            
        if ( GUILayout.Button("Bake", EditorStyles.toolbarButton, GUILayout.Width(100)) )
        {
            Bake();
        }

        GUILayout.EndHorizontal();
    }
     
    public void Bake()
    {
        m_tableTool.Bake();
        AssetDatabase.Refresh();
    }
}

