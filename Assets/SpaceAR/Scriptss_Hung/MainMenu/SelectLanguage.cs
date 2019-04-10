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
        /// chọn ngôn ngữ VN
        /// </summary>
        public GameObject _vnflag;

        /// <summary>
        /// chọn ngôn ngữ Anh
        /// </summary>
        public GameObject _engflag;

        [HideInInspector]
        public bool en, vn;

        private void Start()
        {
            // Th test khi chưa qua scene setting 
            // nếu chưa chọn ngôn ngữ bên scene setting thì set mặc đinhj tiếng việt
            //if (PlayerPrefs.GetString("SpaceLanguage") == null)
            //{
            //    OnOnPickLanguage("VI", "B");
            //    vn = true;
            //    en = false;
            //}
            //// ngược lại nếu đã có rồi thì set localization theo playpref
            //else
            //{
            //    Localization.language = PlayerPrefs.GetString("SpaceLanguage");
            //    // cái này ko chắc đúng vì chưa dc test
            //    // nếu localization đang có key là VI đưa ra 2 biến bool để check bên sound
            //    if (Localization.language == "VI")
            //    {
            //        vn = true;
            //        en = false;
            //    }
            //    if (Localization.language == "EN")
            //    {
            //        vn = false;
            //        en = true;
            //    }
            //}
            Localization.language = "VI";
            en = false;
            vn = true;

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

        /// <summary>
        /// đổi ngôn ngữ VN
        /// </summary>
        public void ClickVNFlag()
        {
            OnOnPickLanguage("VI", "B");
            vn = true;
            en = false;
            HideIconLanguage();
        }

        /// <summary>
        /// đổi ngôn ngữ Eng
        /// </summary>
        public void ClickEngFlag()
        {
            OnOnPickLanguage("EN", "B");
            vn = false;
            en = true;
            HideIconLanguage();
        }

        private void HideIconLanguage()
        {
            UIPlayTween tw = _vnflag.AddComponent<UIPlayTween>();
            tw.tweenTarget = _vnflag;
            tw.includeChildren = true;
            tw.playDirection = AnimationOrTween.Direction.Reverse;

            UIPlayTween tw1 = _engflag.AddComponent<UIPlayTween>();
            tw1.tweenTarget = _engflag;
            tw1.includeChildren = true;
            tw1.playDirection = AnimationOrTween.Direction.Reverse;
        }
    }
}
