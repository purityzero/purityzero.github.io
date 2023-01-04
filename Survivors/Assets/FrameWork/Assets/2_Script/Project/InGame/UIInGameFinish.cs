using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum eInGamePopup
{
    Finish,
    Waiting
}

public class UIInGameFinish : UIDialog
{
    public Text Title;
    public Text MoneyText;
    public Text KillText;
    public Text TimeText;
    public Text Level;

    public Button CloseBtn;
    public Button OkBtn;

    private void Awake()
    {
        Util.UIUtil.SetBtnClick(OkBtn, OnOk);
        Util.UIUtil.SetBtnClick(CloseBtn, OnClose);
    }

    public void Open(eInGamePopup _type)
    {
        base.Open();
        Util.UIUtil.SetText(Title, _type.ToString());
        Util.UIUtil.SetText(KillText, "적을 죽인 횟수 : " + GameData_Stage.Instance.kill.ToString("#,###"));
        Util.UIUtil.SetText(TimeText, "생존 시간 : " + GameData_Stage.Instance.time.ToString("#,###"));
        Util.UIUtil.SetText(MoneyText, "획득한 돈 : " + GameData_Stage.Instance.money.ToString("#,###"));
        Util.UIUtil.SetText(Level, "Level : " + GameData_Stage.Instance.lv.ToString("#,###"));

        Util.UIUtil.SetShow(CloseBtn.gameObject, _type == eInGamePopup.Waiting);
    }

    private void OnClose()
    {
        Close();
    }

    private void OnOk()
    {
        GameData_User.Instance.AddMoney(GameData_Stage.Instance.money);
        GameDataMgr.Instance.RemoveStageData();
        SceneMgr.Instance.LoadScene(eSceneType.Lobby);
    }
}
