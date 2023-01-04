using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FsmClass<T> where T : System.Enum
{
    protected Dictionary<T, FsmState<T>> m_stateList = new Dictionary<T, FsmState<T>>();
    protected FsmState<T> m_state;
    protected bool m_isStateChanging = false;
    public string strStateType;

    public FsmState<T> getState { get { return m_state; } }
    public T getStateType
    {
        get
        {
            if (null == m_state)
                return default(T);

            return m_state.stateType;
        }
    }

    public virtual void Init()
    {

    }

    public virtual void Clear()
    {
        m_stateList.Clear();
        m_state = null;
    }

    public virtual void AddFsm(FsmState<T> _state)
    {
        if( null == _state )
        {
            Debug.LogError("FsmClass::AddFsm()[ null == FsmState<T>");
            return;
        }

        if( true == m_stateList.ContainsKey(_state.stateType) )
        {
            Debug.LogError("FsmClass::AddFsm()[ have state : " + _state.stateType);
            return;
        }

        m_stateList.Add(_state.stateType, _state);
    }


    public virtual void SetState( T _stateType, FsmMsg _msg = null )
    {
        if( false == m_stateList.ContainsKey(_stateType))
        {
            Debug.LogError("FsmClass::SetState()[ no have state : " + _stateType);
            return;
        }

        if(m_isStateChanging == true )
        {
            Debug.LogError("FsmClass::SetState()[ change state : " + _stateType);
            return;
        }

        FsmState<T> _nextState = m_stateList[_stateType];
        if( _nextState == m_state )
        {
            Debug.LogWarning("FsmClass::SetState()[ same state : " + _stateType);
        }

        m_isStateChanging = true;

        if( null != m_state )
        {
            m_state.End();
        }

        strStateType = _nextState.stateType.ToString();
        m_state = _nextState;
        m_state.Enter(_msg);
        m_isStateChanging = false;
    }

    public virtual void SetMsg(FsmMsg _msg)
    {
        if (m_state == null)
            return;

        if (_msg == null)
            return;

        m_state.SetMsg(_msg);
    }

    public virtual void Update()
    {
        if (null == m_state)
            return;

        m_state.Update();
    }
}
