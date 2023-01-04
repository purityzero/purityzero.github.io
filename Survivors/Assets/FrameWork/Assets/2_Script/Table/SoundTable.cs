using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum eSOUND_PLAY_TYPE
{ 
    NONE,
    BGM_INTRO,
    BGM_LOBBY,
    BGM_BATTLE,
}


[System.Serializable]
public class SoundRecord : Record
{
    public string path;
    public eSOUND_PLAY_TYPE soundPlayType;

}


public class SoundRecordGroup_SoundPlayType
{
    public eSOUND_PLAY_TYPE soundPlayType;
    public List<SoundRecord> list = new List<SoundRecord>();
   
    public SoundRecordGroup_SoundPlayType(eSOUND_PLAY_TYPE _soundPlayType)
    {
        soundPlayType = _soundPlayType;
    }

    public SoundRecord GetRandomSound()
    {
        return list[Random.Range(0, list.Count)];
    }
}


public class SoundTable : Table<SoundRecord>
{
    public static SoundTable Instance { get { return TableMgr.Instance.GetTable<SoundTable>(); } }

    Dictionary<eSOUND_PLAY_TYPE, SoundRecordGroup_SoundPlayType> m_soundPlayTypeGroup = new Dictionary<eSOUND_PLAY_TYPE, SoundRecordGroup_SoundPlayType>();

    public override void Load(string _path)
    {
        base.Load(_path);

        for( int i=0;i<list.Count;++i)
        {
            SoundRecord _record = list[i];

            SoundRecordGroup_SoundPlayType _soundPlayTypeGroup = null;
            if (m_soundPlayTypeGroup.ContainsKey(_record.soundPlayType) == false )
            {
                m_soundPlayTypeGroup.Add(_record.soundPlayType, _soundPlayTypeGroup = new SoundRecordGroup_SoundPlayType(_record.soundPlayType));
            }
            else
            {
                _soundPlayTypeGroup = m_soundPlayTypeGroup[_record.soundPlayType];
            }

            _soundPlayTypeGroup.list.Add(_record);
        }
    }

    public string GetPath( int _id )
    {
        SoundRecord _record = Get(_id);
        if (null == _record)
            return null;

        return _record.path;
    }

    public string GetPath(eSOUND_PATH _soundPath)
    {
        return GetPath((int)_soundPath);
    }

    public string GetRandomPath( eSOUND_PLAY_TYPE _soundPlayType )
    {
        if( m_soundPlayTypeGroup.ContainsKey(_soundPlayType) == false )
        {
            return null;
        }

        SoundRecord _record = m_soundPlayTypeGroup[_soundPlayType].GetRandomSound();
        if (null == _record)
            return null;

        return _record.path;
    }
}
