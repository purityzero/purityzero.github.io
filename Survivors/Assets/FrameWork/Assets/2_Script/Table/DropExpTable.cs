using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DropExpRecord : Record
{
    public string sPath;
    public int IDropPercent;
}

public class DropExpTable : Table<DropExpRecord>
{
    public static DropExpTable Instance { get { return TableMgr.Instance.GetTable<DropExpTable>(); } }


    public DropExpRecord WeightRandom()
    {
        int _maxPercent = 0;
        for (int i = 0; i < list.Count; i++)
        {
            _maxPercent += list[i].IDropPercent;
        }

        int _randomPoint = (int)(Random.value * _maxPercent);

        for (int i = 0; i < list.Count; i++)
        {
            if(_randomPoint < list[i].IDropPercent)
            {
                return list[i];
            }
            else
            {
                _randomPoint -= list[i].IDropPercent;
            }
        }
        return null;
    }

    public DropExpRecord WeightRandom(List<int> _idList)
    {
        List<DropExpRecord> _listWeight = new List<DropExpRecord>();
        int _maxPercent = 0;
        int _randomPoint = 0;
        for (int i = 0; i< _idList.Count; i++)
        {
            _listWeight.Add(Get(_idList[i]));
        }

        for (int i = 0; i < _listWeight.Count; i++)
        {
            _maxPercent += _listWeight[i].IDropPercent;
        }

        _randomPoint = (int)(Random.value * _maxPercent);

        for (int i = 0; i < _listWeight.Count; i++)
        {
            if (_randomPoint < _listWeight[i].IDropPercent)
            {
                return _listWeight[i];
            }
            else
            {
                _randomPoint -= _listWeight[i].IDropPercent;
            }
        }
        return null;
    }
}
