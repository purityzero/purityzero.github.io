using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAsset : Asset
{
    float m_time;
    StageRecord m_record;

    private void Awake()
    {
        m_record = StageTable.Instance.Get(GameData_User.Instance.selectStage);
    }

    public override void Open()
    {

        if (GameDataMgr.Instance.IsData<GameData_Stage>() == false)
        {
            m_time = 0;
        }
        else
        {
            m_time = GameData_Stage.Instance.time;
        }

    }

    public override void Notify(Subject _subject)
    {

    }

    public override void SetText()
    {
        Util.UIUtil.SetText(text, $"Time : {Util.TimeUtil.GetMinute((int)GameData_Stage.Instance.time).ToString("00")}:{Util.TimeUtil.GetSecond((int)GameData_Stage.Instance.time).ToString("00")}");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (GameDataMgr.Instance.IsData<GameData_Stage>() == false)
            return;

        if (InGameMgr.Instance.getState() == eInGameState.Levelup || InGameMgr.Instance.getState() == eInGameState.Popup)
            return;

        if (GameData_Stage.Instance.time >= m_time)
        {
            m_time = GameData_Stage.Instance.time;
        }

        m_time += Time.deltaTime;
        if ((int)m_time == GameData_Stage.Instance.time)
            return;

        SetText();
        GameData_Stage.Instance.SetTime((int)m_time);
    }
}
