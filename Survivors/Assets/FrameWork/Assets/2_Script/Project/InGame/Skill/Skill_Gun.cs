using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill_Gun : Skill
{
    public override void Open(SkillRecord skillRecord)
    {
        base.Open(skillRecord);
        m_Fsm.AddFsm(new SkillState_IDLE(this, eSkillState.CREATE));
        m_Fsm.AddFsm(new SkillState_CREATE(this, "Prefab/Bullet", 70, 2));
        ReSetRecord(m_skillRecord);
        m_Fsm.SetState(eSkillState.IDLE);
    }

    public override void ReSetRecord(SkillRecord _record)
    {
        base.ReSetRecord(_record);

    }


    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (InGameMgr.Instance.JoyStick.isTouch)
        {
            gameObject.transform.eulerAngles = new Vector3(0, 0, InGameMgr.Instance.hero.GetAngle());
        }
    }

}
