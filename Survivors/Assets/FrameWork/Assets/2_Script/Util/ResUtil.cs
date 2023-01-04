using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    /**
     * ResUtil
     * 2022-06-02
     * ���ҽ� �ε�
     */
    public static class ResUtil
    {
        public static T Load<T>(string _path) where T : Object
        {
            if (string.IsNullOrWhiteSpace(_path) == true)
            {
                Debug.LogError("ResUtil::Load() null == path " + _path);
                return null;
            }

            /*if (Define.IsUsePatch)
            {
                T _load = Manager.Instance.getPatch.Load<T>(_path);
                if (null != _load)
                    return _load;
            }*/

            T _res = Resources.Load<T>(_path);
            if (null == _res)
            {
                Debug.LogError("ResUtil::Load() No have File : " + _path);
                return null;
            }

            return _res;
        }

        public static GameObject Create(string _path, Transform _parent)
        {
            GameObject _res = Load<GameObject>(_path);
            if (null == _res)
                return null;

            GameObject _ins = GameObject.Instantiate<GameObject>(_res);
            SetAttach(_parent, _ins.transform, _res.transform);
            return _ins;
        }

        public static T Create<T>(string _path, Transform _parent) where T : Component
        {
            T _res = Load<T>(_path);
            T _ins = GameObject.Instantiate<T>(_res);
            if( null == _ins )
            {
                Debug.LogError("ResUtil::Create() no component : " + _path);
                return null;
            }

            SetAttach(_parent, _ins.transform, _res.transform);
            return _ins;
        }

        public static void SetAttach(Transform _parent, Transform _chield, Transform _res)
        {
            if (null == _parent)
                return;

            if (null == _chield)
                return;
            _chield.transform.SetParent(_parent);

            if( null == _res )
            {
                _chield.transform.localPosition = Vector3.zero;
                _chield.transform.localRotation = Quaternion.identity;
                _chield.transform.localScale = Vector3.one;
            }
            else
            {
                _chield.transform.localPosition = _res.transform.localPosition;
                _chield.transform.localRotation = _res.transform.localRotation;
                _chield.transform.localScale = _res.transform.localScale;
            }           
        }
    }
}
