using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMgr : MonoSingletonManager<UIMgr>
{
    public UIDialogManagement popup;
    public UIDialogManagement Asset;

    private void Start()
    {
    }

    public void Clear()
    {
        popup.Clear();
        Asset.Clear();
    }

    public void Init()
    {
        popup.Init();
        Asset.Init();
    }

    private void Update()
    {
        popup.UpdateLogic();
        Asset.UpdateLogic();
    }
}
