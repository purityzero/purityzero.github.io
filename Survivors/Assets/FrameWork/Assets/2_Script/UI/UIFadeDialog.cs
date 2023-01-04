using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIFadeDialog : UIDialog
{
    public enum eSTATE
    {
        NONE,
        FADE_IN,
        FADE_OUT,
    }

    public Image image;
    public AnimationCurve curve;

    protected eSTATE m_state;
    protected float m_time;
    protected float m_duration;
    protected UnityEngine.Events.UnityAction m_complete;

    public virtual void Open( eSTATE _state, UnityEngine.Events.UnityAction _complete, float _duration)
    {
        base.Open();
        m_state = _state;
        m_complete = _complete;
        m_duration = _duration;
        m_time = 0f;

        Util.UIUtil.SetAlpha(image, m_state == eSTATE.FADE_IN ? 0f : 1f);
    }

    public virtual void FadeIn(UnityEngine.Events.UnityAction _complete, float _duration = 0.8f )
    {
        Open(eSTATE.FADE_IN, _complete, _duration);
    }

    public virtual void FadeOut(UnityEngine.Events.UnityAction _complete, float _duration = 0.8f)
    {
        Open(eSTATE.FADE_OUT, _complete, _duration);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (m_state == eSTATE.NONE)
            return;

        if (m_duration <= 0f)
        {
            m_time = 1f;
        }
        else
        {
            m_time += Time.deltaTime / m_duration;
        }

        if (m_state == eSTATE.FADE_IN)
        {
            Util.UIUtil.SetAlpha(image, curve.Evaluate(m_time));
        }
        else
        {
            Util.UIUtil.SetAlpha(image, 1f - curve.Evaluate(m_time));
        }

        if( m_time >= 1f )
        {
            if (m_state == eSTATE.FADE_OUT)
                Close();

            m_state = eSTATE.NONE;
            if (null != m_complete)
                m_complete();
        }
    }
}
