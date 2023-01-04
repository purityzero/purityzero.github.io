using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHeroSelectSlot : UISlot
{
    public Image HeroImage;
    public GameObject LockImage;
    private int m_heroIdx;
    public int HeroIdx => m_heroIdx;

    public void Open(int _heroIdx)
    {
        base.Open();
        m_heroIdx = _heroIdx;
        var _getHeroData = TableMgr.Instance.GetTable<HeroTable>().Get(_heroIdx);

        Util.UIUtil.SetIcon(HeroImage, _getHeroData.SImagePath[0]);
        Util.UIUtil.SetShow(LockImage, GameData_User.Instance.isHeroLock(_heroIdx));
    }
}
