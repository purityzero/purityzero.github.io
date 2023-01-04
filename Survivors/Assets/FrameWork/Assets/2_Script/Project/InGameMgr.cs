using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InGameMgr : SceneLogic<InGameMgr>
{
    [SerializeField]
    private UIJoystick m_joyStick;
    public UIJoystick JoyStick { get { return m_joyStick; } }

    private FsmClass<eInGameState> m_fsm;
    [SerializeField]
    private Hero m_Hero;
    public Hero hero { get { return m_Hero; } }

    private MonsterGroup m_monsterGroup;
    [SerializeField]
    public Transform m_mosterAttach;
    public List<Transform> monsterView;


    private void Awake()
    {
        m_sceneType = eSceneType.Game;
        m_fsm = new FsmClass<eInGameState>();
    }
    public void Start()
    {
        m_monsterGroup = new MonsterGroup(monsterView, m_mosterAttach);
    }

    public override void Init()
    {
        bool _isContinue = GameDataMgr.Instance.IsStageData();
        m_assetList = UIMgr.Instance.Asset.GetDialog<UIAssetList>("Prefab/UIInGameAsset");
        m_assetList.Open();
        GameDataMgr.Instance.SetStageData();

        m_fsm.Clear();
        m_fsm.AddFsm(new InGameState_Ready());
        m_fsm.AddFsm(new InGameState_CreateMonster());
        m_fsm.AddFsm(new InGameState_Idle());
        m_fsm.AddFsm(new InGameState_LevelUp());
        m_fsm.AddFsm(new InGameState_Popup());
        m_fsm.AddFsm(new InGameState_Finish());

        m_monsterGroup.Clear();
        m_Hero.Open(HeroTable.Instance.Get(GameData_User.Instance.getSelectHero));
        if (_isContinue)
        {
            Continue();
        }
        else
        {
            m_Hero.AddSkill(SkillTable.Instance.GetRecord(eSkillDefine.Gun, 1));
            GameData_Stage.Instance.SetHp((int)hero.hp);
        }

        m_fsm.SetState(eInGameState.Ready);
    }

    public void Continue()
    {
        for (int i = 0; i < GameData_Stage.Instance.skillList.Count; i++)
        {
            m_Hero.AddSkill(SkillTable.Instance.GetRecord(GameData_Stage.Instance.skillList[i].eSkillType, GameData_Stage.Instance.skillList[i].lv));
        }
        m_Hero.SetHp(GameData_Stage.Instance.hp);
        if(SkillTable.Instance.GetLevelUpSkillList().Count <= 0)
        {
            GetAsset<ExpAsset>().MaxLv();
        }
       SetNextState(eInGameFsmMsg.ChangeNextState, GameData_Stage.Instance.CurrentState);
    }

    public void SetNextState(eInGameFsmMsg _type, eInGameState _state)
    {
        m_fsm.SetMsg(new InGameFsmMsg(_type, _state));
    }

    public void CraeteMonster()
    {
        m_monsterGroup.CreateMonster();
    }

    float m_abs = 0;
    public void  SetmaxDistance(float _abs)
    {
        if(_abs > m_abs)
        {
            Debug.Log("몬스터 최대 거리 기록 : " + _abs);
            m_abs = _abs;
        }
    }

    public void SetState(eInGameState _state)
    {
        if (_state == getState())
            return;

        m_fsm.SetState(_state);
    }

    public eInGameState getState()
    {
        return m_fsm.getStateType;
    }

    public bool ExistsHero(Vector3 _transform, float _distance)
    {
        Vector3 _tempVec = _transform - hero.transform.position;
        if (Mathf.Abs(_tempVec.magnitude) < _distance)
        {
            return true;
        }
        return false;
    }

    public Monster FindMonster(GameObject _obj)
    {
        if (m_monsterGroup == null)
        {
            Debug.LogError("no find monster Group!");
            return null;
        }

        if (_obj == hero)
        {
            Debug.Log("find Hero");
            return null;
        }

        return m_monsterGroup.FindMonster(_obj);
    }

    public List<Monster> GetDamageMonsterList(Transform _SkillTransform, float _distance)
    {
        return m_monsterGroup.GetDamageMonsterList(_SkillTransform, _distance);
    }

    public T GetAsset<T>() where T : Asset
    {
        return m_assetList.GetAsset<T>();
    }

    protected override void Update()
    {
        base.Update();
        m_fsm.Update();
        if (InGameMgr.Instance.getState() == eInGameState.Levelup || InGameMgr.Instance.getState() == eInGameState.Popup || InGameMgr.Instance.getState() == eInGameState.Ready)
            return;

        Util.UIUtil.SetShow(m_joyStick.gameObject, hero.hp > 0);
        hero.SetShow(hero.hp > 0);
        if (hero.hp <= 0)
        {
            return;
        }
        m_Hero.UpdateLogic();
        m_monsterGroup.UpdateLogic();
    }
}
