using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eSOUND_TYPE 
{
    BGM,
    FX,
}


public class Sound : MonoBehaviour
{
    static public bool IsOpen(Sound _sound)
    {
        if (null == _sound)
            return false;

        return _sound.isOpen;
    }

    eSOUND_TYPE m_soundType;
    AudioSource m_source; 
    int m_resIdx;


    public int resIdx { get { return m_resIdx; } }
    public eSOUND_TYPE soundType { get { return m_soundType; } }
    public bool isOpen 
    { 
        get 
        {
            if (gameObject.activeSelf == false)
                return false;

            if (m_source.isPlaying == false)
                return false;

            return true;
        } 
    }
    
    public void Init()
    {
        m_source = gameObject.AddComponent<AudioSource>();
    }

    public void Play(string _path, float _volume, bool _loop, float _delayTime = 0f)
    {
        gameObject.SetActive(true);

        m_resIdx = _path.ToLower().GetHashCode();
        m_source.clip = Util.ResUtil.Load<AudioClip>(_path);     
        m_source.PlayDelayed(_delayTime);
        m_source.volume = _volume;
        m_source.loop = _loop;
        m_source.Play();      
    }
 
    public void Stop()
    {
        m_source.Stop();
        Close();
    }

    public void SetVolume(float _volume)
    {
        if (m_source.isPlaying == false)
            return;

        m_source.volume = _volume;
    }

    public void UpdateLogic()
    {
        if (m_source.isPlaying == true)
            return;

        Close();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
