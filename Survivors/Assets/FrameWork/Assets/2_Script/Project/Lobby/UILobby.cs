using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILobby : UIDialog
{
    public override void Open()
    {
        base.Open();
        UIMgr.Instance.popup.OpenDialog("Prefab/UILobbyMenu");
        UIMgr.Instance.Asset.OpenDialog("Prefab/UILobbyAsset");
    }
}
