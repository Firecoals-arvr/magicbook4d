using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Firecoals.Space
{
    /// <summary>
    /// chọn ngôn ngữ
    /// </summary>
    public class SelectLanguage : LanguageController
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

        [HideInInspector]
        public bool en, vn;

        /// <summary>
        /// đổi ngôn ngữ VN
        /// </summary>


        private void Start()
        {
            Localization.language = PlayerPrefs.GetString("SpaceLanguage");
            vn = true;
        }
        
        public void ClickVNFlag()
        {
            buttonSelect.GetComponent<UI2DSprite>().sprite2D = _vnflag.GetComponent<UI2DSprite>().sprite2D;
            buttonSelect.GetComponent<UIButton>().normalSprite2D = _vnflag.GetComponent<UIButton>().normalSprite2D;
            buttonSelect.GetComponent<UIButton>().hoverSprite2D = _vnflag.GetComponent<UIButton>().hoverSprite2D;
            buttonSelect.GetComponent<UIButton>().pressedSprite2D = _vnflag.GetComponent<UIButton>().pressedSprite2D;
            buttonSelect.GetComponent<UIButton>().disabledSprite2D = _vnflag.GetComponent<UIButton>().disabledSprite2D;
            OnOnPickLanguage("VI", "B");
            PlayerPrefs.SetString("SpaceLanguage", "Tiếng Việt");
            vn = true;
            en = false;
        }

        /// <summary>
        /// đổi ngôn ngữ Eng
        /// </summary>
        public void ClickEngFlag()
        {
            buttonSelect.GetComponent<UI2DSprite>().sprite2D = _engflag.GetComponent<UI2DSprite>().sprite2D;
            buttonSelect.GetComponent<UIButton>().normalSprite2D = _engflag.GetComponent<UIButton>().normalSprite2D;
            buttonSelect.GetComponent<UIButton>().hoverSprite2D = _engflag.GetComponent<UIButton>().hoverSprite2D;
            buttonSelect.GetComponent<UIButton>().pressedSprite2D = _engflag.GetComponent<UIButton>().pressedSprite2D;
            buttonSelect.GetComponent<UIButton>().disabledSprite2D = _engflag.GetComponent<UIButton>().disabledSprite2D;
            OnOnPickLanguage("EN", "B");
            PlayerPrefs.SetString("SpaceLanguage", "English");
            vn = false;
            en = true;
        }
        
    }
}
