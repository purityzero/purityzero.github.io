using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILevelUp : UIDialog
{
    public UISlotList slotList;

    public override void Open()
    {
        base.Open();
        slotList.clickAction = OnClickSlot;
        slotList.Clear();
        var _list =  SkillTable.Instance.GetLevelUpSkillList();
        if(_list.Count <= 0)
        {
            MaxLv();
        }

        for (int i = 0; i < _list.Count; i++)
        {
            var _slot = slotList.GetSlot(i) as UILevelUpSlot;
            _slot.Open(_list[i]);
        }
    }

    private void MaxLv()
    {
        var _asset = InGameMgr.Instance.GetAsset<ExpAsset>();
        _asset.MaxLv();
        Close();
    }

    public void OnClickSlot(UISlot _slot)
    {
        var _lvUpSlot = _slot as UILevelUpSlot;

        InGameMgr.Instance.hero.AddSkill(_lvUpSlot.m_record);
        Close();
    }
}
