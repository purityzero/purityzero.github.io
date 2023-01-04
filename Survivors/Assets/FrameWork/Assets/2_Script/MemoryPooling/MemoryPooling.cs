using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryPooling<T> where T : MonoBase
{
    public delegate T AddComponentDelegate(GameObject _object);
    protected List<MemoryPoolingItem<T>> m_activeList = new List<MemoryPoolingItem<T>>();
    protected List<MemoryPoolingItem<T>> m_hideList = new List<MemoryPoolingItem<T>>();
    protected Transform m_attach;
    protected int m_maxCount = 100;

    public List<MemoryPoolingItem<T>> activeList { get { return m_activeList; } }
    public List<MemoryPoolingItem<T>> hideList { get { return m_hideList; } }

    public MemoryPooling(Transform _attach, int _maxCount)
    {
        m_attach = _attach;
        m_maxCount = _maxCount;
    }

    void DeleteList(List<MemoryPoolingItem<T>> _list)
    {
        if (null == _list)
            return;
        for(int i=0; i<_list.Count; ++i)
        {
            if (null == _list[i])
                continue;
            if (null == _list[i].item)
                continue;
            GameObject.Destroy(_list[i].item.gameObject);
        }

        _list.Clear();
    }

    public void Clear()
    {
        DeleteList(m_activeList);
        DeleteList(m_hideList);
    }

    public void Close()
    {
        for( int i=0;i<m_activeList.Count; ++i )
        {
            if (null == m_activeList[i])
                continue;
            if (null == m_activeList[i].item)
                continue;
            m_activeList[i].item.Close();
            m_hideList.Add(m_activeList[i]);
        }
        m_activeList.Clear();
    }

    public MemoryPoolingItem<T> GetItem(string _path, AddComponentDelegate _addComponentDelegate)
    {
        int _resKey = _path.GetHashCode();
        MemoryPoolingItem<T> _findItem = m_hideList.Find(item => item == null ? false : item.resKey == _resKey);

        if( null == _findItem )
        {
            GameObject _loadGameObject = Util.ResUtil.Create(_path, m_attach);
            if (null == _loadGameObject)
                return null;

            T _component = _loadGameObject.GetComponent<T>();
            if( null == _component)
            {
                if( null != _addComponentDelegate)
                {
                    _component = _addComponentDelegate(_loadGameObject);
                }
            }

            if( null == _component )
            {
                Debug.LogError("MemoryPooling::GetItem()[ null == Component ] : " + typeof(T).Name);
                return null;
            }

            //_component.Open();
            _findItem = new MemoryPoolingItem<T>(_resKey, _component);
            m_activeList.Add(_findItem);
        }
        else
        {
            m_hideList.Remove(_findItem);
            m_activeList.Add(_findItem);
        }

        return _findItem;
    }

    public T Get(string _path, AddComponentDelegate _addComponentDelegate = null )
    {
        MemoryPoolingItem<T> _getItem = GetItem(_path, _addComponentDelegate);
        if (null == _getItem)
            return null;

        return _getItem.item;
    }

    public void UpdateLogic()
    {
        int _index = 0;
        while(m_activeList.Count > _index)
        {
            MemoryPoolingItem<T> _activeItem = m_activeList[_index];
            if( null == _activeItem.item )
            {
                m_activeList.RemoveAt(_index);
                continue;
            }

            if( _activeItem.item.isOpen == false )
            {
                m_activeList.RemoveAt(_index);
                if (m_maxCount <= m_hideList.Count)
                {
                    GameObject.Destroy(_activeItem.item.gameObject);
                }
                else
                {
                    m_hideList.Add(_activeItem);
                }
                continue;
            }

            _activeItem.item.UpdateLogic();
            ++_index;
        }
    }
}
