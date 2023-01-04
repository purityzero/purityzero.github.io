using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Lego : Skill
{
    public override void Open(SkillRecord _skillRecord)
    {
        base.Open(_skillRecord);
        m_Fsm.AddFsm(new SkillState_IDLE(this, eSkillState.CREATE));
        m_Fsm.AddFsm(new SkillState_CREATE(this, "Prefab/Lego", 0, 10));

        m_Fsm.SetState(eSkillState.IDLE);
    }
}
