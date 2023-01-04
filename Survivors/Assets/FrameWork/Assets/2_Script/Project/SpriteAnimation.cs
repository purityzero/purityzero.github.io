using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SpriteAnimation
{
    Image m_image;
    List<string> m_path = new List<string>();
    List<Sprite> m_spr = new List<Sprite>();
    float m_time;

    float time;
    int m_idx;
    public SpriteAnimation()
    {
    }

    public void init(Image _image, List<string> _path, float _time = 0.2f)
    {
        m_image = _image;
        m_path = _path;
        m_time = _time;
        time = 0;
        m_idx = 0;
        if (_image == null)
        {
            Debug.LogError("m_image == null");
            return;
        }

        if (_path == null || _path.Count <= 0)
        {
            Debug.LogError("m_path == null");
            return;
        }

        Util.UIUtil.SetIcon(m_image, m_path[m_idx]);
    }

    public void init(Image _image, List<Sprite> _spr, float _time = 0.2f)
    {
        m_image = _image;
        m_spr = _spr;
        m_time = _time;
        time = 0;
        m_idx = 0;
        if (_image == null)
        {
            Debug.LogError("m_image == null");
            return;
        }

        if (_spr == null || _spr.Count <= 0)
        {
            Debug.LogError("m_path == null");
            return;
        }

        Util.UIUtil.SetIcon(m_image, _spr[m_idx]);
    }

    private void UpdateSpr()
    {
        if (m_spr.Count <= 0)
            return;

        if (m_idx >= m_spr.Count)
        {
            m_idx = 0;
        }

        Util.UIUtil.SetIcon(m_image, m_spr[m_idx]);
    }

    private void UpdatePath()
    {
        if (m_path.Count <= 0)
            return;

        if (m_idx >= m_path.Count)
        {
            m_idx = 0;
        }
        Util.UIUtil.SetIcon(m_image, m_path[m_idx]);
    }


    public void UpdateLogic()
    {

        time += Time.deltaTime;
        if (time >= m_time)
        {
            time = 0;
            ++m_idx;

            if (m_path.Count > 0)
                UpdatePath();
            else
                UpdateSpr();
        }
    }
}
