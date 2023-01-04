using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolaTrasform : MonoBehaviour
{
    public Vector2 start;
    public Vector2 target = new Vector2(10, 10);
    public float Speed = 5f;
    private float time = 0;
    public float t;

    private Vector3 TMP;
    private float Distacne;

    // cos X , sin y , theta end / start
    private void Start()
    {
        Distacne = Vector3.Distance(this.transform.position, target);
        Debug.Log($"{IncidenceAngle(start, target)}");
    }

    public void Test()
    {
        t += Time.deltaTime;
       //gameObject.transform.position += new Vector3(0, Mathf.Sin(theta));
    }

    public void parabolaTrasform()
    {
       var a = 3 * Mathf.Deg2Rad;
        var b = 3 * Mathf.Rad2Deg;
    }

    public float IncidenceAngle(Vector3 _start, Vector3 _end)
    {
        Vector3 _tmpVector = _end - _start;
        Debug.Log($"{_tmpVector}");
        return Mathf.Atan2(_tmpVector.y, _tmpVector.x) * Mathf.Rad2Deg;
    }


    public void Update()
    {
        //t += Time.deltaTime;
        //float Sin = 10 * Mathf.Sin(Distacne * t);
        //float cos = 10 * Mathf.Cos(Distacne * t);
        //transform.position = new Vector3(cos, Sin);
        //Debug.Log(transform.position);
    }
}
