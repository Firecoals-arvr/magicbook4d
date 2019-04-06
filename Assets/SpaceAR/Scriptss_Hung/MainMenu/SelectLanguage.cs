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
            // Th test khi chưa qua scene setting 
            // nếu chưa chọn ngôn ngữ bên scene setting thì set mặc đinhj tiếng việt
            if (PlayerPrefs.GetString("SpaceLanguage") == null)
            {
                OnOnPickLanguage("VI", "B");
                vn = true;
                en = false;
            }
            // ngược lại nếu đã có rồi thì set localization theo playpref
            else
            {
                Localization.language = PlayerPrefs.GetString("SpaceLanguage");
                // cái này ko chắc đúng vì chưa dc test
                // nếu localization đang có key là VI đưa ra 2 biến bool để check bên sound
                if (Localization.language == "VI")
                {
                    vn = true;
                    en = false;
                }
                if(Localization.language == "EN")
                {
                    vn = false;
                    en = true;
                }
            }
            
        }
        // override lại class vs 2 Th 1 là tiếng việt thì set localization và lưu vào playprefs
        // th2 là tiếng anh cũng v
        protected override void OnOnPickLanguage(string languageId, string projectId)
        {
            switch (languageId)
            {
                case "VI":
                    Localization.language = "VI";
                    PlayerPrefs.SetString("SpaceLanguage", "VI");
                    break;
                case "EN":
                    Localization.language = "EN";
                    PlayerPrefs.SetString("SpaceLanguage", "EN");
                    break;
            }
            base.OnOnPickLanguage(languageId, projectId);
        }
        public void ClickVNFlag()
        {
            buttonSelect.GetComponent<UI2DSprite>().sprite2D = _vnflag.GetComponent<UI2DSprite>().sprite2D;
            buttonSelect.GetComponent<UIButton>().normalSprite2D = _vnflag.GetComponent<UIButton>().normalSprite2D;
            buttonSelect.GetComponent<UIButton>().hoverSprite2D = _vnflag.GetComponent<UIButton>().hoverSprite2D;
            buttonSelect.GetComponent<UIButton>().pressedSprite2D = _vnflag.GetComponent<UIButton>().pressedSprite2D;
            buttonSelect.GetComponent<UIButton>().disabledSprite2D = _vnflag.GetComponent<UIButton>().disabledSprite2D;
            OnOnPickLanguage("VI", "B");
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
            vn = false;
            en = true;
        }
        
    }
}
