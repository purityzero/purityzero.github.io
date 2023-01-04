using UnityEngine;

public class TableEditor_Info : TableEditorDraw
{    
    Vector2 m_scrollbar = Vector2.zero;

    public TableEditor_Info(TableTool _tool) : base(_tool)
    {
        m_tableTool = _tool;
    }

    public override void Draw()
    {
        base.Draw();       

        GUILayout.Space(50);
        m_scrollbar = GUILayout.BeginScrollView(m_scrollbar, GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
        GUILayout.EndScrollView();
    }    
}

