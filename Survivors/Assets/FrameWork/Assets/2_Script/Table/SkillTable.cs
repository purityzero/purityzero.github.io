using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eSkillOption
{
    None,
    Bouncing,
    Range,
    Penetrate,
}

[System.Serializable]
public class SkillRecord : Record
{
    public eSkillDefine eType;
    public int ILevel;
    public long LDamage;
    public float FCoolTime;
    public eSkillOption SkillOption;
    public float fOption;
    public string sPath;
    public string sImagePath;
    public string sInfo;
    public float fPercent;

    public string SkillOptionText()
    {
        switch (SkillOption)
        {
            case eSkillOption.Bouncing:
                return "튕김";
            case eSkillOption.Penetrate:
                return "관통";
            case eSkillOption.Range:
                return "범위";

            default:
                return "없음";
        }

    }

}


public class SkillTable : Table<SkillRecord>
{
    public static SkillTable Instance { get { return TableMgr.Instance.GetTable<SkillTable>(); } }

    public List<SkillRecord> GetRecordList(eSkillDefine _type)
    {
        return list.FindAll(x => x.eType == _type);
    }

    public SkillRecord GetRecord(eSkillDefine _type, int _level)
    {
        return list.Find(x => x.eType == _type && x.ILevel == _level);
    }

    public List<SkillRecord> GetLevelUpSkillList()
    {
        List<SkillRecord> _PeekList = new List<SkillRecord>();
        List<SkillRecord> _weightPeekList = new List<SkillRecord>();
        for (int i = 0; i < System.Enum.GetValues(typeof(eSkillDefine)).Length; i++)
        {
            if ((eSkillDefine)i == eSkillDefine.Dummy)
                continue;

            bool _isHave = GameData_Stage.Instance.skillList.Exists(x => x.eSkillType == (eSkillDefine)i);
            SkillRecord _peekerItem = null;
            if (_isHave)
            {
                var _findData = GameData_Stage.Instance.skillList.Find(x => x.eSkillType == (eSkillDefine)i);
                if (_findData.lv == 10)
                    continue;

                _peekerItem = GetRecord(_findData.eSkillType, _findData.lv + 1);
            }
            else
            {
                _peekerItem = GetRecord((eSkillDefine)i, 1);
            }
            _PeekList.Add(_peekerItem);
        }

        for (int i = 0; i < 3; i++)
        {
            if (_PeekList.Count <= 0)
                break;

            SkillRecord _peek = WeightPeek(_PeekList);
            _PeekList.Remove(_peek);
            _weightPeekList.Add(_peek);
        }


        return _weightPeekList;

    }

    private SkillRecord WeightPeek(List<SkillRecord> _peekerList)
    {
        float total = 0;

        for (int i = 0; i < _peekerList.Count; i++)
        {
            total += _peekerList[i].fPercent;
        }
        

        float randomPoint = Random.value * total;

        for (int i = 0; i < _peekerList.Count; i++)
        {
            if (randomPoint < _peekerList[i].fPercent)
            {
                return _peekerList[i];
            }
            else
            {
                randomPoint -= _peekerList[i].fPercent;
            }
        }
        return _peekerList[_peekerList.Count - 1];
    }
}
