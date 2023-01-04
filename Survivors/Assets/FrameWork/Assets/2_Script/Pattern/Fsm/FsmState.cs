using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FsmState<T> where T : System.Enum
{
    protected T m_stateType;

    public T stateType { get { return m_stateType; } }

    public FsmState(T _stateType)
    {
        m_stateType = _stateType;
    }

    public virtual void Enter(FsmMsg _msg)
    {

    }

    public virtual void Update()
    {

    }

    public virtual void End()
    {

    }

    public virtual void SetMsg(FsmMsg _msg)
    {

    }
}
