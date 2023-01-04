using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eLOGIN_TYPE 
{ 
    NONE,
    GUEST,
    GOOGLE,
    APPLE,
    FACEBOOK,
}


[System.Serializable]
public class GameData_Local : GameData
{
    public static GameData_Local Instance { get { return GameDataMgr.Instance.GetData<GameData_Local>(); } }
    

    [SerializeField] bool m_isAgree;
    [SerializeField] eLOGIN_TYPE m_loginType;
    [SerializeField] string m_userIdx;
    [SerializeField] bool m_isCheckLoad = false;

    public bool isCheckLoad { get { return m_isCheckLoad; } }
    public bool isAgree { get { return m_isAgree; } }
    public eLOGIN_TYPE loginType { get { return m_loginType; } }

    public string userIdx { get { return m_userIdx; } }

    public void SetCheckLoad(bool _value )
    {
        m_isCheckLoad = _value;       
    }
    public void SetLogin( eLOGIN_TYPE _loginType, string _userIdx )
    {
        m_userIdx = _userIdx;
        m_loginType = _loginType;       
    }

    public void SetAgree( bool _isAgree )
    {
        m_isAgree = _isAgree;
    }
    //public override void Init()
    //{
    //    base.Init();
    //    m_loginType = eLOGIN_TYPE.NONE;
    //    m_isAgree = false;
    //}
}
