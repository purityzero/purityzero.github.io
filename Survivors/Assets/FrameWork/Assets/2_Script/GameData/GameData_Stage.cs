using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillData
{
    public eSkillDefine eSkillType;
    public int lv;

    public SkillData(eSkillDefine _type, int _lv)
    {
        eSkillType = _type;
        lv = _lv;
    }

    public void LvUp(int _lv = 1)
    {
        lv += _lv;
    }
}


public class GameData_Stage : GameData
{
    public static GameData_Stage Instance { get { return GameDataMgr.Instance.GetData<GameData_Stage>(); } }
    [SerializeField]
    private int m_stageLv;
    [SerializeField]
    private long m_money;
    [SerializeField]
    private long m_exp;
    [SerializeField]
    List<SkillData> m_skillList = new List<SkillData>();
    [SerializeField]
    private int m_lv;
    [SerializeField]
    private int m_kill;
    [SerializeField]
    private int m_time;
    [SerializeField]
    private eInGameState m_currentState;
    [SerializeField]
    private int m_hp;
    private Subject m_subject = new Subject();
    

    public int stageLv => m_stageLv;
    public long money => m_money;
    public long exp => m_exp;
    public int time => m_time;
    public int lv => m_lv;
    public int kill => m_kill;
    public int hp => m_hp;
    public eInGameState CurrentState => m_currentState;
    public List<SkillData> skillList => m_skillList;

    public GameData_Stage()
    {
        m_subject.Attach(InGameMgr.Instance.GetAsset<ExpAsset>());
        m_subject.Attach(InGameMgr.Instance.GetAsset<MoneyAsset>());
        m_subject.Attach(InGameMgr.Instance.GetAsset<KillAsset>());

        m_stageLv = GameData_User.Instance.selectStage;
        m_lv = 1;
        m_time = 0;

        m_subject.SetNotify();
    }

    public void Add(eAssetType _assetType, int _cnt)
    {
        switch (_assetType)
        {
            case eAssetType.Exp:
                AddExp(_cnt);
                return;
            case eAssetType.Kill:
                AddKill(_cnt);
                return;
            case eAssetType.Money:
                AddMoney(_cnt);
                return;
        }

    }

    public void AddSkill(eSkillDefine _type, int _lv)
    {
        SkillData _data = null;
        if (m_skillList.Exists(_data => _data.eSkillType == _type) == false)
        {
            _data = new SkillData(_type, _lv);
            m_skillList.Add(_data);
        }
        else
        {
            _data = m_skillList.Find(_data => _data.eSkillType == _type);
            _data.lv = _lv;
        }

        SetNeedSave(true);

    }

    public void SetState(eInGameState _state)
    {
        m_currentState = _state;
        SetNeedSave(true);
    }

    public void SetHp(int _hp)
    {
        m_hp = _hp;
        SetNeedSave(true);
    }

    public void SetTime(int _time)
    {
        m_time = _time;
        SetNeedSave(true);
    }

    private void AddExp(long _exp)
    {
        m_exp += _exp;
        if (ExpTable.Instance.Get(m_lv).LNextExp <= m_exp)
        {
            m_exp -= ExpTable.Instance.Get(m_lv).LNextExp;
            AddLv();
            Debug.Log("Level Up");
        }
        SetNeedSave(true);
    }

    private void AddKill(int _kill)
    {
        m_kill += _kill;
        m_subject.SetNotify();
        SetNeedSave(true);
    }

    private void AddLv(int _lv = 1)
    {
        m_lv += _lv;
        InGameMgr.Instance.SetNextState(eInGameFsmMsg.ChangeNextState, eInGameState.Levelup);
        SetNeedSave(true);
    }

    private void AddMoney(long _money)
    {
        m_money += _money;
        SetNeedSave(true);
    }

    public override void Remove()
    {
        m_subject.Detach(InGameMgr.Instance.GetAsset<ExpAsset>());
        m_subject.Detach(InGameMgr.Instance.GetAsset<MoneyAsset>());
        m_subject.Detach(InGameMgr.Instance.GetAsset<KillAsset>());
        base.Remove();
    }

    public void SetInGameState(eInGameState _state)
    {
        m_currentState = _state;
        SetNeedSave(true);
    }

    public override void Update()
    {
        base.Update();
        m_subject.Update();
    }

}
