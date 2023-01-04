using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISelect : MonoBehaviour
{
    public GameObject[] imgList;
    public int index;

    public void SetIndex(int _index)
    {
        gameObject.SetActive(true);
        index = _index;
        for (int i = 0; i < imgList.Length; ++i)
        {
            Util.UIUtil.SetShow(imgList[i], _index == i);
        }
    }

    public void SetCount(int _index)
    {
        gameObject.SetActive(true);
        for (int i = 0; i < imgList.Length; ++i)
        {
            Util.UIUtil.SetShow(imgList[i], _index > i);
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
