using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum eTEAM
{
    PLAYER,
    MONSTER,
}

public enum eSTAT_TYPE
{
    MAX_HP,
    MOVE_SPD,
    ATK,
    ATK_DELAY_TIME,
    ATK_DIS,
    COST,
    REWARD_BATTLE_COIN,
    UPGRADE_COST,
}

public class StatData
{
    public eSTAT_TYPE statType;
    public float statValue;
    public float statAddValue;
    
    public StatData(eSTAT_TYPE _statType, float _statValue, float _statAddValue)
    {
        statType = _statType;
        statValue = _statValue;
        statAddValue = _statAddValue;
    }
}

[System.Serializable]
public class ActorRecord : Record
{
    public string path;
    public float moveSpd;
    public float atkDelayTime;
    public int atk;
    public int maxHp;
    List<StatData> m_statList = new List<StatData>();

    public float GetStatValue( eSTAT_TYPE _statType, int _lv )
    {
        StatData _find = m_statList.Find(item => item.statType == _statType);
        if (null == _find)
            return 0f;

        return _find.statValue + _find.statAddValue * (_lv - 1);
    }

    public void ResetData()
    {
        m_statList.Add(new StatData(eSTAT_TYPE.MOVE_SPD, moveSpd, 0f));
        m_statList.Add(new StatData(eSTAT_TYPE.ATK_DELAY_TIME, atkDelayTime, 0f));
        m_statList.Add(new StatData(eSTAT_TYPE.ATK, atk, 0));
        m_statList.Add(new StatData(eSTAT_TYPE.MAX_HP, maxHp, maxHp));
    }
}

[System.Serializable]
public class ActorTable : Table<ActorRecord> 
{
    static public ActorTable Instance { get { return TableMgr.Instance.GetTable<ActorTable>(); } }

    public override void Load(string _path)
    {
        base.Load(_path);

        for( int i=0;i<list.Count; ++i )
        {
            list[i].ResetData();
        }
    }

}
