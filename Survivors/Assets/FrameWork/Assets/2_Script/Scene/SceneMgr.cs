using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum eSceneType
{
    Intro = 0,
    Lobby,
    Game
}


public class SceneMgr : MonoSingletonManager<SceneMgr>
{
    [SerializeField]
    UILoadingDialog m_LoadingDlg;
    AsyncOperation m_AsyncLoad;
    [SerializeField]
    eSceneType m_type = eSceneType.Intro;
    public AsyncOperation getAsyncLoad => m_AsyncLoad;
    private List<GameObject> listScene;
    private void Awake()
    {

    }
    private void Start()
    {
        UIMgr.Instance.Init();
        Init();
    }

    public void LoadScene(eSceneType _sceneType, float _waitTime = 0f)
    {
        m_type = _sceneType;
        StartCoroutine(LoadAsyncScene(_sceneType, _waitTime));
    }

    IEnumerator LoadAsyncScene(eSceneType _sceneType, float _waitTime = 0f)
    {
        m_LoadingDlg.FadeIn(null);
        yield return new WaitForSeconds(_waitTime);
        UIMgr.Instance.Clear();
        m_AsyncLoad = SceneManager.LoadSceneAsync((int)m_type, LoadSceneMode.Single);

        while (m_AsyncLoad.isDone == false)
        {
            yield return new WaitForSeconds(0.01f);
        }
        Init();
        m_LoadingDlg.FadeOut(null);
    }

    private void Init()
    {


        switch (m_type)
        {
            case eSceneType.Intro:
                Intro.Instance.Init();
                break;
            case eSceneType.Lobby:
                Lobby.Instance.Init();
                break;
            case eSceneType.Game:
                InGameMgr.Instance.Init();
                break;
        }

    }


    private void Update()
    {
        m_LoadingDlg.UpdateLogic();
    }
}
