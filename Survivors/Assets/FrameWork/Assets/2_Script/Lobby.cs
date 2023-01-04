using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby : SceneLogic<Lobby>
{
    private void Awake()
    {
        m_sceneType = eSceneType.Lobby;
    }

    public void Start()
    {

    }

    public override void Init()
    {
        base.Init();
        TableMgr.Instance.Load();
        GameDataMgr.Instance.Load();

        UIMgr.Instance.popup.OpenDialog("Prefab/UILobby");
        if (GameDataMgr.Instance.IsStageData())
        {
            UIMgr.Instance.popup.OpenDialog("Prefab/UIContinueGame");
        }
    }


    protected override void Update()
    {
        base.Update();
    }
}
