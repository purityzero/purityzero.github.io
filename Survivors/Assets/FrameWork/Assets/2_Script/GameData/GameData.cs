using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : Subject
{
    private bool m_isNeedSave;
    public bool isNeedSave { get { return m_isNeedSave; } }

    public void SetNeedSave(bool _isNeedSave) 
    {
        m_isNeedSave = _isNeedSave;
    }

    public void AllChange()
    {
        SetNeedSave(true);
        SetNotify();
    }

    public virtual void Init()
    {

    }
    public virtual void Logout()
    {
        Init();
    }
    public virtual void Save()
    {

    }
    public virtual void Load()
    {

    }

    public virtual void Remove()
    {

    }

    public GameData GetData()
    {
        return this;
    }

    public virtual void UpdateLogic()
    {
        Update();
    }

}