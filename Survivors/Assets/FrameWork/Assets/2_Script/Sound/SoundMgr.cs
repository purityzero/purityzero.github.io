using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * SoundMgr
 * 2022-07-26
 * 사운드를 관리하는 클래스.
 */
public class SoundMgr : MonoSingletonManager<SoundMgr>, IObserver
{    
    List<SoundVolume> m_soundVolumeList = new List<SoundVolume>();
    List<Sound> m_soundList = new List<Sound>();

    float m_noiseTime = 0f;
    float m_maxNoiseTime = 0.3f;

    bool m_isApplicationQuit = false;
    void OnApplicationQuit()
    {
        m_isApplicationQuit = true;
    }

    void OnEnable()
    {
        GameData_Option.Instance.Attach(this);
    }

    void OnDisable()
    {
        if (true == m_isApplicationQuit)
            return;

        GameData_Option.Instance.Detach(this);
    }

    public void Notify(Subject _subject)
    {
        ResetSoundVlume();
    }

    public bool IsMute(eSOUND_TYPE _soundType)
    {
        SoundVolume _soundVolume = m_soundVolumeList.Find(item => item.sound_type == _soundType);
        if( null == _soundVolume )
        {
            return false;
        }

        return _soundVolume.mute;
    }

    public float GetVolume(eSOUND_TYPE _soundType)
    {
        SoundVolume _soundVolume = m_soundVolumeList.Find(item => item.sound_type == _soundType);
        if (null == _soundVolume)
        {
            return 1f;
        }

        return _soundVolume.volume;
    }   

    public void SetVolume( eSOUND_TYPE _soundType, bool _mute, float _volume)
    {
        SoundVolume _soundVolume = m_soundVolumeList.Find(item => item.sound_type == _soundType);
        if (null == _soundVolume)
        {
            m_soundVolumeList.Add(_soundVolume = new SoundVolume(_soundType, _mute, _volume));
        }
        else
        {
            _soundVolume.SetMute(_mute);
            _soundVolume.SetVolume(_volume);
        }        

        for ( int i=0; i<m_soundList.Count; ++i)
        {
            Sound _sound = m_soundList[i];

            if (Sound.IsOpen(_sound) == false)
                continue;

            if (_sound.soundType != _soundType)
                continue;

            _sound.SetVolume(_soundVolume.mute == true ? 0f : _soundVolume.volume);
        }
    }  

    public Sound Play( eSOUND_TYPE _soundType, string _path )
    {
        Sound _sound = m_soundList.Find(item => item.isOpen == false && item.soundType == _soundType);
       
        if (null == _sound)
        {
            GameObject _newSoundObject = new GameObject(_soundType.ToString());
            _newSoundObject.transform.SetParent(transform);
            _sound = _newSoundObject.AddComponent<Sound>();
            _sound.Init();
            m_soundList.Add(_sound);
        }
      
        _sound.Play(_path, IsMute(_soundType)==true?0f:GetVolume(_soundType), _soundType == eSOUND_TYPE.BGM);

        return _sound;
    }

    public Sound PlayFx(string _path)
    {
        return Play(eSOUND_TYPE.FX, _path);
    }

    public Sound PlayNoise(string _path)
    {
        if (Time.realtimeSinceStartup - m_noiseTime < m_maxNoiseTime)
            return null;

        m_noiseTime = Time.realtimeSinceStartup;
        return Play( eSOUND_TYPE.FX, _path);
    }

    public void StopAll(eSOUND_TYPE _soundType)
    {
        for (int i = 0; i < m_soundList.Count; ++i)
        {
            Sound _sound = m_soundList[i];
            if (Sound.IsOpen(_sound) == false)
                continue;
            if (_sound.soundType != _soundType)
                continue;

            _sound.Stop();
        }
    }

    public void StopAll(string _path)
    {
        if (string.IsNullOrWhiteSpace(_path) == true)
            return;

        int _resIdx = _path.ToLower().GetHashCode();
        for( int i=0;i<m_soundList.Count; ++i )
        {
            Sound _sound = m_soundList[i];
            if (Sound.IsOpen(_sound) == false)
                continue;
            if (_sound.resIdx != _resIdx)
                continue;

            _sound.Stop();
        }            
    }

    public void Init()
    {
        ResetSoundVlume();
    }

    public void ResetSoundVlume()
    {
        GameData_Option _option = GameData_Option.Instance;
        SetVolume(eSOUND_TYPE.BGM, _option.sound_mute_bgm, _option.sound_volume_bgm);
        SetVolume(eSOUND_TYPE.FX, _option.sound_mute_fx, _option.sound_volume_fx);
    }

    public void UpdateLogic()
    {
        for (int i = 0; i < m_soundList.Count; ++i)
        {
            Sound _sound = m_soundList[i];
            if (Sound.IsOpen(_sound) == false)
                continue;

            _sound.UpdateLogic();
        }
    }  
}
