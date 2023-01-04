using UnityEngine;

public class SkillState : FsmState<eSkillState>
{
    protected Skill m_skill;

    public SkillState(Skill _skill, eSkillState _state) : base (_state)
    {
        m_skill = _skill;
        m_stateType = _state;
    }

}

public class SkillState_Attack : SkillState
{

    public SkillState_Attack(Skill _skill) : base (_skill, eSkillState.ATTACK)
    {
    }

    private void NextState()
    {
        m_skill.SetState(eSkillState.IDLE);
    }

    public override void Enter(FsmMsg _msg)
    {
        base.Enter(_msg);
        for (int i = 0; i < m_skill.RangeMonsterList.Count; ++i)
        {
            m_skill.RangeMonsterList[i].Damage(m_skill.skillRecord.LDamage);
        }

    }

    public override void Update()
    {
        base.Update();
        NextState();
    }
}

public class SkillState_CREATE : SkillState
{ 
    string m_path;
    float m_speed;
    float m_lifeTime;
    public SkillState_CREATE(Skill _skill, string _path, float _speed, float _lifeTime) : base(_skill, eSkillState.CREATE)
    {
        m_skill = _skill;
        m_path = _path;
        m_speed = _speed;
        m_lifeTime = _lifeTime;
    }

    private void NextState()
    {
        m_skill.SetState(eSkillState.IDLE);
    }

    public override void Enter(FsmMsg _msg)
    {
        base.Enter(_msg);
        Bullet _bullet = Util.ResUtil.Create<Bullet>(m_path, InGameMgr.Instance.m_mosterAttach);
        _bullet.transform.position = m_skill.SkillImage.transform.position;
        _bullet.transform.eulerAngles = m_skill.transform.eulerAngles;
        _bullet.Open(m_skill.skillRecord, (InGameMgr.Instance.hero.transform.position - m_skill.SkillImage.transform.position).normalized, m_speed, m_lifeTime);

    }

    public override void Update()
    {
        base.Update();
        NextState();
    }
}


public class SkillState_IDLE : SkillState
{
    float m_timer;
    eSkillState m_nextState;
    public SkillState_IDLE(Skill _skill, eSkillState _nextState) : base(_skill, eSkillState.IDLE)
    {
        m_skill = _skill;
        m_nextState = _nextState;
    }

    public override void Enter(FsmMsg _msg)
    {
        base.Enter(_msg);
        m_timer = 0;
    }

    public void NextState()
    {
        m_skill.SetState(m_nextState);
    }

    public override void Update()
    {
        base.Update();
        m_timer += Time.deltaTime;
        if (m_skill.skillRecord.FCoolTime <= m_timer)
        {
            NextState();
        }
    }
}