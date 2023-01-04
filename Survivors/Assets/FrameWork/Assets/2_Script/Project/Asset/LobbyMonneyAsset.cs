using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyMonneyAsset : MoneyAsset
{
    public override void Open()
    {
        base.Open();
        count = GameData_User.Instance.getMoney;
        currentCount = GameData_User.Instance.getMoney;
        SetText();
    }

    public override void Notify(Subject _subject)
    {
        if (GameData_User.Instance.getMoney == count)
            return;

        count = GameData_User.Instance.getMoney;
        SetText();
    }
}
