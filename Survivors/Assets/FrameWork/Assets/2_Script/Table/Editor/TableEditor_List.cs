using UnityEngine;
using UnityEditor;

public class TableEditor_List : TableEditorDraw
{
    protected Vector2 m_scrollbar = Vector2.zero;
    

    public TableEditor_List(TableTool _tool) : base(_tool)
    {     
    }  

    public override void Draw()
    {
        base.Draw();
        m_scrollbar = GUILayout.BeginScrollView(m_scrollbar, GUILayout.ExpandHeight(true), GUILayout.Width(200));
        //var _var = TableMgr.Instance.getTableList.GetEnumerator();
        //while(_var.MoveNext())
        //{
        //    GUILayout.Label(_var.Current.Value.ToString());
        //}
        GUILayout.EndScrollView();
    }
}
