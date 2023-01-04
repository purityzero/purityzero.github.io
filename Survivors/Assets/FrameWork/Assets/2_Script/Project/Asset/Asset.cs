using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using TMPro;

public enum eAssetType
{
    Kill,
    Exp,
    Money,
    Time,
}

public abstract class Asset : UIDialog, IObserver
{
    public TextMeshProUGUI text;
    protected long count;
    public eAssetType AssetType;

    public abstract void SetText();
    public abstract void Notify(Subject _subject);

    protected void ResetData()
    {
        count = 0;
    }
}
