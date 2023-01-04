using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelUpSlot : UISlot
{
    public Text SkillName;
    public Text SkillInfo;

    public SkillRecord m_record;

    public void Open(SkillRecord _record)
    {
        base.Open();
        m_record = _record;
        Util.UIUtil.SetText(SkillName, m_record.eType.ToString());
        Util.UIUtil.SetText(SkillInfo, m_record.sInfo + $"\n 데미지 : {m_record.LDamage} 쿨타임 : {m_record.FCoolTime} 옵션 : {m_record.SkillOptionText()} 옵션 횟수 : {m_record.fOption}");
    }

}
