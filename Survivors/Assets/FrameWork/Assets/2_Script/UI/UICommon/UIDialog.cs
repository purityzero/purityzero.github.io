using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDialog : MonoBase
{
    public Button[] btnCloses;
    protected RectTransform m_rectTransform;

    public override void Open()
    {
        base.Open();
        m_rectTransform = transform as RectTransform;
        m_rectTransform.offsetMin = Vector2.zero;
        m_rectTransform.offsetMax = Vector2.zero;
        Util.UIUtil.SetBtnClick(btnCloses, OnClick_Close);
        transform.SetAsLastSibling();
    }
    public virtual void OnClick_Close()
    {
        Close();
    }
}
