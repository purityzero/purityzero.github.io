using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData_Option : GameData
{
    public static GameData_Option Instance { get { return GameDataMgr.Instance.GetData<GameData_Option>(); } }
   
    [SerializeField] private float m_sound_volume_bgm = 1f;
    [SerializeField] private float m_sound_volume_fx = 1f;
    [SerializeField] private bool m_sound_mute_bgm = false;
    [SerializeField] private bool m_sound_mute_fx = false;

    [SerializeField] private SystemLanguage m_language = SystemLanguage.Unknown;
    
    public float sound_volume_bgm { get { return m_sound_volume_bgm; } }
    public float sound_volume_fx { get { return m_sound_volume_fx; } }
    public bool sound_mute_bgm { get { return m_sound_mute_bgm; } }
    public bool sound_mute_fx { get { return m_sound_mute_fx; } }
    public SystemLanguage getLanguage
    {
        get
        {
            if (m_language == SystemLanguage.Unknown)
                return Application.systemLanguage;

            return m_language;
        }
    }

    public override void Init()
    {
        base.Init();
        m_sound_volume_bgm = 1f;
        m_sound_volume_fx = 1f;
        m_sound_mute_bgm = false;
        m_sound_mute_fx = false;
    }

    public override void Logout()
    {
        m_sound_volume_bgm = 1f;
        m_sound_volume_fx = 1f;
        m_sound_mute_bgm = false;
        m_sound_mute_fx = false;
    } 

    public void SetSound_voulme_bgm(float _value)
    {
        m_sound_volume_bgm = Mathf.Clamp( _value, 0f, 1f);
        AllChange();
    }
    public void SetSound_voulme_fx(float _value)
    {
        m_sound_volume_fx = Mathf.Clamp(_value, 0f, 1f);
        AllChange();
    }
    public void SetSound_mute_bgm(bool _mute)
    {
        m_sound_mute_bgm = _mute;
        AllChange();
    }
    public void SetSound_mute_fx(bool _mute)
    {
        m_sound_mute_fx = _mute;
        AllChange();
    }
    public void SetLanguage(SystemLanguage _language)
    {
        m_language = _language;
        AllChange();
    }
}
