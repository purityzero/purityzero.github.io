using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eCUTSCENE_EVENT_TYPE 
{ 
    NEXT_PAGE,

}


public enum eCUTSCENE_PAGEDATA_TYPE
{
    TALK,
    BUTTON,
    TEXT,
    IMAGE,
    MASK,
}

[System.Serializable]
public class CutSceneRecord_evnet
{
    public eCUTSCENE_EVENT_TYPE eventType;   

    public CutSceneRecord_evnet() { }
    public CutSceneRecord_evnet(CutSceneRecord_evnet _other)
    {
        SetData(_other);
    }

    public void SetData(CutSceneRecord_evnet _other)
    {
        eventType = _other.eventType;
    }
}


[System.Serializable]
public class CutSceneRecord_pagedata
{
    public eCUTSCENE_PAGEDATA_TYPE pageDataType;
    public string path;
    public Vector3 pos;
    public Vector3 size = Vector3.one;   
    public CutSceneRecord_evnet pageEvent;

    public CutSceneRecord_pagedata() { }
    public CutSceneRecord_pagedata(CutSceneRecord_pagedata _other)
    {
        pageDataType = _other.pageDataType;      
        path = _other.path;
        pos = _other.pos;
        size = _other.size;
        pageEvent = null;
        if (null != _other.pageEvent)
        {
            pageEvent = new CutSceneRecord_evnet(_other.pageEvent);
        }
    }
}


[System.Serializable]
public class CutSceneRecord_page
{
    public bool isBack = true;
    public bool isLock = true; 
    public bool isSkip = true;
    public bool isHideEvent = false;
    public List<CutSceneRecord_pagedata> pageDataList = new List<CutSceneRecord_pagedata>();

    public CutSceneRecord_page() { }
    public CutSceneRecord_page(CutSceneRecord_page _record)
    {
        SetData(_record);
    }

    public void SetData(CutSceneRecord_page _other)
    {
        isBack = _other.isBack;
        isLock = _other.isLock;
        isSkip = _other.isSkip;
        isHideEvent = _other.isHideEvent;

        pageDataList.Clear();
        for (int i = 0; i < _other.pageDataList.Count; ++i)
        {
            pageDataList.Add(new CutSceneRecord_pagedata(_other.pageDataList[i]));
        }
    }
}


[System.Serializable]
public class CutSceneRecord : Record
{
    public List<CutSceneRecord_page> pageList = new List<CutSceneRecord_page>();
    public string dest;

    public void SetData(CutSceneRecord _other)
    {
        pageList.Clear();
        for (int i = 0; i < _other.pageList.Count; ++i)
        {
            pageList.Add(new CutSceneRecord_page(_other.pageList[i]));
        }
    }
}

public class CutSceneTable : Table<CutSceneRecord>
{

}
