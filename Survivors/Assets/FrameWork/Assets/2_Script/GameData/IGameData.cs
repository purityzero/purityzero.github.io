using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameData
{
    GameData GetData();

    void Init();
    void Logout();
    void Save();
    void Load();
    void UpdateLogic();
    void Remove();
}
