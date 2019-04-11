using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firecoals.Augmentation;
using System;
using Firecoals.AssetBundles;
using Loxodon.Framework.Bundles;
using Loxodon.Framework.Contexts;
using Firecoals.AssetBundles.Sound;
using UnityEngine.SceneManagement;
using Vuforia;

namespace Firecoals.Space
{
    /// <summary>
    /// class dùng thay cho DefaultTrackableEventHandler
    /// </summary>
    public class IntroScripts : DefaultTrackableEventHandler
    {
        /// <summary>
        /// đường dẫn của object trong thư mục
        /// </summary>
        public string path;

        /// <summary>
        /// AssetLoader để load sound, asset hanler và iresources để load models
        /// </summary>
        private AssetLoader assetloader;
        private AssetHandler _assethandler;
        private IResources _resources;

        /// <summary>
        /// thông tin chung về object
        /// </summary>
        private GameObject objectInfo;

        /// <summary>
        /// thông tin về các thành phần của object (nếu có)
        /// </summary>
        private GameObject panelPartInfo;

        /// <summary>
        /// tên của object
        /// </summary>
        private GameObject objectName;

        /// <summary>
        /// biến để so sánh tên object với key localization
        /// </summary>
        private string st;

        /// <summary>
        /// key cho name object
        /// </summary>
        [Header("Name key")]
        public string st1;

        /// <summary>
        /// các key cho info của object & thành phần của nó
        /// </summary>
        [Header("Information key")]
        public string st2;

        /// <summary>
        /// Key để load name và info của models
        /// </summary>
        [Header("Sounds")]
        public string tagSound;
        public string tagInfo;
        private LoadSoundbundles _loadSoundbundle;
        /// <summary>
        /// anim để chạy animation intro lúc tracking found models
        /// </summary>
        Animator anim;

        Animator animatedInfor;
        private GameObject[] inforBtn;
        bool checkOpen;


        protected override void Start()
        {
            base.Start();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        protected override void OnTrackingFound()
        {
            _loadSoundbundle = GameObject.FindObjectOfType<LoadSoundbundles>();
            assetloader = GameObject.FindObjectOfType<AssetLoader>();

            panelPartInfo = GameObject.Find("UI Root/PanelComponentInfor/Scroll View/Info");
            inforBtn = GameObject.FindGameObjectsWithTag("infor");
            animatedInfor = panelPartInfo.GetComponent<Animator>();

            //nếu đã purchase thì vào phần này
            if (ActiveManager.IsActiveOfflineOk("B"))
            {
                CloneModels();
                AutoTriggerInforButton();
            }
            // nếu chưa purchase thì vào phần này
            else
            {
                // nếu là 3 trang đầu thì cho xem model
                if (mTrackableBehaviour.TrackableName == "Solarsystem_scaled" || mTrackableBehaviour.TrackableName == "Sun_scaled" || mTrackableBehaviour.TrackableName == "Mercury_scaled")
                {
                    CloneModels();
                    AutoTriggerInforButton();
                }
                // nếu ko fai là 3 trang đầu thì cho hiện popup trả phí để xem tiếp
                else
                {
                    PopupManager.PopUpDialog("", "Bạn cần kích hoạt để sử dụng hết các tranh", default, default, default, PopupManager.DialogType.YesNoDialog, () =>
                       {
                           SceneManager.LoadScene("Activate", LoadSceneMode.Additive);
                       });
                }
            }
            base.OnTrackingFound();
        }

        protected override void OnTrackingLost()
        {
            _assethandler?.ClearAll();
            _assethandler?.Content.ClearAll();

            foreach (Transform go in mTrackableBehaviour.transform)
            {
                Destroy(go.gameObject);
            }

            ClearKeyLocalization();
            FirecoalsSoundManager.StopAll();
            base.OnTrackingLost();
        }

        /// <summary>
        /// đổi key trong localization để lấy đúng tên, thông tin theo object
        /// </summary>
        private void ChangeKeyLocalization()
        {
            if (st1.Contains(st) && st2.Contains(st))
            {
                objectName.GetComponent<UILocalize>().key = st1;
                objectInfo.GetComponent<UILocalize>().key = st2;

                objectName.GetComponent<UILabel>().text = Localization.Get(st1);
                objectInfo.GetComponent<UILabel>().text = Localization.Get(st2);

                ShowComponentInfor();
            }
        }

        private void ClearKeyLocalization()
        {
            if (objectInfo != null && objectName != null)
            {
                objectName.GetComponent<UILocalize>().key = string.Empty;
                objectInfo.GetComponent<UILocalize>().key = string.Empty;

                objectName.GetComponent<UILabel>().text = string.Empty;
                objectInfo.GetComponent<UILabel>().text = string.Empty;

                panelPartInfo.GetComponent<UILabel>().text = Localization.Get(string.Empty);
            }
        }

        void PlayAnimIntro()
        {
            anim = this.gameObject.GetComponentInChildren<Animator>();
            anim.SetTrigger("Intro");
        }

        void CloneModels()
        {
            var statTime = DateTime.Now;
            _assethandler = new AssetHandler(mTrackableBehaviour.transform);
            ApplicationContext context = Context.GetApplicationContext();
            this._resources = context.GetService<IResources>();
            GameObject go1 = assetloader.LoadGameObjectAsync(path);
            Debug.Log("load in: " + (DateTime.Now - statTime).Milliseconds);
            if (go1 != null)
            {
                var startTime = DateTime.Now;
                Instantiate(go1, mTrackableBehaviour.transform);
                PlayAnimIntro();
                Debug.Log("instantiate in: " + (DateTime.Now - startTime).Milliseconds);
            }
            _loadSoundbundle.PlayNameSound(tagSound);
            objectName = GameObject.Find("UI Root/Name Panel/BangTen/Label");
            objectInfo = GameObject.Find("UI Root/PanelInfor/Scroll View/Info");

            st = mTrackableBehaviour.TrackableName.Substring(0, mTrackableBehaviour.TrackableName.Length - 7);
            st.ToLower();
            ChangeKeyLocalization();
        }

        void AutoTriggerInforButton()
        {
            for (int i = 0; i < inforBtn.Length; i++)
            {
                if (inforBtn[i].activeInHierarchy)
                {
                    inforBtn[i].GetComponent<UIButton>().onClick.Clear();
                    EventDelegate del = new EventDelegate(this, "ShowSmallInfo");
                    EventDelegate.Set(inforBtn[i].GetComponent<UIButton>().onClick, del);
                }
            }
        }

        private void ShowComponentInfor()
        {
            for (int i = 0; i < inforBtn.Length; i++)
            {
                inforBtn[i].GetComponentInChildren<UILabel>().text = Localization.Get(inforBtn[i].GetComponentInChildren<UILocalize>().key);
                panelPartInfo.transform.GetChild(0).transform.GetChild(0).GetComponent<UILabel>().text
                    = Localization.Get(inforBtn[i].GetComponentInChildren<UILocalize>().key);
            }
        }

        public void ShowSmallInfo()
        {
            if (checkOpen == false)
            {
                ShowObjectInfo();
            }
            else
            {
                HideObjectInfo();
            }
        }

        private void ShowObjectInfo()
        {
            checkOpen = true;
            anim.SetBool("isOpen", true);
        }

        private void HideObjectInfo()
        {
            checkOpen = false;
            anim.SetBool("isOpen", false);
        }
    }
}
