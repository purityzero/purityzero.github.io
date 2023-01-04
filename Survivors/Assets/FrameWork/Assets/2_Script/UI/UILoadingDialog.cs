using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UILoadingDialog : UIFadeDialog
{
    AsyncOperation m_asyncOperation => SceneMgr.Instance.getAsyncLoad;
    public Text m_PrecentText;
    public TextMeshProUGUI m_LoadingText;
    public override void Open(eSTATE _state, UnityAction _complete, float _duration)
    {
        base.Open(_state, _complete, _duration);
        Util.UIUtil.SetText(m_PrecentText, "0%");
        Util.UIUtil.SetShow(m_PrecentText?.gameObject, m_asyncOperation != null);
    }


    public override void Close()
    {
        base.Close();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        //if (m_state == eSTATE.NONE)
        //    return;

        //if (m_asyncOperation != null)
        //{
        //    Util.UIUtil.SetText(m_PrecentText, $"{m_asyncOperation.progress}%");
        //}

        //if (m_state == eSTATE.FADE_IN)
        //{
        //    Util.UIUtil.SetAlpha(m_PrecentText, curve.Evaluate(m_time));
        //    Util.UIUtil.SetAlpha(m_LoadingText, curve.Evaluate(m_time));
        //}
        //else
        //{
        //    Util.UIUtil.SetAlpha(m_PrecentText, 1f - curve.Evaluate(m_time));
        //    Util.UIUtil.SetAlpha(m_LoadingText, 1f -curve.Evaluate(m_time));
        //}
    }
}
