using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum eInGameState
{
    Ready,
    Idle,
    Popup,
    Levelup,
    CreateMonster,
    Finish
}

public enum eInGameFsmMsg
{
    ChangeNextState,
}


public class InGameFsmMsg : FsmMsg
{
    public eInGameState? state;
    public InGameFsmMsg(eInGameFsmMsg _type, eInGameState _state) : base((int)_type)
    {
        state = _state;
    }

    Dictionary<string, int> DicKey = new Dictionary<string, int>();

    public void Test()
    {
        string test = "aaaa";
        test[0].ToString();
        if (DicKey.ContainsKey("a"))
        {
            DicKey["a"] += 1;
        }
    }
}

public class InGameState : FsmState<eInGameState>
{
    protected eInGameState m_NextState;
    public InGameState(eInGameState _state) : base(_state)
    {
        m_stateType = _state;
        GameData_Stage.Instance.SetInGameState(_state);
    }

    public override void Enter(FsmMsg _msg)
    {
        base.Enter(_msg);
        if (GameDataMgr.Instance.IsData<GameData_Stage>() == false)
            return;

        GameData_Stage.Instance.SetState(m_stateType);
    }

    public override void SetMsg(FsmMsg _msg)
    {
        base.SetMsg(_msg);
        var _inGameMsg = _msg as InGameFsmMsg;
        if((eInGameFsmMsg)_inGameMsg.msgType == eInGameFsmMsg.ChangeNextState)
        {
            if (_inGameMsg.state != null)
            {
                SetNextState((eInGameState)_inGameMsg.state);
            }
        }
    }

    public void SetNextState(eInGameState _type)
    {
        m_NextState = _type;
    }
}

public class InGameState_Ready : InGameState
{

    FlowCommand m_command = new FlowCommand();
    public InGameState_Ready() : base(eInGameState.Ready)
    {
    }

    public override void Enter(FsmMsg _msg)
    {
        base.Enter(_msg);
        SetNextState(eInGameState.CreateMonster);
        m_command.Add(new Command_DeltaTime(2f, null));
    }

    public override void Update()
    {
        m_command.Update();
        if (m_command.IsFinished())
        {
            InGameMgr.Instance.SetState(m_NextState);
        }
    }
}

public class InGameState_Idle : InGameState
{
    public float timer = 0;
    public FlowCommand m_command = new FlowCommand();
    public InGameState_Idle() : base(eInGameState.Idle)
    {
    }

    public override void Enter(FsmMsg _msg)
    {
        base.Enter(_msg);
        m_NextState = eInGameState.CreateMonster;
        m_command.Add(new Command_DeltaTime(3f, ()=> { InGameMgr.Instance.SetState(m_NextState); }));
    }



    public override void Update()
    {
        base.Update();

        m_command.Update();
        if (m_NextState == eInGameState.Levelup || m_NextState == eInGameState.Popup)
        {
            m_command.Clear();
            InGameMgr.Instance.SetState(m_NextState);
        }
    }
}

public class InGameState_Popup : InGameState
{

    public InGameState_Popup() : base(eInGameState.Popup)
    {
    }

    public override void Enter(FsmMsg _msg)
    {
        base.Enter(_msg);
        m_NextState = eInGameState.Idle;
    }

    public override void Update()
    {
        base.Update();
        if (UIMgr.Instance.popup.GetCount() <= 0)
        {
            InGameMgr.Instance.SetState(m_NextState);
        }
    }
}

public class InGameState_LevelUp : InGameState
{
    private FlowCommand m_command = new FlowCommand();
    public InGameState_LevelUp() : base(eInGameState.Levelup)
    {
    }

    public override void Enter(FsmMsg _msg)
    {
        base.Enter(_msg);
        Debug.Log("Enter Level Up");
        m_NextState = eInGameState.Idle;

        m_command.Add(new Command_OpenDialog("Prefab/UIInGameLvUp"));
    }


    public override void Update()
    {
        base.Update();
        m_command.Update();
        if(m_command.IsFinished())
        {
            InGameMgr.Instance.SetState(m_NextState);
        }
    }
}

public class InGameState_CreateMonster : InGameState
{

    FlowCommand m_command = new FlowCommand();
    public InGameState_CreateMonster() : base(eInGameState.CreateMonster)
    {
    }

    public override void Enter(FsmMsg _msg)
    {
        base.Enter(_msg);
        m_NextState = eInGameState.Idle;
        m_command.Add(new Command_Delegate(InGameMgr.Instance.CraeteMonster));
    }

    public override void Update()
    {
        base.Update();
        m_command.Update();
        if (m_command.IsFinished())
        {
            InGameMgr.Instance.SetState(m_NextState);
        }
    }
}

public class InGameState_Finish : InGameState
{

    public InGameState_Finish() : base(eInGameState.Finish)
    {
    }

    public override void Enter(FsmMsg _msg)
    {
        var optionDlg = UIMgr.Instance.popup.GetDialog<UIInGameFinish>("Prefab/UIInGameFinish");
        optionDlg.Open(eInGamePopup.Finish);
        GameData_User.Instance.SetBestTime(GameData_Stage.Instance.stageLv, GameData_Stage.Instance.time);
    }
}