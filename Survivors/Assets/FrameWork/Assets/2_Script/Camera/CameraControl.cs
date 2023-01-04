using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{   
    public Transform content;
    public Transform shake;
    
    protected Vector3 m_shakePower = Vector3.zero;
    protected bool m_isMovable = false;    
   
    public virtual void UpdateLogic()
    {
        ShakeUpdate();        
    }

    public virtual void LateUpdateLogic()
    {

    }

    public virtual void ShakeUpdate()
    {
        if (null == shake)
            return;

        if (m_shakePower == Vector3.zero)
            return;

        Vector3 _power = m_shakePower = Vector3.Lerp(m_shakePower, Vector3.zero, Time.deltaTime * 5f);
        _power.x = Mathf.Cos(Time.time * 90f) * _power.x;
        _power.y = Mathf.Cos(Time.time * 90f) * _power.y;
        _power.z = Mathf.Cos(Time.time * 90f) * _power.z;
        shake.localPosition = _power;            
    }   

    public virtual void Shake(Vector3 power)
    {
        m_shakePower = -power;
    }

    public virtual void HoldCamMove(bool _isMove)
    {
        m_isMovable = _isMove;
    }

    public bool IsMovalbe
    {
        get
        {
            return m_isMovable;
        }
    }
}
