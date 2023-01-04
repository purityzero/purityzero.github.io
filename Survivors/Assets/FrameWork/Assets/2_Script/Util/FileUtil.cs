using UnityEngine;
using System.IO;

namespace Util
{
    public static class FileUtil
    {
        static public bool Save(string _path, string _data)
        {
            if( string.IsNullOrWhiteSpace(_path) == true )
            {
                Debug.LogError("[null == path]");
                return false;
            }          

            if ( null == _data )
            {
                Debug.LogError("[null == data] path : " + _path);
                return false;
            }

            string _directory = Path.GetDirectoryName(_path);
            if( Directory.Exists(_directory) == false )
            {
                Directory.CreateDirectory(_directory);
            }

            try
            {
                File.WriteAllText(_path, _data);
            }
            catch(System.Exception _e)
            {
                Debug.LogError("[Save error] path : " + _path + ", Exception : " + _e.ToString());
            }            

            return true;
        }
        static public string Load(string _path)
        {
            string _data = null;

            if( System.IO.File.Exists(_path) == false )
            {
                Debug.LogWarning("no have file : " + _path);
                return null;
            }

            try
            {
                _data = File.ReadAllText(_path);
            }
            catch (System.Exception _e)
            {
                Debug.LogError("[Load error] path : " + _path + ", Exception : " + _e.ToString());
            }

            return _data;
        }

        static public void  Delete(string _path)
        {
            if (System.IO.File.Exists(_path) == false)
            {
                Debug.LogWarning("no have file : " + _path);
                return;
            }
            File.Delete(_path);
        }

        static public bool Exists(string _path)
        {
            return System.IO.File.Exists(_path);
        }

        static public string LoadRes(string _path)
        {
            TextAsset _asset = ResUtil.Load<TextAsset>(_path);
            if (null == _asset)
                return null;

            return _asset.text;
        }
    }
}

