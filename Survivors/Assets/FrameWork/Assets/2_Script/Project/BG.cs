using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class BG : UIDialog
{
    public bool isMain;
    [SerializeField]
    public eDicrectionType dicType;
    public GameObject obj;
    //private RectTransform m_rectTransform;
    public RectTransform getRectTrasform { get { return m_rectTransform; } }
    private bool m_isClose = false;
    public bool isClose { get { return m_isClose; } }
    public string BgPath;
    public Image img;

    private string m_bgPath;
    private void Awake()
    {
        m_rectTransform = GetComponent<RectTransform>();

    }

    public void Open(Vector2 _ItemPos, eDicrectionType _type, bool _isMain = false)
    {
        base.Open();
        m_rectTransform.localPosition = _ItemPos;
        obj.SetActive(true);
        if (string.IsNullOrWhiteSpace(m_bgPath))
        {
            Util.UIUtil.SetIcon(img, m_bgPath = BgPath + $"{Random.Range(1, 4)}", false);
        }
        dicType = _type;
        isMain = _isMain;
        m_isClose = false;
    }

    public override void Close()
    {
        obj.SetActive(false);
        m_isClose = true;
    }


    public void setMain(bool _isMain)
    {
        isMain = _isMain;
    }

}
