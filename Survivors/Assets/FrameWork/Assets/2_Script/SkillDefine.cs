using System.Collections.Generic;
using UnityEngine;

public enum eSkillDefine
{
    Gun,
    Stink,
    Lego,
    Dummy,
}

public class SkillMgr
{
    List<Skill> elements = new List<Skill>();
    public Transform m_attach;

    public SkillMgr(Transform _attach, SkillRecord _skillRecord)
    {
        m_attach = _attach;
        if (_skillRecord != null)
        {
            Add(_skillRecord);
        }
    }

    public void SetShow(bool _isActive)
    {
        if (_isActive)
        {
            elements.ForEach(x => x.Open());
        }
        else
        {
            elements.ForEach(x => x.Close());
        }
    }

    public void Add(SkillRecord _skillRecord)
    {
        Debug.Log($"Skill Add : {_skillRecord.eType}, {_skillRecord.ILevel}");
        var _exists = elements.Exists(_data => _data.skillRecord.eType == _skillRecord.eType);
        if (_exists == false)
        {
            elements.Add(CraeteSkill(_skillRecord.eType, _skillRecord.ILevel));
            GameData_Stage.Instance.AddSkill(_skillRecord.eType, _skillRecord.ILevel);
            return;
        }

        var _findItem = elements.Find(_data => _data.skillRecord.eType == _skillRecord.eType);
        GameData_Stage.Instance.AddSkill(_skillRecord.eType, _skillRecord.ILevel);
        _findItem.ReSetRecord(_skillRecord);


    }

    // MonoBase Create
    private Skill CraeteSkill(eSkillDefine _skill, int _lv = 1)
    {
        var _record = SkillTable.Instance.GetRecord(_skill, _lv);
        switch (_skill)
        {
            case eSkillDefine.Gun:
                var _skill_Gun = Util.ResUtil.Create<Skill_Gun>(_record.sPath, m_attach);
                _skill_Gun.Open(_record);
                return _skill_Gun;

            case eSkillDefine.Stink:
                var _stink = Util.ResUtil.Create<Skill_Stink>(_record.sPath, m_attach);
                _stink.Open(_record);
                return _stink;
            case eSkillDefine.Lego:
                var _lego = Util.ResUtil.Create<Skill_Lego>(_record.sPath, m_attach);
                _lego.Open(_record);
                return _lego;

            case eSkillDefine.Dummy:
                var _dummy = Util.ResUtil.Create<Skill>(_record.sPath, m_attach);
                _dummy.Open(_record);
                return _dummy;

            default:
                return null;
        }
    }

    public void UpDateLogic()
    {

        if (InGameMgr.Instance.getState() == eInGameState.Levelup || InGameMgr.Instance.getState() == eInGameState.Popup || InGameMgr.Instance.getState() == eInGameState.Ready)
            return;

        elements.ForEach(x => x.UpdateLogic());
    }
}
