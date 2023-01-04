using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;


// HeroLv은 InGameMgr에서 올려줄꺼임.
public class Hero : UIDialog
{

    private SkillMgr m_skillMgr;
    //public List<Skill> SkillGroup;
    public Transform SkillAttach;
    public Image HeroImage;
    float currentAngle = 0;
    public long HP = 1000;
    public Slider HpBar;
    public long hp { get; private set; }
    public int atk { get; private set; }
    public float speed { get; private set; }
    SpriteAtlasAnimator<MonsterAnimationType> m_animation;

    [SerializeField]
    private float triggerTime = 0;
    [SerializeField]
    private float timer = 1f;
    private HeroRecord m_heroRecord;


    public void SetShow(bool _isActive)
    {
        Util.UIUtil.SetShow(HeroImage.gameObject, _isActive);
        Util.UIUtil.SetShow(HpBar.gameObject, _isActive);
        m_skillMgr?.SetShow(_isActive);
    }

    public void Open(HeroRecord _record)
    {
        m_heroRecord = _record;
        m_skillMgr = new SkillMgr(SkillAttach, null);
        //Util.UIUtil.SetIcon(HeroImage, _record.SImagePath);
        HP = _record.Ihp;
        hp = HP;
        atk = m_heroRecord.Iatk;
        speed = m_heroRecord.FSpeed;

        SpriteKey keyList = new SpriteKey("Dust", 3, 0.2f, false);
        Dictionary<MonsterAnimationType, SpriteKey> _Animation = new Dictionary<MonsterAnimationType, SpriteKey>();
        _Animation.Add(MonsterAnimationType.Idle, keyList);
        m_animation = new SpriteAtlasAnimator<MonsterAnimationType>(HeroImage, _Animation, "Atlas/HeroAtlas");
    }

    public void AddSkill(SkillRecord _record)
    {
        m_skillMgr.Add(_record);
    }

    public float GetAngle()
    {
        if (InGameMgr.Instance.JoyStick.isTouch)
        {
            int _Revers = InGameMgr.Instance.JoyStick.inputVector.x >= 0 ? -1 : 1;
            float _angle = _Revers * Vector2.Angle(Vector3.up, InGameMgr.Instance.JoyStick.inputVector);
            currentAngle = _angle;
        }
        return currentAngle;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var _monster = InGameMgr.Instance.FindMonster(collision.gameObject);
        if (_monster == null)
            return;

        Damage(_monster.atk);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        var _monster = InGameMgr.Instance.FindMonster(collision.gameObject);
        if (_monster == null)
            return;

        triggerTime += Time.deltaTime;
        if (triggerTime >= timer)
        {
            triggerTime = 0;
            Damage(_monster.atk);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
    }


    public void Damage(long _damage)
    {
        hp -= _damage;
        HpBar.value = (float)hp / HP;
        GameData_Stage.Instance.SetHp((int)hp);
        if (hp <= 0)
        {
            InGameMgr.Instance.SetState(eInGameState.Finish);
        }
    }

    public void SetHp(long _hp)
    {
        Debug.Log($"SetHp : Max : {HP} / current : {_hp}");
        hp = _hp;
        HpBar.value = (float)hp / HP;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (hp <= 0)
        {
            return;
        }

        if (InGameMgr.Instance.JoyStick.isTouch)
        {
            gameObject.transform.localPosition += InGameMgr.Instance.JoyStick.inputVector * speed;

        }
        m_animation.Update();
        m_skillMgr?.UpDateLogic();
    }
}
