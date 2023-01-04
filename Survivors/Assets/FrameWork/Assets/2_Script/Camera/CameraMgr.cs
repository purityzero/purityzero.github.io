using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMgr : MonoSingletonManager<CameraMgr>
{
    public CameraControl cameraControl;

    public void Init()
    {

    }

    public void SetCamera(CameraControl _camera)
    {
        cameraControl = _camera;
    }

    public T GetCamera<T>() where T : CameraControl
    {
        return cameraControl as T;
    }

    public void UpdateLogic()
    {
        if (null != cameraControl)
            cameraControl.UpdateLogic();
    }
}
