using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterRecord : Record
{
    public string spath;
    public int IAtk;
    public float fspeed;
    public int IHp;
    public int IMoney;
    public List<int> DropExp = new List<int>();
    public List<string> sImagePath;
}


public class MonsterTable : Table<MonsterRecord>
{
    public static MonsterTable Instance { get { return TableMgr.Instance.GetTable<MonsterTable>(); } }
}
