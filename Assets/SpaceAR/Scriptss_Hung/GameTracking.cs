using Firecoals.Augmentation;
using Firecoals.Threading.Tasks;
using Loxodon.Framework.Bundles;
using Loxodon.Framework.Contexts;
<<<<<<< HEAD
using UnityEngine;
=======
using UnityEngine.SceneManagement;
using Vuforia;
using Firecoals.Threading.Tasks;
>>>>>>> origin/space
using Dispatcher = Firecoals.Threading.Dispatcher;

namespace Firecoals.Space
{
    /// <summary>
    /// class dùng thay cho DefaultTrackableEventHandler
    /// </summary>
    public class GameTracking : DefaultTrackableEventHandler
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

<<<<<<< HEAD
        private GameObject _gameobjectLoaded;
=======
        /// <summary>
        /// anim để chạy animation intro lúc tracking found models
        /// </summary>
        Animator anim;

        private GameObject[] inforBtn;
        bool checkOpen;

>>>>>>> origin/space

        protected override void Start()
        {
            ApplicationContext context = Context.GetApplicationContext();
            this._resources = context.GetService<IResources>();
            assetloader = GameObject.FindObjectOfType<AssetLoader>();
            base.Start();
            ApplicationContext context = Context.GetApplicationContext();
            _resources = context.GetService<IResources>();
            assetloader = GameObject.FindObjectOfType<AssetLoader>();

            _gameobjectLoaded = assetloader.LoadGameObjectAsync(path, mTrackableBehaviour.transform);

            Dispatcher.Initialize(); 
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        protected override void OnTrackingFound()
        {
<<<<<<< HEAD
            foreach (Transform a in mTrackableBehaviour.transform)
            {
                a.gameObject.SetActive(true);
=======

            inforBtn = GameObject.FindGameObjectsWithTag("infor");
            NGUITools.SetActive(objectName, true);

            //SpawnModel();
            //nếu đã purchase thì vào phần này
            if (ActiveManager.IsActiveOfflineOk("B"))
            {
                //SpawnModel();
                ShowModelsOnScreen();
            }
            // nếu chưa purchase thì vào phần này
            else
            {
                //nếu là 3 trang đầu thì cho xem model
                if (mTrackableBehaviour.TrackableName == "Solarsystem_scaled" || mTrackableBehaviour.TrackableName == "Sun_scaled" || mTrackableBehaviour.TrackableName == "Mercury_scaled")
                {
                    //SpawnModel();
                    ShowModelsOnScreen();
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

            ShowModelsOnScreen();
            NGUITools.SetActive(objectName, true);

            //SpawnModel();
            //nếu đã purchase thì vào phần này
            //if (ActiveManager.IsActiveOfflineOk("B"))
            //{
            //    //SpawnModel();
            //    ShowModelsOnScreen();
            //}
            //// nếu chưa purchase thì vào phần này
            //else
            //{
            //    //nếu là 3 trang đầu thì cho xem model
            //    if (mTrackableBehaviour.TrackableName == "Solarsystem_scaled" || mTrackableBehaviour.TrackableName == "Sun_scaled" || mTrackableBehaviour.TrackableName == "Mercury_scaled")
            //    {
            //        //SpawnModel();
            //        ShowModelsOnScreen();
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
<<<<<<< HEAD
            //foreach (Transform go in mTrackableBehaviour.transform)
            //{
            //    Destroy(go.gameObject);
            //}

            foreach (Transform a in mTrackableBehaviour.transform)
=======
            foreach (Transform go in mTrackableBehaviour.transform)
>>>>>>> origin/space
            {
                a.gameObject.SetActive(false);
            }
            NGUITools.SetActive(objectName, false);

            ClearKeyLocalization();
            base.OnTrackingLost();
        }

        private void ShowModelsOnScreen()
        {
<<<<<<< HEAD
            nameTargetSpace = mTrackableBehaviour.TrackableName.Substring(0, mTrackableBehaviour.TrackableName.Length - 7);
            nameTargetSpace.ToLower();
            ChangeKeyLocalization();
        }


        //        public void Execute()
        //        {
        //#if UNITY_WSA && !UNITY_EDITOR
        //        System.Threading.Tasks.Task t = new System.Threading.Tasks.Task(Augment);
        //#else
        //            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(Augment));
        //#endif
        //            t.Start();
        //        }

        //        private void Augment()
        //        {

        //            GameObject go = null;
        //            Task.WhenAll(Task.Run(() =>
        //            {
        //                Debug.Log("<color=turquoise>In background thread</color>");
        //                Task.RunInMainThread(() =>
        //                {
        //                    //assetloader.LoadGameObjectAsync(path, mTrackableBehaviour.transform);
        //                    //_loadSoundbundle.PlayNameSound(tagSound);
        //                });
        //            })).ContinueInMainThreadWith(task =>
        //            {
        //                nameTargetSpace = mTrackableBehaviour.TrackableName.Substring(0, mTrackableBehaviour.TrackableName.Length - 7);
        //                nameTargetSpace.ToLower();
        //                ChangeKeyLocalization();
        //            });
        //        }
=======
            if (IsTargetEmpty())
            {
                Execute();
            }
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
>>>>>>> origin/space

        private void ClearAllOtherTargetContents()
        {
            foreach (Transform target in mTrackableBehaviour.transform.parent.transform)
            {
                if (target != transform && target.childCount > 0)
                    Destroy(target.GetChild(0).gameObject);
            }
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
<<<<<<< HEAD
=======

                objectName.GetComponentInChildren<UILabel>().text = Localization.Get(nameKeySpace);
                objectInfo.GetComponent<UILabel>().text = Localization.Get(inforKeySpace);
>>>>>>> origin/space

                objectName.GetComponentInChildren<UILabel>().text = Localization.Get(nameKeySpace);
                objectInfo.GetComponent<UILabel>().text = Localization.Get(inforKeySpace);
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

<<<<<<< HEAD
        /// <summary>
        /// Check if there is no child on the target
        /// </summary>
        /// <returns></returns>
        private bool IsTargetEmpty()
        {
            return mTrackableBehaviour.transform.childCount <= 0;
=======
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
>>>>>>> origin/space
        }

        private IEnumerator InstantiationAsycnModels(GameObject go)
        {
            yield return new WaitForSeconds(0.2f);
            Instantiate(go, mTrackableBehaviour.transform);
            //assetloader = GameObject.FindObjectOfType<AssetLoader>();
            //InstantiationAsync.InstantiateAsync(go, mTrackableBehaviour.transform, 300);
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
    }
}