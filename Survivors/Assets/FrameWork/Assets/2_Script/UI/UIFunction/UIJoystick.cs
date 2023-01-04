using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{   
    public Image imgBg;
    public Image imgStick;
    public UISelect activeState;

    Vector3 m_inputVector = Vector3.zero;
    public Vector3 inputVector { get { return m_inputVector; } }

    bool m_isTouch = false;
    public bool isTouch { get { return m_isTouch; } }

    public void Start()
    {
        ResetData();
    }

    public void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            imgBg.rectTransform,
            ped.position,
            ped.pressEventCamera,
            out pos))
        {
            m_isTouch = true;
            pos.x = (pos.x / imgBg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / imgBg.rectTransform.sizeDelta.y);

            m_inputVector = new Vector3(pos.x, pos.y, 0f);
            m_inputVector = (m_inputVector.magnitude > 1f) ? m_inputVector.normalized : m_inputVector;

            imgStick.rectTransform.anchoredPosition
                = new Vector3(m_inputVector.x * (imgBg.rectTransform.sizeDelta.x / 3f),
                m_inputVector.y * (imgBg.rectTransform.sizeDelta.y / 3f), 0f);
        }
    }

    public void OnPointerDown(PointerEventData ped)
    {
        imgBg.transform.position = ped.pointerCurrentRaycast.worldPosition;
        activeState.SetIndex(0);
    }

    public void OnPointerUp(PointerEventData ped)
    {
        m_isTouch = false;
        m_inputVector = Vector3.zero;
        activeState.SetIndex(1);
        imgStick.rectTransform.anchoredPosition = Vector3.zero;
    }

    public void ResetData()
    {
        m_isTouch = false;
        imgBg.transform.position = Vector3.zero;
        m_inputVector = Vector3.zero;
        activeState.SetIndex(1);
        imgStick.rectTransform.anchoredPosition = Vector3.zero;
    }

    public float GetHorizontalValue()
    {
        return m_inputVector.x;
    }

    public float GetVerticalValue()
    {
        return m_inputVector.y;
    }

    public Vector3 GetValue()
    {
        return m_inputVector;
    }

    private void Update()
    {
        if (m_isTouch == false)
            return;


        if (m_inputVector == Vector3.zero)
        {
            float _y = Input.GetAxis("Vertical");
            float _x = Input.GetAxis("Horizontal");
            if (Mathf.Abs(_x) > 0f || Mathf.Abs(_y) > 0f)
            {
                m_inputVector.x = _x;
                m_inputVector.y = _y;
            }
            else
            {
                m_inputVector.x = 0f;
                m_inputVector.y = 0f;
            }
        }

        gameObject.transform.localPosition += inputVector * InGameMgr.Instance.hero.speed;
    }


    public static float SignedAngle(Vector3 from, Vector3 to, Vector3 normal)
    {
        // angle in [0,180]
        float angle = Vector3.Angle(from, to);
        float sign = Mathf.Sign(Vector3.Dot(normal, Vector3.Cross(from, to)));
        return angle * sign + 180f;
    }

}