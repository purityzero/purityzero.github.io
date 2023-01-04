using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroSelectDlg : UIDialog
{
    [SerializeField]
    UISlotList m_slotList;

    public Image HeroImage;
    public Text atk;
    public Text hp;
    public Text Speed;
    public Text Explanation;


    public override void Open()
    {
        base.Open();
        m_slotList.Clear();
        m_slotList.clickAction = OnClickSlot;

        var _heroTable = TableMgr.Instance.GetTable<HeroTable>();
        for (int i = 0; i < _heroTable.list.Count; i++)
        {
            var _slot = m_slotList.GetSlot(i) as UIHeroSelectSlot;
            _slot.Open(_heroTable.list[i].id);
        }

        OnClickSlot(GetSlot(GameData_User.Instance.getSelectHero));
    }

    private UIHeroSelectSlot GetSlot(int _idx)
    {
       for (int i = 0; i < m_slotList.m_slotList.Count; i++)
        {
            UIHeroSelectSlot _slot = m_slotList.m_slotList[i] as UIHeroSelectSlot;
            if(_slot.HeroIdx == _idx)
            {
                return _slot;
            }
        }
        Debug.LogError("no find idx : " + _idx);
        return null;    
    }

    private void OnClickSlot(UISlot _slot)
    {
        var _selectSlot = _slot as UIHeroSelectSlot;
        Debug.Log(_selectSlot.HeroIdx);

        bool isLock = GameData_User.Instance.isHeroLock(_selectSlot.HeroIdx);
        if (isLock)
        {
            Debug.Log("Lock Popup");
            return;
        }

        var getRecord = HeroTable.Instance.Get(_selectSlot.HeroIdx);

        Util.UIUtil.SetIcon(HeroImage, getRecord.SImagePath[0]);
        Util.UIUtil.SetText(atk, "ATK : " + getRecord.Iatk.ToString());
        Util.UIUtil.SetText(hp, "HP : " + getRecord.Ihp.ToString());
        Util.UIUtil.SetText(Speed, "SPEED : " + getRecord.FSpeed.ToString());
        GameData_User.Instance.SetSelect(_selectSlot.HeroIdx);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        //m_slotList.UpdateLogic();
    }
}
