using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassSingletonManager<T> where T : class, new()
{
    private static T m_instance;
    private static object m_lock = new object();

    public static T Instance
    {
        get
        {
            lock (m_lock)
            {
                if (null == m_instance)
                {
                    m_instance = new T();
                }
                return m_instance;
            }
        }
    }
}
