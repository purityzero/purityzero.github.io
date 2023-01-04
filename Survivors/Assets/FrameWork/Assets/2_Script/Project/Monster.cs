using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum eMonsterState
{
    IDLE,
    MOVE,
    ATTACK,
    ABNORMAL,
    DIE,
}

public class Monster : MonoBase
{
    public GameObject Obj;
    public long HP;
    public Slider HpBar;
    private long hp = 0;
    public long atk = 30;
    public float speed;
    private MonsterRecord m_record;
    private Action<MonsterRecord, Vector3> m_closeAction;
    private float m_TriggerTime = 1f;
    private float m_time = 0f;
    public Image img;
    public SpriteAnimation m_animation = new SpriteAnimation();

    public void Open(MonsterRecord _record, Action<MonsterRecord, Vector3> _closeAction)
    {
        base.Open();
        m_record = _record;
        HP = m_record.IHp;
        hp = HP;
        atk = m_record.IAtk;
        speed = m_record.fspeed;
        m_animation.init(img, m_record.sImagePath);

        HpBar.value = (float)hp / HP;
        m_closeAction = _closeAction;
    }

    public override void Close()
    {
        base.Close();
        GameData_Stage.Instance.Add(eAssetType.Money, m_record.IMoney);
        GameData_Stage.Instance.Add(eAssetType.Kill, 1);

        m_closeAction?.Invoke(m_record, this.transform.localPosition);
    }

    public void Damage(long _damage)
    {
        hp = hp - _damage;
        HpBar.value = (float)hp / HP;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (hp <= 0)
        {
            Close();
        }

        if (InGameMgr.Instance.hero == null)
            return;

        Vector2 direction = new Vector2(
            transform.position.x - InGameMgr.Instance.hero.transform.position.x,
            transform.position.y - InGameMgr.Instance.hero.transform.position.y);


        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, 3 * Time.deltaTime);
        Obj.transform.rotation = rotation;


        gameObject.transform.position = Vector3.MoveTowards(this.transform.position, InGameMgr.Instance.hero.transform.position, speed * 0.001f);


        m_animation.UpdateLogic();

        Vector3 _tempVec = InGameMgr.Instance.hero.transform.position - transform.position;
        InGameMgr.Instance.SetmaxDistance(Mathf.Abs(_tempVec.magnitude));
        if (Mathf.Abs(_tempVec.magnitude) >= 10f)
        {
            base.Close();
        }


    }
}
