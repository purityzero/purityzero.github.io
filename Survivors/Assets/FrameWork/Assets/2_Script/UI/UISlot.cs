using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBase
{
    public Image icon;
    public Button btnClick;
    public GameObject select;
    public GameObject content;

    public UnityEngine.Events.UnityAction<UISlot> clickAction;

    RectTransform m_rectTransform;
    public RectTransform getRectTransform { get { return m_rectTransform; } }

    public virtual void SetSelect(bool _isShow)
    {
        Util.UIUtil.SetShow(select, _isShow);
    }

    public bool isSelect
    {
        get
        {
            return Util.UIUtil.IsShow(select);
        }
    }

    public virtual void ResetData()
    {

    }

    public void SetShow(bool _isShow)
    {
        Util.UIUtil.SetShow(content, _isShow);      
    }

    protected virtual void Awake()
    {
        Util.UIUtil.SetBtnClick(btnClick, OnClick);
    }

    public override void Open()
    {
        base.Open();
        SetShow(true);
        SetSelect(false);
        m_rectTransform = GetComponent<RectTransform>();
    }

    public virtual void OnClick()
    {
        if (null != clickAction)
            clickAction(this);
    }

}
