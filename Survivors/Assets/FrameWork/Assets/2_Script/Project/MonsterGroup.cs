using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MonsterGroup
{
    public MemoryPooling<Monster> monsterGroup;
    public MemoryPooling<DropExp> expGroup;
    private List<Transform> m_groupTransform;
    private int MAX_CREATE_CNT = 150;

    public MonsterGroup(List<Transform> _groupTransform, Transform _attach)
    {
        m_groupTransform = _groupTransform;
        monsterGroup = new MemoryPooling<Monster>(_attach, MAX_CREATE_CNT);
        expGroup = new MemoryPooling<DropExp>(_attach, MAX_CREATE_CNT);
    }


    public void CreateMonster()
    {
        int _createCount = 20; // data로 빼서 차근차근 늘려가는 방향
        var _monsterRecord =  MonsterTable.Instance.Get(1);

        for (int i = 0; i < _createCount; ++i)
        {
            if (MAX_CREATE_CNT <= monsterGroup.activeList.Count)
                continue;

            int _randomSpot = UnityEngine.Random.Range(0, m_groupTransform.Count);
            var _monster = monsterGroup.Get(_monsterRecord.spath);
            if (_monster == null)
                return;

            _monster.Open(_monsterRecord, OnMonsterClose);
            _monster.transform.localPosition = m_groupTransform[_randomSpot].localPosition + (Vector3.one * (UnityEngine.Random.Range(0, 50f)));
        }

    }

    public void OnMonsterClose(MonsterRecord _record, Vector3 _position)
    {
        var _dropRecord = DropExpTable.Instance.Get(1);
        var _dropExp = expGroup.Get(_dropRecord.sPath);

        _dropExp.Open(_dropRecord);
        _dropExp.transform.localPosition = _position;
    }

    public List<Monster> GetDamageMonsterList(Transform _SkillTransform, float _distance)
    {
        List<Monster> monsterList = new List<Monster>();

        for (int j = 0; j < monsterGroup.activeList.Count; j++)
        {

            Vector3 _tempVec = _SkillTransform.position - monsterGroup.activeList[j].item.transform.position;
            if (Mathf.Abs(_tempVec.magnitude) < _distance)
                monsterList.Add(monsterGroup.activeList[j].item);
        }

        return monsterList;
    }

    public Monster FindMonster(GameObject _obj)
    {
        if (monsterGroup.activeList.Count <= 0)
            return null;

        if (monsterGroup.activeList.Exists(x => x.item.gameObject == _obj) == false)
            return null;

        Monster obj = monsterGroup.activeList.Find(x => x.item.gameObject == _obj).item;

        return obj;
    }


    public void Clear()
    {
        monsterGroup.Clear();
        expGroup.Clear();
    }

    public void UpdateLogic()
    {
        m_groupTransform.ForEach(x => x.transform.localPosition += InGameMgr.Instance.JoyStick.inputVector * InGameMgr.Instance.hero.speed);
        monsterGroup.UpdateLogic();
        expGroup.UpdateLogic();
    }
}
