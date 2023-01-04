using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableMgr : ClassSingletonManager<TableMgr>
{
    private Dictionary<System.Type, ITable> m_tableList;

    public TableCreate CreateFactory()
    {
        return new TableCreate_Game();
    }

    public void Load()
    {     
        m_tableList = CreateFactory().CreateTableList();
        var _var = m_tableList.GetEnumerator();
        while(_var.MoveNext())
        {
            _var.Current.Value.Load(string.Format("Table/{0}", _var.Current.Key.Name));
        }
    }

    public T GetTable<T>() where T : class, ITable
    {
        System.Type _type = typeof(T);

        ITable _find = null;
        if(m_tableList.TryGetValue(_type, out _find) == false )
        {
            Debug.LogError(this.ToString() + "::GetTable() : " + _type.ToString());
            return null;
        }

        return _find as T;
    }
  
   
}
