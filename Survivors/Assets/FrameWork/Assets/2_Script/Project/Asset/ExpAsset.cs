using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpAsset : Asset
{
    public Slider ExpBar;
    private bool isMaxLv = false;

    public override void Open()
    {
       // base.Open();
        ExpBar.value = 0f;
        isMaxLv = false;
        count = 0;
        Util.UIUtil.SetText(text, $"{ExpTable.Instance.Get(1).LNextExp} / {count}");
    }


    public override void Notify(Subject _subject)
    {
        if (count == GameData_Stage.Instance.exp)
            return;
        Debug.Log("############ Exp Noti");
        count = GameData_Stage.Instance.exp;
        SetText();
    }

    public void MaxLv()
    {
        Util.UIUtil.SetText(text, "Max Lv");
        isMaxLv = true;
    }

    public override void SetText()
    {
        long _exp = 0;
        if (isMaxLv)
            return;

        if (GameData_Stage.Instance == null)
        {
            _exp = ExpTable.Instance.Get(1).LNextExp;
        }
        else
        {
            _exp = ExpTable.Instance.Get(GameData_Stage.Instance.lv).LNextExp;
        }
        Util.UIUtil.SetText(text, $"{_exp} / {count}");
        ExpBar.value = (float)count / _exp;
    }

}
