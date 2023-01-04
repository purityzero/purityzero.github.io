using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataProxy<TGameData> : IGameData where TGameData : GameData, new()
{
    string m_path;
    TGameData m_gameData;

    public GameData GetData()
    { 
        return m_gameData;
    }
    public void SetPath(string _path)
    {
        m_path = _path;
    }

    public void Init()
    {
        if (null == m_gameData)
            m_gameData = new TGameData();

        m_gameData.Init();
    }
    public void Logout()
    {
        m_gameData.Logout();
    }
    public void Save()
    {
        string _data = JsonUtility.ToJson(m_gameData);
        if( Util.FileUtil.Save(m_path, _data)  == false)
        {
            Debug.LogError("save failed : " + m_path);
            return;
        }
         
        m_gameData.Save();
    }
    public void Load()
    {
        string _data = Util.FileUtil.Load(m_path);
        m_gameData = JsonUtility.FromJson<TGameData>(_data);
        if( null == m_gameData)
        {
            Init();
            return;
        }

        m_gameData.Load();
    }
    

    public void Remove()
    {
        m_gameData = null;
        Util.FileUtil.Delete(m_path);
    }

    public void UpdateLogic()
    {
        if (m_gameData == null)
            return;

        m_gameData.Update();
        if (m_gameData.isNeedSave)
        {
            m_gameData.SetNeedSave(false);
            Save();
        }
    }
}
