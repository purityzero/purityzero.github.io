using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDialogManagement : MonoBehaviour
{
    MemoryPooling<UIDialog> m_pooling;

    private void Start()
    {
    }

    public void Init()
    {
        m_pooling = new MemoryPooling<UIDialog>(transform, 100);
    }

    public void Close()
    {
        m_pooling.Close();
    }

    public void Clear()
    {
        m_pooling.Clear();
    }

    public int GetCount()
    {
        return m_pooling.activeList.Count;
    }

    public UIDialog OpenDialog(string _path)
    {
        UIDialog _dlg = m_pooling.Get(_path);
        if (null == _dlg)
            return null;

        _dlg.Open();

        return _dlg;
    }

    public T GetDialog<T>(string _path) where T : UIDialog
    {
        return m_pooling.Get(_path) as T;
    }

    public void UpdateLogic()
    {
        m_pooling.UpdateLogic();
    }
}
