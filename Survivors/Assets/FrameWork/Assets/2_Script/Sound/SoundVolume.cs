using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundVolume
{
    eSOUND_TYPE m_soundType;
    float m_volume = 1f;
    bool m_mute = false;

    public eSOUND_TYPE sound_type { get { return m_soundType; } }
    public float volume { get { return m_volume; } }
    public bool mute { get { return m_mute; } }

    public SoundVolume( eSOUND_TYPE _soundType, bool _mute, float _volume)
    {
        m_soundType = _soundType;
        m_mute = _mute;
        m_volume = _volume;
    }

    public void SetMute(bool _isMute)
    {
        m_mute = _isMute;
    }

    public void SetVolume(float _volume)
    {
        m_volume = _volume;
    }
}
