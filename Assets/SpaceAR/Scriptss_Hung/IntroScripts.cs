using Firecoals.AssetBundles.Sound;
using Firecoals.Augmentation;
using Firecoals.MagicBook;
using Firecoals.Threading.Tasks;
using Loxodon.Framework.Bundles;
using Loxodon.Framework.Contexts;
using UnityEngine;
using UnityEngine.SceneManagement;
<<<<<<< HEAD
=======
using Vuforia;
using Firecoals.Threading.Tasks;
>>>>>>> origin/space
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
<<<<<<< HEAD
        //public string bundleName;
=======
        public string bundleName;
>>>>>>> origin/space

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
        [SerializeField] private GameObject objectInfo = default;

        /// <summary>
        /// bảng hiển thị tên của object
        /// </summary>
        [SerializeField] private GameObject objectName = default;

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
        /// nhạc nền
        /// </summary>
        [Header("Music background")]
        public string _backgroundMusic;

        /// <summary>
        /// load gameobject lên scene
        /// </summary>
        private GameObject _gameobjectLoaded;

        /// <summary>
        /// anim để chạy animation intro lúc tracking found models
        /// </summary>
        private Animator anim;

<<<<<<< HEAD
        /// <summary>
        /// check lúc OnTrackingFound() thì cho chạy âm thanh, OnTrackingLost() thì tắt
        /// </summary>
        private bool _playSound;
=======
        private GameObject[] inforBtn = new GameObject[] { };
        bool checkOpen;
>>>>>>> origin/space

        /// <summary>
        /// Các imagetargets
        /// </summary>
        private GameObject[] _orignialObjectTransform;

        /// <summary>
        /// scale ban ban đầu của object,
        /// các object khác nhau scale ban đầu khác nhau
        /// </summary>
        [Header("Original scale of object")]
        public Vector3 _originalLocalScale;

        protected override void Start()
        {
<<<<<<< HEAD
            base.Start();
            ApplicationContext context = Context.GetApplicationContext();
            _resources = context.GetService<IResources>();
            assetloader = GameObject.FindObjectOfType<AssetLoader>();
            _loadSoundbundle = GameObject.FindObjectOfType<LoadSoundbundles>();
            if (ActiveManager.IsActiveOfflineOk(ActiveManager.NameToProjectID(ThemeController.instance.Theme))
                || mTrackableBehaviour.TrackableName == "Solarsystem_scaled"
                || mTrackableBehaviour.TrackableName == "Sun_scaled"
                || mTrackableBehaviour.TrackableName == "Mercury_scaled")
            {
                _gameobjectLoaded = assetloader.LoadGameObjectAsync(path, mTrackableBehaviour.transform);
            }

            _playSound = true;

=======
            ApplicationContext context = Context.GetApplicationContext();
            this._resources = context.GetService<IResources>();
            assetloader = GameObject.FindObjectOfType<AssetLoader>();
            _loadSoundbundle = GameObject.FindObjectOfType<LoadSoundbundles>();
            base.Start();
>>>>>>> origin/space
            Dispatcher.Initialize();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            foreach (Transform go in mTrackableBehaviour.transform)
            {
                Destroy(go.gameObject);
            }
        }

        protected override void OnTrackingFound()
        {
<<<<<<< HEAD
            if (ActiveManager.IsActiveOfflineOk(ActiveManager.NameToProjectID(ThemeController.instance.Theme))
                || mTrackableBehaviour.TrackableName == "Solarsystem_scaled"
                || mTrackableBehaviour.TrackableName == "Sun_scaled"
                || mTrackableBehaviour.TrackableName == "Mercury_scaled")
            {
                foreach (Transform go in mTrackableBehaviour.transform)
                {
                    go.gameObject.SetActive(true);
                }
                GetOriginalTransform();
=======
            //assetloader = GameObject.FindObjectOfType<AssetLoader>();
            inforBtn = GameObject.FindGameObjectsWithTag("infor");

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
>>>>>>> origin/space
            }
            else
            {
                Debug.Log("NotPurchase");
                PopupManager.PopUpDialog("", "Bạn cần kích hoạt để sử dụng hết các tranh", "OK", "Yes", "No", PopupManager.DialogType.YesNoDialog, () =>
                {
<<<<<<< HEAD
                    SceneManager.LoadScene("Activate", LoadSceneMode.Single);
                });
=======
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
>>>>>>> origin/space
            }

<<<<<<< HEAD
            ChangeAnim.checkOpen = false;
            ShowModelsOnScreen();
=======
        private void ShowModelsOnScreen()
        {
            if (IsTargetEmpty())
            {
                Execute();
            }
        }

        protected override void OnTrackingLost()
        {
            foreach (Transform go in mTrackableBehaviour.transform)
            {
                Destroy(go.gameObject);
            }
            NGUITools.SetActive(objectName, false);
>>>>>>> origin/space

            base.OnTrackingFound();
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
            Task loadTask = Dispatcher.instance.TaskToMainThread(() =>
            {
                go = assetloader.LoadGameObjectSync(bundleName, path);
                //_cached = true;
                //Debug.Log("<color=red> GameObject created in background thread: " + go.name + "<color>");

            });
            loadTask.ContinueInMainThreadWith((task) =>
            {
                if (task.IsCompleted)
                {
                    if (go != null)
                        CloneModels(go);
                }
            });
        }

        /// <summary>
        /// hiển thị model lên màn hình khi chiếu cam vào tranh
        /// </summary>
        private void ShowModelsOnScreen()
        {
<<<<<<< HEAD
            NGUITools.SetActive(objectName, true);
            if (_playSound)
            {
                _loadSoundbundle.PlayNameSound(tagSound);
            }
            if (_backgroundMusic != string.Empty && _playSound)
=======
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
>>>>>>> origin/space
            {
                _loadSoundbundle.PlayMusicOfObjects(_backgroundMusic);
            }
            PlayAnimIntro();
            nameTargetSpace = mTrackableBehaviour.TrackableName.Substring(0, mTrackableBehaviour.TrackableName.Length - 7);
            nameTargetSpace.ToLower();
            ChangeKeyLocalization();
            _playSound = false;
        }


        protected override void OnTrackingLost()
        {
            foreach (GameObject go in AnimateInfo.lst)
            {
                Destroy(go);
            }
<<<<<<< HEAD
            foreach (Transform a in mTrackableBehaviour.transform)
            {
                a.gameObject.SetActive(false);
            }

            NGUITools.SetActive(objectName, false);

            ClearKeyLocalization();
            FirecoalsSoundManager.StopAll();
            _playSound = true;

            base.OnTrackingLost();
=======
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
>>>>>>> origin/space
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
        /// Lấy transform ban đầu từ khi instantiate object ra
        /// </summary>
        private void GetOriginalTransform()
        {
            _orignialObjectTransform = GameObject.FindGameObjectsWithTag("Leanscale");
            foreach (var a in _orignialObjectTransform)
            {
                if (a != null && a.activeSelf)
                {
                    a.transform.localScale = _originalLocalScale;
                }
            }
        }

        /// <summary>
        /// đổi key trong localization để lấy đúng tên, thông tin theo object
        /// </summary>
        private void ChangeKeyLocalization()
        {
            if (nameKeySpace.Contains(nameTargetSpace) && inforKeySpace.Contains(nameTargetSpace))
            {
<<<<<<< HEAD
                objectName.GetComponentInChildren<UILocalize>().key = nameKeySpace;
                objectInfo.GetComponent<UILocalize>().key = inforKeySpace;

                objectName.GetComponentInChildren<UILabel>().text = "[b]" + Localization.Get(nameKeySpace) + "[/b]";
                objectInfo.GetComponent<UILabel>().text = Localization.Get(inforKeySpace);
=======
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
>>>>>>> origin/space
            }
        }

        /// <summary>
        /// Clear all except this
        /// </summary>
<<<<<<< HEAD
        private void ClearAllOtherTargetContents()
=======
        private void ShowSmallInfo()
>>>>>>> origin/space
        {
            foreach (Transform target in mTrackableBehaviour.transform.parent.transform)
            {
<<<<<<< HEAD
                if (target != transform && target.childCount > 0)
                    Destroy(target.GetChild(0).gameObject);
=======
                ShowObjectInfo();
                ShowComponentInfor();
>>>>>>> origin/space
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

<<<<<<< HEAD
        /// <summary>
        /// Check if there is no child on the target
        /// </summary>
        /// <returns></returns>
        private bool IsTargetEmpty()
        {
            return mTrackableBehaviour.transform.childCount <= 0;
=======
        [Header("Panel information")]
        [SerializeField] Animator panelInforAnim;

        private void ShowObjectInfo()
        {
            checkOpen = true;
            panelInforAnim.SetBool("isOpen", true);
>>>>>>> origin/space
        }

        private void PlayAnimIntro()
        {
<<<<<<< HEAD
            ChangeAnim.changAnim = true;
            if (gameObject.GetComponentInChildren<Animator>() != null)
            {
                anim = gameObject.GetComponentInChildren<Animator>();
                //anim.Play("Idle");
                anim.SetTrigger("Intro");
            }
=======
            checkOpen = false;
            panelInforAnim.SetBool("isOpen", false);
>>>>>>> origin/space
        }
    }
}