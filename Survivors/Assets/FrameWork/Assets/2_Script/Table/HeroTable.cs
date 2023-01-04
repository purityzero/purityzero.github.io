using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum eLockType
{
    None,
    Money,
    Diamond,
}

[System.Serializable]
public class HeroRecord : Record
{
    public List<string> SImagePath;
    public int Iatk;
    public int Ihp;
    public eLockType eLockType;
    public float FSpeed;
    public long lLockCount;
    public bool bIsLock;
}

public class HeroTable : Table<HeroRecord>
{
    public static HeroTable Instance { get { return TableMgr.Instance.GetTable<HeroTable>(); } }

}
