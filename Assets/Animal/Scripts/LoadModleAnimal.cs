using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firecoals.Augmentation;
using System;
using Firecoals.AssetBundles;
using Loxodon.Framework.Bundles;
using Loxodon.Framework.Contexts;
using Firecoals.AssetBundles.Sound;
using Lean.Touch;
using UnityEngine.SceneManagement;
namespace Firecoals.Animal
{
    /// <summary>
    /// class dùng thay cho DefaultTrackableEventHandler
    /// </summary>
    public class LoadModleAnimal : DefaultTrackableEventHandler
    {
        /// <summary>
        /// tên object trong asset bundles
        /// </summary>
        public string BuildName;

        /// <summary>
        /// đường dẫn của object trong thư mục
        /// </summary>
        public string path;

        /// <summary>
        /// đường dẫn sound
        /// </summary>
        ///
        public float TimeStartEffect;
        private AssetHandler assetHandler { get; set; }
        private AssetLoader assetLoader { get; set; }
        private LoadSoundbundles _loadsoundbundles;
        private AudioSource audio;
        private IResources _resources;
        public string tagInfo;
        public string tagSound;
        public string tagName;
        private GameObject _textInfo;
        private GameObject _textName;
        public string st;
        /// <summary>
        /// key cho name object
        /// </summary>
        [Header("Name key")]
        public string TextInfoName;

        /// <summary>
        /// key cho info object
        /// </summary>
        [Header("Information key")]
        public string st2;

        /// <summary>
        /// Key để load name và info của models
        /// </summary>
        private string _nameModelTracking;

        private UILabel InformationLabel;
        protected override void Start()
        {
           
            base.Start();
            audio = gameObject.GetComponent<AudioSource>();
            assetHandler = new AssetHandler(mTrackableBehaviour.transform);
            _loadsoundbundles = GameObject.FindObjectOfType<LoadSoundbundles>();
              PlayerPrefs.SetString("AnimalLanguage", "EN");
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        IEnumerator StarEffect(GameObject go)
        {
            assetLoader = GameObject.FindObjectOfType<AssetLoader>();
            Debug.Log("mTrackableBehaviour"+ mTrackableBehaviour);
            RandomEffect.Instance.Onfound(TimeStartEffect);
            yield return new WaitForSeconds(0.1f);
            GameObject.Instantiate(go, mTrackableBehaviour.transform);
            _loadsoundbundles.PlaySound(tagSound);
            _loadsoundbundles.PlayName(tagName);  
        }
       
        protected override void OnTrackingFound()
        {
            
            GameObject  go  = assetHandler.CreateUnique(BuildName, path);
            _nameModelTracking = mTrackableBehaviour.TrackableName;
            if (go != null)
            {
                //if (ActiveManager.IsActiveOfflineOk("A"))
                //{
                //    LoadModelBundles(go);
                //}
                //else
                //{
                //    if (mTrackableBehaviour.TrackableName == "Lion" || mTrackableBehaviour.TrackableName == "Elephant" || mTrackableBehaviour.TrackableName == "Gorilla")
                //    {
                //        LoadModelBundles(go);
                //    }
                //    else
                //    {
                //        PopupManager.PopUpDialog("Thông báo", "Chọn đồng ý để mở khóa trang", PopupManager.DialogType.YesNoDialog, () =>
                //        {
                //            SceneManager.LoadScene("Activate", LoadSceneMode.Additive);
                //        });
                //    }
                //}
                LoadModelBundles(go);
                base.OnTrackingFound();
            }
        }

        public void LoadModelBundles(GameObject go)
        {
            StartCoroutine(StarEffect(go));
            _textName = GameObject.Find("UI Root/Name Panel/BangTen/Label");
            _textInfo = GameObject.Find("UI Root/PanelInfor/Scroll View/Info");
            if (st != null)
            {
                Debug.Log("S-------------------t" + st.ToString());
              ChangeKeyAnimalLocalization();
            }      
        }
        protected override void OnTrackingLost()
        {
            FirecoalsSoundManager.StopAll();
            RandomEffect.Instance.Onlost();
            assetHandler?.ClearAll();
            assetHandler?.Content.ClearAll();
            //   Debug.Log("Destroy" + mTrackableBehaviour.name);
            foreach (Transform go in mTrackableBehaviour.transform)
            {
                Destroy(go.gameObject);
            }
            base.OnTrackingLost();
        }
        //private void RunAnimationIntro()
        //{
        //    if (this.gameObject.transform.GetChild(0).GetComponent<Animation>() != null)
        //    {
        //        this.gameObject.transform.GetChild(0).GetComponent<Animation>().Play("intro");
        //    }
        //    else
        //    {
        //        Debug.LogWarning("This object hasn't intro animation.");
        //    }
        //}
        private void ChangeKeyAnimalLocalization()
        {
            _textInfo.GetComponent<UILocalize>().key = TextInfoName;
            _textInfo.GetComponent<UILabel>().text = Localization.Get(TextInfoName);
            _textName.GetComponent<UILabel>().text = st;
        }
        private void ClearKeyLocalization()
        {
            if (_textName != null && _textInfo != null)
            {
                _textName.GetComponent<UILocalize>().key = string.Empty;
                _textInfo.GetComponent<UILocalize>().key = string.Empty;
            }
        }
    }
}
