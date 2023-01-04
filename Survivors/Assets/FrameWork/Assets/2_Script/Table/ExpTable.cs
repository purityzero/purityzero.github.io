using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class ExpRecord : Record
{
    public long LNextExp;
}


public class ExpTable : Table<ExpRecord>
{
    public static ExpTable Instance { get { return TableMgr.Instance.GetTable<ExpTable>(); } }

    public int GetMaxLv()
    {
        return list.Max(_record => _record.id);
    }
}
