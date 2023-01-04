using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBase
{
    private long m_damage;
    private float m_speed;
    private Vector3 dir;
    bool isDamage = false;
    private SkillRecord m_record;

    private float LifeTime = 2;
    private float timer = 0;
    private float m_optionCnt = 0;

    public void Open(SkillRecord _record, Vector3 _dir, float _speed, float _lifeTime)
    {
        m_record = _record;
        m_damage = m_record.LDamage;
        m_optionCnt = m_record.fOption;
        m_speed = _speed;
        _dir.z = 0;
        dir = -_dir;
        timer = 0;
        LifeTime = _lifeTime;
        isDamage = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDamage)
        {
            Destroy(this.gameObject);
            return;
        }

        Monster _monster = InGameMgr.Instance.FindMonster(collision.gameObject);
        if (_monster == null)
            return;

        isDamage = true;
        _monster.Damage(m_damage);
        Option();

    }

    private void Option()
    {
        if (m_optionCnt > 0)
            m_optionCnt -= 1;

        switch (m_record.SkillOption)
        {
            case eSkillOption.Penetrate:
                Penetrate();
                break;
            case eSkillOption.Range:
                Range();
                break;
            default:
                Destroy(this.gameObject);
                break;

        }
    }

    private void Range()
    {
        var _damagelist = InGameMgr.Instance.GetDamageMonsterList(this.transform, m_record.fOption * 0.01f);
        _damagelist.ForEach(x => x.Damage(m_damage));
    }

    private void Penetrate()
    {
        if (m_record.SkillOption == eSkillOption.Penetrate)
        {
            if (m_optionCnt > 0)
                m_optionCnt -= 1;
            else
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    private void Update()
    {
        if (InGameMgr.Instance.getState() != eInGameState.Idle && InGameMgr.Instance.getState() != eInGameState.CreateMonster)
            return;


        transform.localPosition += dir * m_speed;
        Vector3 _tempVec = InGameMgr.Instance.hero.transform.position - transform.position;
        if (Mathf.Abs(_tempVec.magnitude) >= 10f)
        {
            base.Close();
        }




    }
}
