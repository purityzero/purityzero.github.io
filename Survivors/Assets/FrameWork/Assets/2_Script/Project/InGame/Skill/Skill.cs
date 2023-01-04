using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum eSkillState
{
    IDLE,
    ATTACK,
    CREATE,

}

public abstract class Skill : MonoBase
{
    public Image SkillImage;
    protected SkillRecord m_skillRecord;
    public SkillRecord skillRecord { get { return m_skillRecord; } }
    protected FsmClass<eSkillState> m_Fsm;

    protected List<Monster> m_RangeMonsterList = new List<Monster>();
    public List<Monster> RangeMonsterList => m_RangeMonsterList;

    public virtual void Open(SkillRecord _skillRecord)
    {
        base.Open();
        m_Fsm = new FsmClass<eSkillState>();
        m_skillRecord = _skillRecord;
    }

    protected virtual void Awake()
    {
    }

    public virtual void ReSetRecord(SkillRecord _record)
    {
        m_skillRecord = _record;
    }


    public virtual void SetState(eSkillState _state)
    {
        m_Fsm.SetState(_state);
    }

    public override void UpdateLogic()
    {
        m_Fsm?.Update();
    }
}

/*
public class Skill : UIDialog
{
    private SkillData m_data;
    public SkillData getData { get { return m_data; } }

    private  FsmClass<eSkillState> m_fsm;

    public Image SkillImage;

    private void Awake()
    {
        m_fsm = new FsmClass<eSkillState>();
        m_fsm.AddFsm(new SkillState_IDLE(this));
        m_fsm.AddFsm(new SkillState_Attack(this));
    }

    public void Open(SkillData _data)
    {
        m_data = _data;
        SkillImage.sprite = Resources.Load<Sprite>(_data.SkillImage);
        m_fsm.SetState(eSkillState.IDLE);

    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        m_fsm.Update();


    }

    public void SetState(eSkillState _state)
    {
        m_fsm.SetState(_state);
    }
}

public class SkillState_IDLE : FsmState<eSkillState>
{
    protected Skill m_skill;
    float m_timer;
    public SkillState_IDLE(Skill _skill) : base(eSkillState.IDLE)
    {
        m_skill = _skill;
    }

    public override void Enter(FsmMsg _msg)
    {
        base.Enter(_msg);
        m_timer = 0;
    }

    public void NextState()
    {
        m_skill.SetState(eSkillState.ATTACK);
    }

    public override void Update()
    {
        base.Update();
        m_timer += Time.deltaTime;
        if (m_skill.getData.TriggerTime <= m_timer)
        {
            NextState();
        }
    }
}

public class SkillState_Attack : FsmState<eSkillState>
{
    protected Skill m_skill;

    public SkillState_Attack(Skill _skill) : base(eSkillState.ATTACK)
    {
        m_skill = _skill;
    }

    private void NextState()
    {
        m_skill.SetState(eSkillState.IDLE);
    }

    public override void Enter(FsmMsg _msg)
    {
        base.Enter(_msg);
        var monsterList = InGameMgr.Instance.GetDamageMonsterList(m_skill.transform, m_skill.getData.Range * 0.2f);
        for (int i = 0; i < monsterList.Count; ++i)
        {
            monsterList[i].Damage(m_skill.getData.Damage);
        }

    }

    public override void Update()
    {
        base.Update();
        NextState();
    }
}
*/
