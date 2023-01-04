using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : SceneLogic<Intro>
{
    private FlowCommand m_command = new FlowCommand();
    private void Awake()
    {
        m_sceneType = eSceneType.Intro;
    }

    public override void Init()
    {
        base.Init();
        Application.targetFrameRate = 60;
        UIMgr.Instance.popup.OpenDialog("Prefab/UIIntro");
        m_command.Add(new Command_DeltaTime(3f, () => { SceneMgr.Instance.LoadScene(eSceneType.Lobby, 4); }));
    }

    protected override void Update()
    {
        m_command.Update();
    }

}
