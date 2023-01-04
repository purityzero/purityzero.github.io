using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl_TargetMove : CameraControl
{
    public Transform target;
    public float minHeight = 3f;
    public float wallMoveSpeed = 10f;

    public float dampTime = 0.2f;
    private Vector3 m_MoveVelocity = Vector3.zero;

    public void SetTarget( Transform _target )
    {
        target = _target;
        if( null != target)
            transform.position = target.position;
    }

    

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    
        if (null == target)
            return;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            target.position,
            ref m_MoveVelocity, dampTime);
    }
}