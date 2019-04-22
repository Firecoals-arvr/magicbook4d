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
using System.Threading.Tasks;

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
        /// bảng hiển thị thông tin về object
        /// </summary>
        [SerializeField] private GameObject objectInfo;

        /// <summary>
        /// bảng hiển thị tên của object
        /// </summary>
        [SerializeField] private GameObject objectName;

        /// <summary>
        /// tên của target
        /// </summary>
        private string nameTargetSpace;

        /// <summary>
        /// key cho name object trong localization
        /// </summary>
        [Header("Name key")]
        public string nameKeySpace;

        /// <summary>
        /// các key cho info của object & thành phần của nó trong localization
        /// </summary>
        [Header("Information key")]
        public string inforKeySpace;

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

        private GameObject[] inforBtn = new GameObject[] { };
        bool checkOpen;


        protected override void Start()
        {
            ApplicationContext context = Context.GetApplicationContext();
            this._resources = context.GetService<IResources>();

            _loadSoundbundle = GameObject.FindObjectOfType<LoadSoundbundles>();
            base.Start();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        protected override void OnTrackingFound()
        {
            _assethandler = new AssetHandler(mTrackableBehaviour.transform);
            assetloader = GameObject.FindObjectOfType<AssetLoader>();
            inforBtn = GameObject.FindGameObjectsWithTag("infor");
            foreach (var a in inforBtn)
            {
                Debug.Log("button name: " + a.name);
            }
            NGUITools.SetActive(objectName, true);
            SpawnModel();
            AutoTriggerInforButton();
            //nếu đã purchase thì vào phần này
            //if (ActiveManager.IsActiveOfflineOk("B"))
            //{
            //    SpawnModel();
            //    AutoTriggerInforButton();
            //}
            //// nếu chưa purchase thì vào phần này
            //else
            //{
            //    //nếu là 3 trang đầu thì cho xem model
            //    if (mTrackableBehaviour.TrackableName == "Solarsystem_scaled" || mTrackableBehaviour.TrackableName == "Sun_scaled" || mTrackableBehaviour.TrackableName == "Mercury_scaled")
            //    {
            //        SpawnModel();
            //        AutoTriggerInforButton();
            //    }
            //    //nếu ko fai là 3 trang đầu thì cho hiện popup trả phí để xem tiếp
            //    else
            //    {
            //        PopupManager.PopUpDialog("", "Bạn cần kích hoạt để sử dụng hết các tranh", "OK", "Yes", "No", PopupManager.DialogType.YesNoDialog, () =>
            //        {
            //            SceneManager.LoadScene("Activate", LoadSceneMode.Additive);
            //        });
            //    }
            //}
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
            NGUITools.SetActive(objectName, false);

            ClearKeyLocalization();
            FirecoalsSoundManager.StopAll();
            base.OnTrackingLost();
        }

        //private void Augment()
        //{
        //    GameObject go = null;
        //    Task loadTask = Dispatcher.instance.TaskToMainThread(() =>
        //    {
        //        go = assetLoader.LoadGameObjectSync(bundleName, bundlePath);
        //        //_cached = true;
        //        Debug.Log("<color=red> GameObject created in background thread: " + go.name + "<color>");

        //    });
        //    loadTask.ContinueInMainThreadWith((task) =>
        //    {
        //        if (task.IsCompleted)
        //        {
        //            if (go != null)
        //                CloneModels(go);
        //        }
        //    });
        //}

        /// <summary>
        /// đổi key trong localization để lấy đúng tên, thông tin theo object
        /// </summary>
        private void ChangeKeyLocalization()
        {
            if (nameKeySpace.Contains(nameTargetSpace) && inforKeySpace.Contains(nameTargetSpace))
            {
                objectName.GetComponentInChildren<UILocalize>().key = nameKeySpace;
                objectInfo.GetComponent<UILocalize>().key = inforKeySpace;

                objectName.GetComponentInChildren<UILabel>().text = Localization.Get(nameKeySpace);
                objectInfo.GetComponent<UILabel>().text = Localization.Get(inforKeySpace);

                //ShowComponentInfor();
            }
        }

        private void SpawnModel()
        {
            if (IsTargetEmpty() && !_cached)
            {
                CachingArContents();
            }
            if (IsTargetEmpty() && _cached)
            {
                LoadingCache();
            }
        }

        /// <summary>
        /// xóa key UIlocalize khi chạy vào OnTrackingLost()
        /// </summary>
        private void ClearKeyLocalization()
        {
            if (objectInfo != null && objectName != null)
            {
                objectName.GetComponentInChildren<UILocalize>().key = string.Empty;
                objectInfo.GetComponent<UILocalize>().key = string.Empty;

                objectName.GetComponentInChildren<UILabel>().text = string.Empty;
                objectInfo.GetComponent<UILabel>().text = string.Empty;
            }
        }

        void PlayAnimIntro()
        {
            if (this.gameObject.GetComponentInChildren<Animator>() != null)
            {
                anim = this.gameObject.GetComponentInChildren<Animator>();
                anim.SetTrigger("Intro");
            }
        }

        private bool _cached = false;

        void CloneModels(GameObject go)
        {
            StartCoroutine(InstantiationAsycnModels(go));
            PlayAnimIntro();

            //var statTime = DateTime.Now;
            //Debug.Log("load in: " + (DateTime.Now - statTime).Milliseconds);
            //if (this.transform.childCount == 0)
            //{
            //    var startTime = DateTime.Now;
            //    //Instantiate(go1, mTrackableBehaviour.transform);
            //    PlayAnimIntro();
            //    Debug.Log("instantiate in: " + (DateTime.Now - startTime).Milliseconds);
            //}

            nameTargetSpace = mTrackableBehaviour.TrackableName.Substring(0, mTrackableBehaviour.TrackableName.Length - 7);
            nameTargetSpace.ToLower();
            ChangeKeyLocalization();
        }

        private IEnumerator InstantiationAsycnModels(GameObject go)
        {
            yield return new WaitForSeconds(0.2f);
            Instantiate(go, mTrackableBehaviour.transform);
            _loadSoundbundle.PlayNameSound(tagSound);
        }

        /// <summary>
        /// Check if there is no child on the target
        /// </summary>
        /// <returns></returns>
        private bool IsTargetEmpty()
        {
            return mTrackableBehaviour.transform.childCount <= 0;
        }

        private void CachingArContents()
        {
            GameObject go = null;

            InstantiationAsync.Asynchronous(() =>
            {
                if (IsTargetEmpty())
                {
                    go = assetloader.LoadGameObjectAsync(path);
                    _cached = true;
                    if (go != null)
                        CloneModels(go);
                }

            }, 100);
        }

        private void LoadingCache()
        {
            GameObject go = assetloader.LoadGameObjectAsync(path);
            if (go != null)
                CloneModels(go);
        }

        /// <summary>
        /// xem thông tin thành phần của hành tinh
        /// </summary>
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

        /// <summary>
        /// lấy thông tin thành phần con của hành tinh
        /// </summary>
        private void ShowComponentInfor()
        {
            for (int i = 0; i < inforBtn.Length; i++)
            {
                if (inforBtn[i] != null)
                {
                    inforBtn[i].GetComponentInChildren<UILabel>().text = Localization.Get(inforBtn[i].GetComponentInChildren<UILocalize>().key);
                    objectInfo.GetComponent<UILabel>().text
                        = Localization.Get(inforBtn[i].GetComponentInChildren<UILocalize>().key);
                }
                else
                {
                    objectInfo.GetComponent<UILabel>().text = Localization.Get(string.Empty);
                }
            }
        }

        /// <summary>
        /// hiện bảng thông tin con của hành tinh
        /// </summary>
        private void ShowSmallInfo()
        {
            if (checkOpen == false)
            {
                ShowObjectInfo();
                ShowComponentInfor();
            }
            else
            {
                HideObjectInfo();
            }
        }

        [Header("Panel information")]
        [SerializeField] Animator panelInforAnim;

        private void ShowObjectInfo()
        {
            checkOpen = true;
            panelInforAnim.SetBool("isOpen", true);
        }

        private void HideObjectInfo()
        {
            checkOpen = false;
            panelInforAnim.SetBool("isOpen", false);
        }
    }
}