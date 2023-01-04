using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eASSET_TYPE
{
    GOLD,
    ENERGY,
    DIAMOND,
}

[System.Serializable]
public class GDAsset
{
    [SerializeField] private eASSET_TYPE m_type;
    [SerializeField] private long m_count;

    public eASSET_TYPE type { get { return m_type; } }
    public long count { get { return m_count; } }

    public GDAsset() { }
    public GDAsset(eASSET_TYPE _type) { m_type = _type; }
    public void SetCount(long _count)
    {
        m_count = _count;
        if (m_count < 0)
            m_count = 0;
    }
}

[System.Serializable]
public class GameData_Asset : GameData
{    
    static public GameData_Asset Instance { get { return GameDataMgr.Instance.GetData<GameData_Asset>(); } }

    [SerializeField] List<GDAsset> m_assetList = new List<GDAsset>();

 
    public long GetCount(eASSET_TYPE _type)
    {
        GDAsset _find = m_assetList.Find(item => item.type == _type);
        if (_find == null)
            return 0;

        return _find.count;
    }
    public void SetCount(eASSET_TYPE _type, long _count)
    {
        //GDAsset _find = m_assetList.Find(item => item.type == _type);
        //if( null == _find)
        //{
        //    m_assetList.Add(_find = new GDAsset(_type));
        //}

        //_find.SetCount(_count);

        //AllChange();
    }

    public override void Save()
    {
        Debug.Log("save");
        base.Save();
    }
    public void AddCount(eASSET_TYPE _type, long _count)
    {
        SetCount(_type, GetCount(_type) + _count);
    }
}
