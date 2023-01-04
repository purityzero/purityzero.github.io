using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UICutSceneDialog : UIDialog
{
    public Image imgBack;
    public Image imgLock;
    public Button btnSkip;

    public List<UICutScenePageData> pageDataList = new List<UICutScenePageData>();
    public int page;
}
