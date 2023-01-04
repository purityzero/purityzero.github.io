using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StageRecord : Record
{
    public string sMapPath;
    public List<int> iFirstPhaseMonster;
    public List<int> iSecondPhaseMonster;
    public List<int> iThridPhaseMonster;
    public List<int> iMonsterCount;
    public float finfinity;
    public int iEndTime;
}


public class StageTable : Table<StageRecord>
{
    public static StageTable Instance { get { return TableMgr.Instance.GetTable<StageTable>(); } }
}
