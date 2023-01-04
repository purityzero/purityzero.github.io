using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BestTImeData
{
    public int id;
    [SerializeField]
    private int m_value = -1;
    public int value => m_value;
    public bool isClear = false;

    public BestTImeData(int _id)
    {
        id = _id;
    }

    public void SetValue(int _value)
    {
        if (m_value == -1)
            m_value = _value;

        if (m_value > _value)
        {
            m_value = _value;
        }
    }
}
[System.Serializable]
public class HeroData
{
    [SerializeField]
    int m_idx;
    [SerializeField]
    bool m_isLock;
    public bool IsLock => m_isLock;
    public int Idx => m_idx;
    public HeroData(int _idx, bool _isLock)
    {
        m_idx = _idx;
        m_isLock = _isLock;
    }

    public void SetLock(bool _isActive)
    {
        m_isLock = _isActive;
    }
}

[System.Serializable]
public class GameData_User : GameData
{
    public static GameData_User Instance { get { return GameDataMgr.Instance.GetData<GameData_User>(); } }
    [SerializeField]
    private int SelectHero;
    [SerializeField]
    private long Money;
    [SerializeField]
    private List<BestTImeData> ListBestTime = new List<BestTImeData>();
    [SerializeField]
    public int selectStage;
    public long getMoney => Money;

    public int getSelectHero => SelectHero;

    public List<HeroData> listHeroData = new List<HeroData>();

    public override void Init()
    {
        base.Init();
        SelectHero = 1;
        selectStage = 1;
        Money = 0;
        for (int i = 0; i < HeroTable.Instance.list.Count; i++)
        {
            var getRecord = HeroTable.Instance.list[i];
            listHeroData.Add(new HeroData(getRecord.id, getRecord.bIsLock));
        }
        GameDataMgr.Instance.Save<GameData_User>();
    }

    public bool IsClear(int _id)
    {
        var _data = ListBestTime.Find(x => x.id == _id);
        if (_data == null)
            return false;

        return _data.isClear;
    }

    public int GetBestTime(int _id)
    {
        var _data = ListBestTime.Find(x => x.id == _id);
        if (_data == null)
        {
            Debug.Log("no best time record : " + _id);
            return 0;
        }
        return _data.value;
    }

    public void SetBestTime(int _id , int _time)
    {
        var _data = ListBestTime.Find(x => x.id == _id);
        if (_data == null)
        {
            Debug.Log("no best time record : " + _id);
            _data = new BestTImeData(_id);
            ListBestTime.Add(_data);
        }

        var _record  = StageTable.Instance.Get(_id);
        if (_record.iEndTime <= _time)
            _data.isClear = true;

        _data.SetValue(_time);
    }

    public void SetStage(int _id)
    {
        if(_id <= 1)
        {
            _id = 1;
        }

        selectStage = _id;
        SetNeedSave(true);
    }

    public void SetSelect(int _idx)
    {
        SelectHero = _idx;
        SetNeedSave(true);
    }

    public void AddMoney(long _money)
    {
        Money += _money;
        SetNeedSave(true);
    }

    public void SetMoney(long _money)
    {
        Money = _money;
        SetNeedSave(true);
    }

    public void SetHeroLock(int _idx, bool _isActive)
    {
        var data = listHeroData.Find(x => x.Idx == _idx);
        if (data == null)
        {
            Debug.LogError("HeroData is Null idx : " + _idx);
            return;
        }
        data.SetLock(_isActive);
        SetNeedSave(true);
    }

    public bool isHeroLock(int _idx)
    {
        var data = listHeroData.Find(x => x.Idx == _idx);
        if (data == null)
        {
            Debug.LogError("HeroData is Null idx : " + _idx);
            return true;
        }
        return data.IsLock;
    }

    public override void Update()
    {
        base.Update();
    }

}
