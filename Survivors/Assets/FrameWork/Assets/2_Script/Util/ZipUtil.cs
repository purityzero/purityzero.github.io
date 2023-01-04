using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ICSharpCode.SharpZipLib.BZip2;
using System.IO;


namespace Util
{
    public class ZipUtil
    {
        static public string Zip(string _data, int _level)
        {
            if (null == _data)
            {
                Debug.LogError("Zip data null");
                return null;
            }

            byte[] _byteData = System.Text.Encoding.Unicode.GetBytes(_data);
            byte[] _byteResult = Zip(_byteData, _level);
            string _result = System.Convert.ToBase64String(_byteResult);            
            return _result;
        }

        static public byte[] Zip(byte[] _data, int _level)
        {
            if (null == _data)
            {
                Debug.LogError("Zip data null");
                return null;
            }

            MemoryStream _ms_in = null;
            MemoryStream _ms_out = null;
            byte[] _result = null;

            try
            {
                _ms_in = new MemoryStream(_data, 0, _data.Length);
                _ms_out = new MemoryStream();
                BZip2.Compress(_ms_in, _ms_out, false, _level);

                _result = _ms_out.ToArray();

                _ms_in.Close();
                _ms_out.Close();
            }
            catch(System.Exception _e)
            {
                if (null != _ms_in)
                    _ms_in.Close();
                if (null != _ms_out)
                    _ms_out.Close();

                Debug.LogError("[Zip error] Exception : " + _e.ToString());
            }

            return _result;
        }

        static public string UnZip(string _data)
        {
            if (null == _data)
            {
                Debug.LogError("UnZip data null");
                return null;
            }

            byte[] _byteData = System.Convert.FromBase64String(_data);
            byte[] _byteResult = UnZip(_byteData);
            string _result = System.Text.Encoding.Unicode.GetString(_byteResult);
            return _result;
        }

        static public byte[] UnZip(byte[] _data)
        {
            if (null == _data)
            {
                Debug.LogError("UnZip data null");
                return null;
            }

            MemoryStream _ms_in = null;
            MemoryStream _ms_out = null;
            byte[] _result = null;

            try
            {
                _ms_in = new MemoryStream(_data, 0, _data.Length);
                _ms_out = new MemoryStream();
                BZip2.Decompress(_ms_in, _ms_out, false);

                _result = _ms_out.ToArray();

                _ms_in.Close();
                _ms_out.Close();
            }
            catch (System.Exception _e)
            {
                if (null != _ms_in)
                    _ms_in.Close();
                if (null != _ms_out)
                    _ms_out.Close();

                Debug.LogError("[UnZip error] Exception : " + _e.ToString());
            }

            return _result;
        }
    }
}