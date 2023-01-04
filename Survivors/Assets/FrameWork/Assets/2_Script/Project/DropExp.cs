using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpSubject : Subject
{
    private DropExpRecord m_record;
    public DropExpRecord getRecord { get { return m_record; } }

    public void SetRecord(DropExpRecord _record)
    {
        m_record = _record;
    }

    protected override void Notify()
    {
        for (int i = 0; i < m_observerList.Count; ++i)
        {
            IObserver _observer = m_observerList[i];
            if (null == _observer)
                continue;
            _observer.Notify(this);
        }
    }
}

public class DropExp : MonoBase
{
    private DropExpRecord m_record;
    private bool isGet = false;


    public void Open(DropExpRecord _record)
    {
        base.Open();
        m_record = _record;
        isGet = false;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (InGameMgr.Instance.ExistsHero(this.transform.position, 2f))
        {
            isGet = true;
        }
        if (isGet)
        {
            gameObject.transform.position = Vector3.MoveTowards(this.transform.position, InGameMgr.Instance.hero.transform.position, 0.1f);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == InGameMgr.Instance.hero.gameObject)
        {
            GameData_Stage.Instance.Add(eAssetType.Exp, m_record.IDropPercent);
            Close();
        }
    }
}
