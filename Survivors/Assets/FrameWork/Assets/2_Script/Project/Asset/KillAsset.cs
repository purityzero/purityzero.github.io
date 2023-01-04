using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Febucci.UI;

public class KillAsset : Asset
{
    FlowCommand m_flowCommand = new FlowCommand();
    [SerializeField]
    private TextAnimatorPlayer m_animator;
    public override void Open()
    {
       // base.Open();
        ResetData();
        SetText();
    }


    public override void Notify(Subject _subject)
    {
        if (GameData_Stage.Instance.kill == count)
            return;
        Debug.Log("############ Kill Notify");
        count = GameData_Stage.Instance.kill;
        SetText();
    }

    public override void SetText()
    {
        m_flowCommand.Clear();
        m_flowCommand.Add(new Command_Delegate(() => { m_animator.ShowText($"<bounce>Kill : {count}</bounce>"); }));
        m_flowCommand.Add(new Command_DeltaTime(1, () => { m_animator.StopShowingText(); }));
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        m_flowCommand.Update();
    }
}
