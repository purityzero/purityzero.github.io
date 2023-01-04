using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum eSCROLL_TYPE
{
    horizon,
    vertical,
    both,
}

public class UISlotList : MonoBase
{
    public ScrollRect scrollRect;
    public Transform attachSlot;
    public string slotPath;
    public UnityEngine.Events.UnityAction<UISlot> clickAction;
    public eSCROLL_TYPE scrollType;

    public List<UISlot> m_slotList = new List<UISlot>();    
    protected UISlot m_select;

    public List<UISlot> getSlotList
    {
        get
        {
            return m_slotList;
        }
    }

    public UISlot getSelect
    {
        get
        {
            return m_select;
        }
    }

    public int GetSlotCount()
    {
        int _count = 0;
        for( int i=0; i<m_slotList.Count; ++i )
        {
            if (m_slotList[i] == null)
                break;

            if (m_slotList[i].isOpen == false)
                break;
            ++_count;
        }

        return _count;
    }

    public UISlot GetActiveSlot( int _idx)
    {
        int _slotIdx = 0;
        for (int i = 0; i < m_slotList.Count; ++i)
        {
            if (m_slotList[i] == null)
                break;

            if (m_slotList[i].isOpen == false)
                break;

            if (_slotIdx == _idx)
                return m_slotList[i];

            ++_slotIdx;
        }

        return null;
    }

    public virtual void Clear()
    {
        for (int i = 0; i < m_slotList.Count; ++i)
        {
            if (null == m_slotList[i])
                continue;

            GameObject.Destroy(m_slotList[i].gameObject);
        }

        m_slotList.Clear();
    }

    public virtual void CloseSlotList()
    {
        for (int i = 0; i < m_slotList.Count; ++i)
        {
            if (null == m_slotList[i])
                continue;

            m_slotList[i].Close();
        }

        SetSelect(null);
    }

    public virtual UISlot GetSlot(int _idx)
    {
        if (m_slotList.Count <= _idx)
        {
            UISlot _slot = Util.ResUtil.Create<UISlot>(slotPath, attachSlot);
            if (null == _slot)
                return null;

            _slot.gameObject.name = _idx.ToString();
            _slot.clickAction = clickAction;
            m_slotList.Add(_slot);
            return _slot;
        }
        return m_slotList[_idx];
    }

    public virtual T TGetSlot<T>(int _idx) where T : UISlot
    {
       return  GetSlot(_idx) as T;       
    }

    public virtual void SetSelect(int _idx)
    {
        if (m_slotList.Count <= _idx)
            return;

        if (m_slotList[_idx] == null)
            return;

        if (m_slotList[_idx].isOpen == true)
            return;

        SetSelect(m_slotList[_idx]);
    }

    public virtual void SetSelect(UISlot _slot)
    {
        if (null != m_select)
            m_select.SetSelect(false);

        m_select = _slot;

        if (null != m_select)
            m_select.SetSelect(true);
    }   

    public void ResetData()
    {
        for (int i = 0; i < m_slotList.Count; ++i)
        {
            if (null == m_slotList[i])
                continue;

            if (m_slotList[i].isOpen == false)
                continue;

            m_slotList[i].ResetData();
        }
    }

    public override void UpdateLogic()
    {
        for (int i = 0; i < m_slotList.Count; ++i)
        {
            if (null == m_slotList[i])
                continue;

            if (m_slotList[i].isOpen == false)
                continue;

            m_slotList[i].UpdateLogic();
        }
    }

    public virtual void SetScroll( float _value )
    {
        if (scrollRect == null)
            return;
        if (scrollType == eSCROLL_TYPE.vertical)
        {
            scrollRect.verticalNormalizedPosition = _value;
        }
        else
        {
            scrollRect.horizontalNormalizedPosition = _value;
        }
    }

    public virtual void SetScroll(UISlot _slot)
    {
        if(scrollType == eSCROLL_TYPE.horizon)
        {
            SetScrollWidth(_slot);
        }
        else if (scrollType == eSCROLL_TYPE.vertical)
        {
            SetScrollHeight(_slot);
        }

    }


    private void SetScrollHeight(UISlot _slot)
    {
        bool _isLast = false;
        float _result = 0;
        for (int i = 0; i < GetSlotCount(); i++)
        {
            if (i == GetSlotCount() - 1)
            {
                _isLast = true;
                break;
            }

            if (_slot == GetSlot(i))
            {
                _isLast = false;
                break;
            }
        }

        if (_isLast == true)
        {
            _result = -(_slot.transform.localPosition.y + _slot.getRectTransform.rect.height) + 100;
        }
        else
        {
            float _scrollRectheight = scrollRect.content.rect.height;
            float _distance = _scrollRectheight - _slot.transform.localPosition.y;
            _result = -(_scrollRectheight - _distance) + (_slot.getRectTransform.rect.y / 2 + 100);
        }

        scrollRect.content.anchoredPosition = new Vector2(scrollRect.content.anchoredPosition.x, _result);
    }

    private void SetScrollWidth(UISlot _slot)
    {
        bool _isLast = false;
        float _result = 0;
        for (int i = 0; i < GetSlotCount(); i++)
        {
            if (i == GetSlotCount() - 1)
            {
                _isLast = true;
                break;
            }

            if (_slot == GetSlot(i))
            {
                _isLast = false;
                break;
            }
        }

        if (_isLast == true)
        {
            _result = -(_slot.transform.localPosition.x + _slot.getRectTransform.rect.width) + 100;
        }
        else
        {
            float _scrollRectWidth = scrollRect.content.rect.width;
            float _distance = _scrollRectWidth - _slot.transform.localPosition.x;
            _result = -(_scrollRectWidth - _distance) + (_slot.getRectTransform.rect.x / 2 + 100);
        }

        scrollRect.content.anchoredPosition = new Vector2(_result, scrollRect.content.anchoredPosition.y);
    }
}
