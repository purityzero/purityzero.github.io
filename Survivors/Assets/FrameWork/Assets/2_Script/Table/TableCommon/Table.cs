using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table<T> : ITable where T : Record, new ()
{
    public class CompareRecord : IComparer<T>
    {
        public int Compare(T x, T y) { return x.id.CompareTo(y.id); }
    }

    public List<T> list = new List<T>();
    T m_search = new T();
    CompareRecord m_compare = new CompareRecord();

    public virtual void Sort()
    {
        list.Sort(m_compare);
    }

    public T Get(int _id, bool _isShowLog = true)
    {
        m_search.id = _id;
        int _searchIndex = list.BinarySearch(m_search, m_compare);
        if( _searchIndex < 0 )
        {
            if( _isShowLog == true )
            {
                Debug.LogError(typeof(T).ToString() + " : " + _id);
            }
            return null;
        }
        return list[_searchIndex];
    }

    public bool IsHas(int _id)
    {
        m_search.id = _id;
        return list.BinarySearch(m_search, m_compare) >= 0;

    }

    public virtual void Save(string _path)
    {
        string _data = Util.JsonHelper.GetString<T>(list);
        string _data_zip = Util.ZipUtil.Zip(_data, 9);
        Util.FileUtil.Save(_path, _data_zip);
    }

    public virtual void Load(string _path)
    {        
        string _data = Util.FileUtil.LoadRes(_path);
        string _data_unzip = Util.ZipUtil.UnZip(_data);
        list = Util.JsonHelper.GetList<T>(_data_unzip);
        if (null == list)
            list = new List<T>();

        Sort();
    }
}
