using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill_Stink : Skill
{
    public RectTransform m_rect;
    public override void Open(SkillRecord skillRecord)
    {
        base.Open(skillRecord);
        m_Fsm.AddFsm(new SkillState_IDLE(this, eSkillState.ATTACK));
        m_Fsm.AddFsm(new SkillState_Attack(this));

        Util.UIUtil.SetIcon(SkillImage, m_skillRecord.sImagePath, false);
        ReSetRecord(m_skillRecord);
        m_Fsm.SetState(eSkillState.IDLE);
    }

    public override void ReSetRecord(SkillRecord _record)
    {
        base.ReSetRecord(_record);
        m_rect.localScale = Vector3.one * m_skillRecord.fOption;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var _obj = InGameMgr.Instance.FindMonster(collision.gameObject);

        if (_obj != null)
        {
            if (m_RangeMonsterList.Exists(x => x == _obj) == false)
                m_RangeMonsterList.Add(_obj);
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        var _obj = InGameMgr.Instance.FindMonster(collision.gameObject);
        if (_obj != null)
        {
            if (m_RangeMonsterList.Exists(x => x == _obj))
                m_RangeMonsterList.Remove(_obj);
        }
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        for (int i = 0; i < m_RangeMonsterList.Count; i++)
        {
            if (m_RangeMonsterList[i] == null || m_RangeMonsterList[i].isOpen == false)
            {
                m_RangeMonsterList.Remove(m_RangeMonsterList[i]);
            }
        }
    }
}
