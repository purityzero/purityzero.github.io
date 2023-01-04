using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command_OpenDialog : ICommand
{
    UIDialog m_dialog;
    string m_path;

    public Command_OpenDialog(string _path)
    {
        m_path = _path;
    }

    public void Execute()
    {
        m_dialog = UIMgr.Instance.popup.OpenDialog(m_path);
    }

    public void Update() { }
    public void Cancel() { }
    public bool IsFinished()
    {
        return Util.UIUtil.IsOpen(m_dialog) == false;
    }
}
