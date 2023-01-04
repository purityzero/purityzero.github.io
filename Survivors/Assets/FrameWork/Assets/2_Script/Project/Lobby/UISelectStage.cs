using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]
public class StageButton 
{
    public Text text;
    public Button button;

}


public class UISelectStage : UIDialog
{
    public StageButton stageButton;
    public Button NextButton;
    public Button BackButton;
    public Text ToastText;
    public Text BestTimeText;
    private Tweener m_tweener;


    public void Awake()
    {
        Util.UIUtil.SetBtnClick(NextButton, OnNextButton);
        Util.UIUtil.SetBtnClick(BackButton, OnBackButton);
        Util.UIUtil.SetBtnClick(stageButton.button, OnStageButton);

    }

    public override void Open()
    {
        base.Open();
        Util.UIUtil.SetText(stageButton.text, "Stage " + GameData_User.Instance.selectStage.ToString());
        SetBestTime();

    }

    private void OnStageButton()
    {
        SceneMgr.Instance.LoadScene(eSceneType.Game);
        Close();
    }

    private void OnNextButton()
    {
        bool isNext = GameData_User.Instance.IsClear(GameData_User.Instance.selectStage);
        if (isNext == false)
        {
            ShowToastMessage("아직 이전 단계를 클리어 하지 않으셨어요.");
            return;
        }
        GameData_User.Instance.SetStage(GameData_User.Instance.selectStage + 1);
        Util.UIUtil.SetText(stageButton.text, "Stage " + GameData_User.Instance.selectStage.ToString());
        SetBestTime();
    }

    private void OnBackButton()
    {
        if (GameData_User.Instance.selectStage == 1)
        {
            ShowToastMessage("1단계 입니다.");
            return;
        }
        GameData_User.Instance.SetStage(GameData_User.Instance.selectStage - 1);
        Util.UIUtil.SetText(stageButton.text, "Stage " + GameData_User.Instance.selectStage.ToString());
        SetBestTime();
    }

    private void SetBestTime()
    {
        var _time = GameData_User.Instance.GetBestTime(GameData_User.Instance.selectStage);
        if (_time <= 0)
        {
            Util.UIUtil.SetText(BestTimeText, $"BestTime : 최초 플레이");
            return;
        }

        Util.UIUtil.SetText(BestTimeText, $"BestTime : {Util.TimeUtil.GetMinute(_time).ToString("00")}:{Util.TimeUtil.GetSecond(_time)}");
    }

    private void ShowToastMessage(string _text)
    {
        m_tweener?.Pause();
        Util.UIUtil.SetText(ToastText, _text);
        ToastText.color = new Color(ToastText.color.r, ToastText.color.g, ToastText.color.b, 1);
        m_tweener = ToastText.DOFade(0, 2f);
    }
}
