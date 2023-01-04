using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIContinueGame : UIDialog
{
    public Button BtnYes;
    public Button BtnNo;
    void Start()
    {
        Util.UIUtil.SetBtnClick(BtnYes, OnYes);
        Util.UIUtil.SetBtnClick(BtnNo, OnNo);
    }

    public void OnYes()
    {
        SceneMgr.Instance.LoadScene(eSceneType.Game);
        Close();
    }

    public void OnNo()
    {
        GameDataMgr.Instance.RemoveStageData();
        Close();
    }


}
