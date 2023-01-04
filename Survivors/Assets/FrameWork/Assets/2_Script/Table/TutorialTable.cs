using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum eTUTORIAL_OPEN_TYPE
{
    NONE = 0,
    INTRO = 1,
    LOBBY = 2,
    BATTLE = 3,
    WORLD_MAP = 4,
}


[System.Serializable]
public class TutorialRecord : Record
{
    public bool use;
    public eTUTORIAL_OPEN_TYPE openType;
    public int cutsceneId;
}


public class TutorialTable : Table<TutorialRecord>
{
    public static TutorialTable Instance { get { return TableMgr.Instance.GetTable<TutorialTable>(); } }
}
