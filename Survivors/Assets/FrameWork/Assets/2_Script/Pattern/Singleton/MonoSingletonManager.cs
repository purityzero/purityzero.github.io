using UnityEngine;

[DisallowMultipleComponent] // DisallowMultipleComponent : MonoBehaviour가 GameObject에 두번 이상 추가되는것을 방지한다.
public abstract class MonoSingletonManager<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T m_instance;
    private static object m_lock = new object();

    public static T Instance
    {
        get
        {
            lock (m_lock) // 쓰레드 lock. 굳이 필요한지 모르겠음.
            {
                if (null == m_instance)
                {
                    T[] _finds = FindObjectsOfType<T>();//FindObjectsOfType : 활성화된 Type의 리스트를 가지고온다. 
                    if (_finds.Length > 0)
                    {
                        m_instance = _finds[0];
                        DontDestroyOnLoad(m_instance.gameObject); // DontDestroyOnLoad : A Scene에서 B Scene으로 넘어갈때 A Scene의 게임오브젝트를 제거 하는데 제거되지 않도록 하기 위한 함수.
                    }

                    if (_finds.Length > 1)
                    {
                        // 1개 이상일 경우 1개를 제외한 모두 제거한다.
                        for (int i = 1; i < _finds.Length; ++i)
                        {
                            Destroy(_finds[i].gameObject);
                        }
                        Debug.LogError("There is more than one " + typeof(T).Name + " in the Scene."); // 이런경우는있으면 안되기 때문에 에러로그를 표시한다.
                    }

                    if (null == m_instance)
                    {
                        GameObject _createGameObject = new GameObject(typeof(T).Name);
                        DontDestroyOnLoad(_createGameObject);
                        m_instance = _createGameObject.AddComponent<T>();
                    }
                }

                return m_instance;
            }
        }
    }
}