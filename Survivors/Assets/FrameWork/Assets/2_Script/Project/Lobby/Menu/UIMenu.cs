using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class UIMenu : UIDialog
{
    public Button NewGame;
    public Button HeroSelect;

    protected void Start()
    {
    }

    public override void Open()
    {
        base.Open();
        Debug.Log("Open Menu");
        UIUtil.SetBtnClick(NewGame, OnNewGame);
        UIUtil.SetBtnClick(HeroSelect, OnHeroSelect);
    }




    private void OnNewGame()
    {
        //SceneMgr.Instance.LoadScene(eSceneType.Game);
        UIMgr.Instance.popup.OpenDialog("Prefab/UIStageSelect");
    }

    private void OnHeroSelect()
    {
        Debug.Log("OnHeroSelect");
        var _heroDlg = UIMgr.Instance.popup.GetDialog<HeroSelectDlg>("Prefab/HeroSelectDlg");
        _heroDlg.Open();
    }
}
