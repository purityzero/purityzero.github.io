using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.U2D;

namespace Util
{
    /**
     * UIUtil
     * 2022-07-27
     * UGUI ���� ��� ���.
     */
    public static class UIUtil
    {
        public static void SetBtnClick(Button _btn, UnityEngine.Events.UnityAction _click)
        {
            if (null == _btn)
                return;

            _btn.onClick.AddListener(_click);
        }

        public static void SetBtnClick(Button[] _btns, UnityEngine.Events.UnityAction _click)
        {
            if (null == _btns)
                return;

            for (int i = 0; i < _btns.Length; ++i)
            {
                SetBtnClick(_btns[i], _click);
            }
        }

        static SpriteAtlas GetSprites(string _path)
        {
            if (string.IsNullOrEmpty(_path) == true)
                return null;

            return ResUtil.Load<SpriteAtlas>(_path);

        }

        static public void SetIcon(Image _icon, string _path, bool _setNativeSize = true)
        {
            if (null == _icon)
                return;

            if (string.IsNullOrEmpty(_path) == true)
            {
                _icon.gameObject.SetActive(false);
                return;
            }

            _icon.gameObject.SetActive(true);
            _icon.sprite = ResUtil.Load<Sprite>(_path);
            if (_setNativeSize)
                _icon.SetNativeSize();
        }

        static public void SetIcon(Image _icon, Sprite _spr, bool _setNativeSize = true)
        {
            if (null == _icon)
                return;

            if (null == _spr)
            {
                _icon.gameObject.SetActive(false);
                return;
            }

            _icon.gameObject.SetActive(true);
            _icon.sprite = _spr;
            if (_setNativeSize)
                _icon.SetNativeSize();

        }

        public static void SetAlpha(Graphic _graphic, float _alpha)
        {
            if (null == _graphic)
                return;

            Color _color = _graphic.color;
            _color.a = _alpha;
            _graphic.color = _color;
        }

        public static void SetAlpha(Graphic[] _graphics, float _alpha)
        {
            if (null == _graphics)
                return;

            for (int i = 0; i < _graphics.Length; ++i)
            {
                SetAlpha(_graphics[i], _alpha);
            }
        }

        public static bool IsOpen(MonoBase _mono)
        {
            if (null == _mono)
                return false;

            return _mono.isOpen;
        }

        public static void SetText(TextMeshProUGUI _text, string _str)
        {
            if (null == _text)
                return;

            if (string.IsNullOrWhiteSpace(_str) == true)
            {
                _text.gameObject.SetActive(false);
                return;
            }

            _text.gameObject.SetActive(true);
            _text.text = _str;
        }

        public static void SetText(Text _text, string _str)
        {
            if (null == _text)
                return;

            if (string.IsNullOrWhiteSpace(_str) == true)
            {
                _text.gameObject.SetActive(false);
                return;
            }

            _text.gameObject.SetActive(true);
            _text.text = _str;
        }

        public static void SetText(Text[] _texts, string _str)
        {
            if (null == _texts)
                return;
            for (int i = 0; i < _texts.Length; ++i)
            {
                SetText(_texts[i], _str);
            }
        }

        public static void SetValue(Slider _slider, float _value)
        {
            if (null == _slider)
                return;

            _slider.value = _value;
        }

        public static void SetColor(Graphic _graphic, Color _color)
        {
            if (null == _graphic)
                return;

            _graphic.color = _color;
        }

        public static bool IsShow(GameObject _gameObject)
        {
            if (null == _gameObject)
                return false;

            return _gameObject.activeSelf;
        }

        public static void SetShow(GameObject _gameObject, bool _isActive)
        {
            if (null == _gameObject)
                return;
            _gameObject.SetActive(_isActive);
        }
    }
}
