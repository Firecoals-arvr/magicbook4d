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
using Firecoals.Threading.Tasks;
using Dispatcher = Firecoals.Threading.Dispatcher;

namespace Firecoals.Space
{
    /// <summary>
    /// class dùng thay cho DefaultTrackableEventHandler
    /// </summary>
    public class IntroScripts : DefaultTrackableEventHandler
    {
        /// <summary>
        /// tên bundle của object
        /// </summary>
        public string bundleName;

        /// <summary>
        /// đường dẫn của object trong thư mục
        /// </summary>
        public string path;

        /// <summary>
        /// AssetLoader để load sound, asset hanler và iresources để load models
        /// </summary>
        private AssetLoader assetloader;

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
            assetloader = GameObject.FindObjectOfType<AssetLoader>();
            _loadSoundbundle = GameObject.FindObjectOfType<LoadSoundbundles>();
            base.Start();
            Dispatcher.Initialize();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        protected override void OnTrackingFound()
        {
            //assetloader = GameObject.FindObjectOfType<AssetLoader>();
            inforBtn = GameObject.FindGameObjectsWithTag("infor");
            foreach (var a in inforBtn)
            {
                Debug.Log("<color=orange>" + a.name + "</color>");
            }

            //if (IsTargetEmpty())
            //{
            //    Execute();
            //}

            NGUITools.SetActive(objectName, true);
            AutoTriggerInforButton();
            //nếu đã purchase thì vào phần này
            if (ActiveManager.IsActiveOfflineOk("B"))
            {
                //SpawnModel();
                ShowModelsOnScreen();
                AutoTriggerInforButton();
            }
            // nếu chưa purchase thì vào phần này
            else
            {
                //nếu là 3 trang đầu thì cho xem model
                if (mTrackableBehaviour.TrackableName == "Solarsystem_scaled" || mTrackableBehaviour.TrackableName == "Sun_scaled" || mTrackableBehaviour.TrackableName == "Mercury_scaled")
                {
                    //SpawnModel();
                    ShowModelsOnScreen();
                    AutoTriggerInforButton();
                }
                //nếu ko fai là 3 trang đầu thì cho hiện popup trả phí để xem tiếp
                else
                {
                    PopupManager.PopUpDialog("", "Bạn cần kích hoạt để sử dụng hết các tranh", "OK", "Yes", "No", PopupManager.DialogType.YesNoDialog, () =>
                    {
                        SceneManager.LoadScene("Activate", LoadSceneMode.Additive);
                    });
                }
            }
            base.OnTrackingFound();
        }

        private void ShowModelsOnScreen()
        {
            if (IsTargetEmpty())
            {
                //Execute();
                NormalLoad();
            }
        }

        protected override void OnTrackingLost()
        {
            foreach (Transform go in mTrackableBehaviour.transform)
            {
                Destroy(go.gameObject);
            }
            NGUITools.SetActive(objectName, false);

            ClearKeyLocalization();
            FirecoalsSoundManager.StopAll();
            base.OnTrackingLost();
        }

        public void NormalLoad()
        {
            assetloader.LoadGameObjectAsync(path, mTrackableBehaviour.transform);
            _loadSoundbundle.PlayNameSound(tagSound);
            PlayAnimIntro();
            nameTargetSpace = mTrackableBehaviour.TrackableName.Substring(0, mTrackableBehaviour.TrackableName.Length - 7);
            nameTargetSpace.ToLower();
            ChangeKeyLocalization();
        }
        public void Execute()
        {
#if UNITY_WSA && !UNITY_EDITOR
        System.Threading.Tasks.Task t = new System.Threading.Tasks.Task(Augment);
#else
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(Augment));
#endif
            t.Start();
        }

        private void Augment()
        {

            GameObject go = null;
            Task.WhenAll(Task.Run(() =>
            {
                Debug.Log("<color=turquoise>In background thread</color>");
                Task.RunInMainThread(() =>
                {
                    assetloader.LoadGameObjectAsync(path, mTrackableBehaviour.transform);
                    _loadSoundbundle.PlayNameSound(tagSound);
                });
            })).ContinueInMainThreadWith(task =>
            {
                PlayAnimIntro();
                nameTargetSpace = mTrackableBehaviour.TrackableName.Substring(0, mTrackableBehaviour.TrackableName.Length - 7);
                nameTargetSpace.ToLower();
                ChangeKeyLocalization();
            });

        }

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


        //private void SpawnModel()
        //{
        //    if (IsTargetEmpty() && !_cached)
        //    {
        //        CachingArContents();
        //    }
        //    if (IsTargetEmpty() && _cached)
        //    {
        //        LoadingCache();
        //    }
        //}

        /// <summary>
        /// Clear all except this
        /// </summary>
        private void ClearAllOtherTargetContents()
        {
            foreach (Transform target in mTrackableBehaviour.transform.parent.transform)
            {
                if (target != transform && target.childCount > 0)
                    Destroy(target.GetChild(0).gameObject);
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
            //StartCoroutine(InstantiationAsycnModels(go));
            //InstantiationAsync.InstantiateAsync(go, 100);
            PlayAnimIntro();

            nameTargetSpace = mTrackableBehaviour.TrackableName.Substring(0, mTrackableBehaviour.TrackableName.Length - 7);
            nameTargetSpace.ToLower();
            ChangeKeyLocalization();
        }

        private IEnumerator InstantiationAsycnModels(GameObject go)
        {
            yield return new WaitForSeconds(0.2f);
            if (go != null)
            {
                Instantiate(go, mTrackableBehaviour.transform);
            }
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
                    labelInfo.gameObject.GetComponent<UILabel>().text
                        = Localization.Get(inforBtn[i].GetComponentInChildren<UILocalize>().key);
                }
                else
                {
                    labelInfo.gameObject.GetComponent<UILabel>().text = Localization.Get(string.Empty);
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

        /// <summary>
        /// thông tin thành phần con của hành tinh
        /// </summary>
        [Header("Panel information")]
        [SerializeField] Animator panelInforAnim;
        [SerializeField] UILabel labelInfo;

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