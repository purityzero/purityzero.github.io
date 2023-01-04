using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAssetList : UIDialog
{
    public Asset[] Assets;
    public Button OptionBtn;

    private void Awake()
    {
        Util.UIUtil.SetBtnClick(OptionBtn, OnOption);
    }

    public override void Open()
    {
        base.Open();
        for (int i = 0; i < Assets.Length; ++i)
        {
            Assets[i].Open();
        }

    }
    public T GetAsset<T>() where T : Asset
    {
        T _findAsset = null;

        for (int i = 0; i < Assets.Length; ++i)
        {
            if (Assets[i] is T == false)
                continue;

            _findAsset = Assets[i] as T;
        }

        if (_findAsset == null)
        {
            Debug.LogError("no have Type Asset ");
            return null;
        }

        return _findAsset;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        for (int i = 0; i < Assets.Length; ++i)
        {
            Assets[i].UpdateLogic();
        }
    }

    private void OnOption()
    {
        var optionDlg = UIMgr.Instance.popup.GetDialog<UIInGameFinish>("Prefab/UIInGameFinish");
        optionDlg.Open(eInGamePopup.Waiting);
        InGameMgr.Instance.SetNextState(eInGameFsmMsg.ChangeNextState, eInGameState.Popup);
    }
}
