using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;

public enum eDicrectionType
{
    Middle = 0,
    Left,
    Right,
    Up,
    Down,
    LeftUp,
    LeftDown,
    RightUp,
    RightDown,
}

public class BGMgr : MonoSingletonManager<BGMgr>
{
    public int MaxCount = 15;
    private MemoryPooling<BG> BgPool;
    private int mainBgIdx = 0;

    private Vector2 CurrentVector = Vector2.zero;

    public RectTransform ParentRect;

    private void Start()
    {
        BgPool = new MemoryPooling<BG>(this.transform, MaxCount);
        var _item = BgPool.Get("Prefab/BG");
        _item.Open(Vector2.zero, eDicrectionType.Middle, true);
        mainBgIdx = 0;
        BgCreate(Vector2.zero);
    }

    private void Update()
    {
        BgPool.UpdateLogic();
        SetMain();
    }

    private void SetMain()
    {
        Vector3 _heroPos = InGameMgr.Instance.hero.transform.position;
        int _idx = 0;
        float _tempSize = 9999f;
        for (int i = 0; i < BgPool.activeList.Count; i++)
        {
            float _temp = Mathf.Abs(((_heroPos - BgPool.activeList[i].item.transform.position)).magnitude);
            if (_tempSize > _temp)
            {
                _idx = i;
                _tempSize = _temp;
            }
        }

        if (_idx != mainBgIdx)
        {
            var _main = BgPool.activeList[_idx];
            _main.item.Open(_main.item.getRectTrasform.anchoredPosition, eDicrectionType.Middle, true);
            BgCreate(_main.item.getRectTrasform.anchoredPosition);
            BgHide(_main.item.getRectTrasform.anchoredPosition);
            mainBgIdx = _idx;
        }
    }

    private void BgCreate(Vector2 _pos)
    {
        IEnumerator _type = Enum.GetValues(typeof(eDicrectionType)).GetEnumerator();
        while (_type.MoveNext())
        {
            eDicrectionType type = (eDicrectionType)_type.Current;
            if (type == eDicrectionType.Middle)
                continue;

            Vector2 _getPos = GetDircetionPosition(type, _pos);
            MemoryPoolingItem<BG> _item = null;
            _item = BgPool.activeList.Find(_x => _x.item.getRectTrasform.anchoredPosition == _getPos);


            if (_item != null)
            {
                _item.item.Open(_item.item.getRectTrasform.anchoredPosition, type);
            }
            else
            {
                if (BgPool.activeList.Count == MaxCount)
                {
                    _item = BgPool.activeList.Find(x => x.item.isClose == true);
                }
                else
                {
                    _item = BgPool.GetItem("Prefab/BG", null);
                }
                _item.item.Open(_getPos, type);
            }
        }
    }


    private void BgHide(Vector2 _pos)
    {
        IEnumerator _type = Enum.GetValues(typeof(eDicrectionType)).GetEnumerator();
        List<Vector2> tmpVecList = new List<Vector2>();

        while (_type.MoveNext())
        {
            tmpVecList.Add(GetDircetionPosition((eDicrectionType)_type.Current, _pos));
        }

        for (int i = 0; i < BgPool.activeList.Count; i++)
        {
            if (tmpVecList.Exists(_x => _x == BgPool.activeList[i].item.getRectTrasform.anchoredPosition))
            {
                continue;
            }
            BgPool.activeList[i].item.Close();
        }
    }

    private Vector2 GetDircetionPosition(eDicrectionType _type, Vector2 _pos)
    {
        float _width = ParentRect.rect.width;
        float _hight = ParentRect.rect.height;

        switch (_type)
        {
            case eDicrectionType.Middle:
                return _pos;
            case eDicrectionType.Down:
                return _pos + (Vector2.down * _hight);
            case eDicrectionType.Up:
                return _pos + (Vector2.up * _hight);
            case eDicrectionType.Left:
                return _pos + (Vector2.left * _width);
            case eDicrectionType.Right:
                return _pos + (Vector2.right * _width);
            case eDicrectionType.LeftDown:
                return _pos + (Vector2.left * _width) + (Vector2.down * _hight);
            case eDicrectionType.LeftUp:
                return _pos + (Vector2.left * _width) + (Vector2.up * _hight);
            case eDicrectionType.RightDown:
                return _pos + (Vector2.right * _width) + (Vector2.down * _hight);
            case eDicrectionType.RightUp:
                return _pos + (Vector2.right * _width) + (Vector2.up * _hight);

            default:
                Debug.Log("찾을 수 없습니다 : " + _type.ToString());
                return _pos;

        }
    }

}
