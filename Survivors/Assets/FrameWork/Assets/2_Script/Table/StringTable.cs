using UnityEngine;


[System.Serializable]
public class StringRecord : Record
{
    public string KOR;
    public string ENG;
    public string JPN;

    public string GetText(SystemLanguage _language)
    {
        switch (_language)
        {
            case SystemLanguage.Korean:
                return KOR;
            case SystemLanguage.English:
                return ENG;
            case SystemLanguage.Japanese:
                return JPN;            
            default:
                Debug.LogError("no code : " + _language);
                break;
        }

        return ENG;
    }
}


public class StringTable : Table<StringRecord>
{
    public static StringTable Instance { get { return TableMgr.Instance.GetTable<StringTable>(); } }

    public string GetText(int _index)
    {
        return GetText(_index, SystemLanguage.Korean);
    }

    public string GetText(int _index, SystemLanguage _language)
    {
        StringRecord _find = Get(_index);
        if (null == _find)
        {
            return string.Format("idx : {0}", _index);
        }

        return _find.GetText(_language);
    }
}
