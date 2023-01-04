using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class UIAutoScreen : MonoBehaviour
{
    public float baseScreen_height = 1280f;
    public float baseScreen_width = 720f;
    public CanvasScaler canvasScaler;

    private void Start()
    {
        ResetScreen();
    }

    void ResetScreen()
    {       
        float ScreenRatio = baseScreen_width / baseScreen_height;
        float currentRatio = Screen.width / (float)Screen.height;

        if (ScreenRatio < currentRatio)
        {
            canvasScaler.matchWidthOrHeight = 1f;
        }
        else
        {
            canvasScaler.matchWidthOrHeight = 0f;
        }
    }

    private void OnRectTransformDimensionsChange()
    {
        ResetScreen();
    }
}
