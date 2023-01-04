using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class MoneyAsset : Asset
{
    
    protected long currentCount = 0;
    protected Tweener m_Tweener = null;
    public override void Open()
    {
        currentCount = 0;
        count = 0;
        SetText();
    }

    public override void Notify(Subject _subject)
    {
        if (count == GameData_Stage.Instance.exp)
            return;
        count = GameData_Stage.Instance.money;
        RollingUpText(currentCount, count);
    }

    public override void SetText()
    {
        RollingUpText(currentCount, count);

    }

    public void RollingUpText(long _a, long _b)
    {
        m_Tweener?.Kill(true);
        m_Tweener = DOTween.To(() => _a, x => _a = x, _b, 0.2f).OnUpdate(() =>
        {
            Util.UIUtil.SetText(text, _a.ToString("#,##0"));
        }).OnComplete(() =>
        {
            currentCount = _b; 
        });
    }
}
