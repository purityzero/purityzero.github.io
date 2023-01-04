using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class GameDataMgr : ClassSingletonManager<GameDataMgr>
{  
    protected Dictionary<System.Type, IGameData> m_dataList = new Dictionary<System.Type, IGameData>();

    public GameDataMgr()
    {
        AddData<GameData_User>(true);
    }


    private void AddData<TGameData>(bool _isUseSave) where TGameData : GameData, new()
    {
        System.Type _type = typeof(TGameData);
        if( m_dataList.ContainsKey(_type) == true )
        {
            Debug.LogError("GameDataManager::AddData() [ have : " + _type.ToString());
            return;
        }
        GameDataProxy<TGameData> _gameDataProxy = new GameDataProxy<TGameData>();        
        m_dataList.Add(_type, _gameDataProxy);

        if (_isUseSave)
        {
            _gameDataProxy.SetPath($"{Application.persistentDataPath}/Data/{typeof(TGameData)}.bytes");
        }
    }

    public TGameData GetData<TGameData>() where TGameData : GameData
    {
        System.Type _type = typeof(TGameData);
        if (m_dataList.ContainsKey(_type) == false)
        {
            Debug.LogError("GameDataManager::GetData()[ no Have : " + _type.ToString());
            return null;
        }
        return m_dataList[_type].GetData() as TGameData;
    }

    public void Load()
    {
        var _var = m_dataList.GetEnumerator();
        while (_var.MoveNext())
        {
            _var.Current.Value.Load();
        }
    }

    private void RemoveData<TGameData>() where TGameData : GameData
    {
        System.Type _type = typeof(TGameData);
        if (m_dataList.ContainsKey(_type) == false)
        {
            return;
        }
        m_dataList[_type].Remove();
        m_dataList.Remove(_type);
    }


    public void SetStageData()
    {
        System.Type _type = typeof(GameData_Stage);
        if (IsData<GameData_Stage>() == false)
        {
            AddData<GameData_Stage>(true);
        }
        m_dataList[_type].Load();
        Save<GameData_Stage>();
    }

    public bool IsStageData()
    {
        string _data = $"{Application.persistentDataPath}/Data/GameData_Stage.bytes";
        return Util.FileUtil.Exists(_data);
    }


    public void RemoveStageData()
    {
        RemoveData<GameData_Stage>();

        string _data = $"{Application.persistentDataPath}/Data/GameData_Stage.bytes";
        Util.FileUtil.Delete(_data);
        Debug.Log("삭제 완료");
    }

    public void Save<TGameData>() where TGameData : GameData
    {
        System.Type _type = typeof(TGameData);
        if (m_dataList.ContainsKey(_type) == false)
        {
            Debug.LogError("GameDataManager::GetData()[ no Have : " + _type.ToString());
            return;
        }
        m_dataList[_type].Save();
    }

    public bool IsData<TGameData>() where TGameData : GameData
    {
        System.Type _type = typeof(TGameData);
        return m_dataList.ContainsKey(_type);
    }


    public void Logout()
    {
        var _Var = m_dataList.GetEnumerator();
        while(_Var.MoveNext())
        {
            _Var.Current.Value.Logout();
        }
    }

    public void UpdateLogic()
    {
        var _Var = m_dataList.GetEnumerator();
        while (_Var.MoveNext())
        {
            _Var.Current.Value.UpdateLogic();
        }
    }
}
