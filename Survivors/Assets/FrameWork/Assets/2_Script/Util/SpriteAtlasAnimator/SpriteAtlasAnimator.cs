using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using System.Linq;

public sealed class SpriteKey
{
    public string key { get; private set; }
    public List<Sprite> sprAnimation { get; private set; }
    public float updateTime { get; private set; }
    public int count { get; private set; }
    public bool isLoop { get; private set; }

    public SpriteKey(string _key, int _cnt,  float _updateTime, bool _isLoop = false)
    {
        key = _key.ToLower();
        updateTime = _updateTime;
        count = _cnt;
        isLoop = _isLoop;
    }

    public void Set(List<Sprite> _sprList)
    {
        sprAnimation = _sprList;
    }
}

public sealed class SpriteAtlasState<T> where T : Enum
{
    public SpriteKey key { get; private set; }
    private float timer;
    private int currentIdx;
    public T m_type { get; private set; }
    private Image mainImage;
    private SpriteRenderer mainRender;
    public bool isComplete;
    public bool isLoop => key.isLoop;
    public SpriteAtlasState(T _type, Image _spr, SpriteKey _key)
    {
        m_type = _type;
        mainImage = _spr;
        key = _key;
    }

    public SpriteAtlasState(T _type, SpriteRenderer _spr, SpriteKey _key)
    {
        m_type = _type;
        mainRender = _spr;
        key = _key;
    }

    public void Enter()
    {
        timer = 0;
        currentIdx = 0;
        isComplete = false;
    }

    public void Update()
    {
        if (isComplete)
            return;

        timer += Time.deltaTime;
        if (key.updateTime <= timer)
        {
            if(currentIdx >= key.sprAnimation.Count)
            {
                if(isLoop == false)
                {
                    isComplete = true;
                    return;
                }
                currentIdx = 0;
            }

            if (mainRender == null)
            {
                Util.UIUtil.SetIcon(mainImage, key.sprAnimation[currentIdx]);
            }
            else
            {
                mainRender.sprite = key.sprAnimation[currentIdx];
            }

            ++currentIdx;
            timer = 0;
        }
    }
}

public sealed class SpriteAtlasAnimator<T> where T : Enum
{
    public Image image;
    public SpriteRenderer imageRenderer;
    private SpriteAtlas m_Atlas;

    private int idleidx = 0;    // idle이 없으면 default 값으로 0번의 enum으로 설정함.
    private SpriteAtlasState<T> m_currentState;

    private Dictionary<T, SpriteAtlasState<T>> m_DicState = new Dictionary<T, SpriteAtlasState<T>>();


    public SpriteAtlasAnimator(Image _mainSpr, Dictionary<T, SpriteKey> _key, string _atlasPath)
    {
        if (_key.Count <= 0)
            return;

        m_Atlas = Util.ResUtil.Load<SpriteAtlas>(_atlasPath);

        if (m_Atlas == null)
            return;

        image = _mainSpr;
        var _val = Enum.GetValues(typeof(T)).GetEnumerator();
        while (_val.MoveNext()) // Enum값 돌림
        {
            if (_key.ContainsKey((T)_val.Current)) // _key cotainsKey 이면
            {
                if (string.Compare("idle", _val.Current.ToString(), true) == 0)
                {
                    idleidx = (int)(_val.Current);
                }


                List<Sprite> _tmpList = new List<Sprite>(); // atlas 에 있는 sprite 받아오는 List 설정
                for (int i = 0; i < _key[(T)_val.Current].count; i++)
                {
                    _tmpList.Add(m_Atlas.GetSprite(_key[(T)_val.Current].key + $"_{i}"));
                }
                _key[(T)_val.Current].Set(_tmpList);
                var _state = new SpriteAtlasState<T>((T)_val.Current, image, _key[(T)_val.Current]);
                m_DicState.Add((T)_val.Current, _state);
            }
        }
    }

    public SpriteAtlasAnimator(SpriteRenderer _mainSpr, Dictionary<T, SpriteKey> _key, string _atlasPath)
    {
        if (_key.Count <= 0)
            return;

        m_Atlas = Util.ResUtil.Load<SpriteAtlas>(_atlasPath);

        if (m_Atlas == null)
            return;

        imageRenderer = _mainSpr;
        var _val = Enum.GetValues(typeof(T)).GetEnumerator();
        while (_val.MoveNext()) // Enum값 돌림
        {
            if (_key.ContainsKey((T)_val.Current)) // _key cotainsKey 이면
            {
                if (_val.Current.ToString().ToLower().Contains("idle"))
                {
                    idleidx = (int)(_val.Current);
                }

                List<Sprite> _tmpList = new List<Sprite>(); // atlas 에 있는 sprite 받아오는 List 설정
                for (int i = 0; i < _key[(T)_val.Current].count; i++)
                {
                    _tmpList.Add(m_Atlas.GetSprite(_key[(T)_val.Current].key + $"_{i}"));
                }
                _key[(T)_val.Current].Set(_tmpList);
                var _state = new SpriteAtlasState<T>((T)_val.Current, imageRenderer, _key[(T)_val.Current]);
                m_DicState.Add((T)_val.Current, _state);
            }
        }
    }

    public void AddState(T _type, SpriteKey _key)
    {
        if (m_DicState.ContainsKey(_type))
            return;

        List<Sprite> _tmpList = new List<Sprite>();
        for (int i = 0; i < _key.count; ++i)
        {
            _tmpList.Add(m_Atlas.GetSprite(_key.key + $"_{i}"));
        }
        _key.Set(_tmpList);
        SpriteAtlasState<T> _state;

        if (imageRenderer == null)
        {
             _state = new SpriteAtlasState<T>(_type, image, _key);
        }
        else
        {
            _state = new SpriteAtlasState<T>(_type, imageRenderer, _key);
        }
        m_DicState.Add(_type, _state);
    }


    public void SetState(T _state)
    {
        var _find = m_DicState[_state];
        if (_find == null)
        {
            Debug.LogError($"no find key value ! : {_state}");
            return;
        }
        m_currentState = _find;
        m_currentState.Enter();
        Debug.Log("current State : " + m_currentState.m_type.ToString());
    }

    public void Update()
    {
        if (m_currentState == null)
        {
            m_currentState = m_DicState[(T)Enum.ToObject(typeof(T), idleidx)];
        }

        m_currentState.Update();
        if(m_currentState.isComplete)
        {
            SetState((T)Enum.ToObject(typeof(T), idleidx)); // Loop가 아니면 항상 idle state로 가게 변경.
        }
    }

}
