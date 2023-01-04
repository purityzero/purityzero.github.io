using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLogic<T> : MonoBehaviour where T : SceneLogic<T>
{
    protected UIAssetList m_assetList;

    private static T m_instance;
    public static T Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<T>();
            }
            return m_instance;
        }
    } 

    protected eSceneType m_sceneType;
    public eSceneType getSceneType { get { return m_sceneType; } }
    public virtual void Init()
    {
    }


    protected virtual void Update()
    {
        GameDataMgr.Instance.UpdateLogic();
    }
}
