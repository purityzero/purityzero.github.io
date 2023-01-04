using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MonsterAnimationType
{
    Idle,
    Attack,
    Hurt,
    Die
}

public class TestScripts : MonoBehaviour
{
    public Image main;
    public SpriteRenderer spriteRender;

    public SpriteAtlasAnimator<MonsterAnimationType> spriteAtlasAnimator;

    private void Start()
    {
        SpriteKey keyList = new SpriteKey("Dust", 3, 0.2f, false);
        Dictionary<MonsterAnimationType, SpriteKey> _Animation = new Dictionary<MonsterAnimationType, SpriteKey>();
        _Animation.Add(MonsterAnimationType.Idle, keyList);
        //var a = new SpriteAtlasAnimator(main, keyList, "Atlas/HeroAtlas");
        if (spriteRender == null)
        {
            spriteAtlasAnimator = new SpriteAtlasAnimator<MonsterAnimationType>(main, _Animation, "Atlas/HeroAtlas");
        }
        else
        {
            spriteAtlasAnimator = new SpriteAtlasAnimator<MonsterAnimationType>(spriteRender, _Animation, "Atlas/HeroAtlas");
        }
        //spriteAtlasAnimator = a;
    }

    private void Update()
    {
        spriteAtlasAnimator.Update();
    }

    public void OnRectTransformDimensionsChange()
    {
    }

    //    public IEnumerator coStartApp()
    //{
    //    m_time = 0;
    //    Debug.Log("0");
    //    yield return new WaitForSeconds(0.4f);
    //    Debug.Log("1");
    //    yield return null;
    //    Debug.Log("2");
    //    yield return new WaitForEndOfFrame(); // null ���� �ٸ�. EndOfFrame�� ��� �������� ���� �� ȣ��, null�� Update�� ȣ�� �� �� ȣ�� �ȴ�.
    //    Debug.Log("3");
    //    yield return "�Ǿ����ϴ�. ���������� ���ϴ�.";
    //}

    //private void Update()
    //{
    //    m_time += Time.deltaTime;
    //    Debug.Log($"Ÿ�� �� : {m_time}");
    //}
}
