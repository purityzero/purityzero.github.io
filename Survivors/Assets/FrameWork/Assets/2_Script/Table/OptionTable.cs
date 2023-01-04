using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OptionRecord : Record
{
    public float optionValue;
    public float minValue;
    public float maxValue;
}


public class OptionTable : Table<OptionRecord>
{
    public static OptionTable Instance { get { return TableMgr.Instance.GetTable<OptionTable>(); } }

    public int GetInt(int _id)
    {
        OptionRecord _record = Get(_id);
        if (null == _record)
            return 0;

        return (int)_record.optionValue;
    }

    public int GetInt_Min(int _id)
    {
        OptionRecord _record = Get(_id);
        if (null == _record)
            return 0;

        return (int)_record.minValue;
    }

    public int GetInt_Max(int _id)
    {
        OptionRecord _record = Get(_id);
        if (null == _record)
            return 0;

        return (int)_record.maxValue;
    }


    public float GetFloat(int _id)
    {
        OptionRecord _record = Get(_id);
        if (null == _record)
            return 0f;

        return _record.optionValue;
    }

    public float GetFloat_Min(int _id)
    {
        OptionRecord _record = Get(_id);
        if (null == _record)
            return 0f;

        return _record.minValue;
    }

    public float GetFloat_Max(int _id)
    {
        OptionRecord _record = Get(_id);
        if (null == _record)
            return 0f;

        return _record.maxValue;
    }

    

}
