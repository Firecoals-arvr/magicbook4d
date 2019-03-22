using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Firecoals.Space
{
    /// <summary>
    /// chọn ngôn ngữ
    /// </summary>
    public class SelectLanguage : MonoBehaviour
    {
        /// <summary>
        /// button chọn ngôn ngữ
        /// </summary>
        public GameObject buttonSelect;

        /// <summary>
        /// chọn ngôn ngữ VN
        /// </summary>
        public GameObject _vnflag;

        /// <summary>
        /// chọn ngôn ngữ Anh
        /// </summary>
        public GameObject _engflag;

        TweenPosition tw1, tw2;
        private void Start()
        {
            tw1 = _vnflag.GetComponent<TweenPosition>();
            tw2 = _engflag.GetComponent<TweenPosition>();
        }

        /// <summary>
        /// chọn ngôn ngữ
        /// </summary>
        public void ClickToChangeLanguage()
        {
            Vietnamese();
            English();
        }

        private void Vietnamese()
        {
            tw1.from = _vnflag.transform.position;
            tw1.to = _vnflag.transform.position + new Vector3(300f, 0f, 0f);
            tw1.style = UITweener.Style.Once;
            tw1.method = UITweener.Method.EaseIn;
        }

        private void English() { }

        /// <summary>
        /// đổi sang cờ VN
        /// </summary>
        public void ClickVNFlag()
        {
            buttonSelect.GetComponent<UI2DSprite>().sprite2D = _vnflag.GetComponent<UI2DSprite>().sprite2D;
            buttonSelect.GetComponent<UIButton>().normalSprite2D = _vnflag.GetComponent<UIButton>().normalSprite2D;
            buttonSelect.GetComponent<UIButton>().hoverSprite2D = _vnflag.GetComponent<UIButton>().hoverSprite2D;
            buttonSelect.GetComponent<UIButton>().pressedSprite2D = _vnflag.GetComponent<UIButton>().pressedSprite2D;
            buttonSelect.GetComponent<UIButton>().disabledSprite2D = _vnflag.GetComponent<UIButton>().disabledSprite2D;
            LanguageHasSelected();
        }

        /// <summary>
        /// đổi sang cờ Anh
        /// </summary>
        public void ClickEngFlag()
        {
            buttonSelect.GetComponent<UI2DSprite>().sprite2D = _engflag.GetComponent<UI2DSprite>().sprite2D;
            buttonSelect.GetComponent<UIButton>().normalSprite2D = _engflag.GetComponent<UIButton>().normalSprite2D;
            buttonSelect.GetComponent<UIButton>().hoverSprite2D = _engflag.GetComponent<UIButton>().hoverSprite2D;
            buttonSelect.GetComponent<UIButton>().pressedSprite2D = _engflag.GetComponent<UIButton>().pressedSprite2D;
            buttonSelect.GetComponent<UIButton>().disabledSprite2D = _engflag.GetComponent<UIButton>().disabledSprite2D;
            LanguageHasSelected();
        }

        /// <summary>
        /// chọn ngôn ngữ xong thì ẩn đi
        /// </summary>
        void LanguageHasSelected()
        {
        }
    }
}
