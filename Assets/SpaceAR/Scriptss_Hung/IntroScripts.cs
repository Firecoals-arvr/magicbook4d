using Firecoals.AssetBundles.Sound;
using Firecoals.Augmentation;
using Firecoals.MagicBook;
using Firecoals.Threading.Tasks;
using Loxodon.Framework.Bundles;
using Loxodon.Framework.Contexts;
using UnityEngine;
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

        private GameObject _gameobjectLoaded;

        /// <summary>
        /// anim để chạy animation intro lúc tracking found models
        /// </summary>
        private Animator anim;

        //private GameObject[] inforBtn;
        private bool checkOpen;

        private AudioSource _audiosrc;

        protected override void Start()
        {
            base.Start();
            ApplicationContext context = Context.GetApplicationContext();
            _resources = context.GetService<IResources>();
            assetloader = GameObject.FindObjectOfType<AssetLoader>();
            _loadSoundbundle = GameObject.FindObjectOfType<LoadSoundbundles>();
            if (ActiveManager.IsActiveOfflineOk(ThemeController.instance.Theme) ||
                mTrackableBehaviour.name == "Solarsystem_scaled"
                || mTrackableBehaviour.name == "Sun_scaled"
                || mTrackableBehaviour.name == "Mercury_scaled")
            {
                _gameobjectLoaded = assetloader.LoadGameObjectAsync(path, mTrackableBehaviour.transform);
            }
                

            Dispatcher.Initialize();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        protected override void OnTrackingFound()
        {
            _audiosrc = gameObject.GetComponentInChildren<AudioSource>();
            foreach (Transform a in mTrackableBehaviour.transform)
            {
                a.gameObject.SetActive(true);
            }

            if (IsTargetEmpty())
            {
                ShowModelsOnScreen();
                PlayAudioOfObject();
            }

            //nếu đã purchase thì vào phần này
            //if (ActiveManager.IsActiveOfflineOk("B"))
            //{
            //    //SpawnModel();
            //    ShowModelsOnScreen();
            //    AutoTriggerInforButton();
            //}
            //// nếu chưa purchase thì vào phần này
            //else
            //{
            //    //nếu là 3 trang đầu thì cho xem model
            //    if (mTrackableBehaviour.TrackableName == "Solarsystem_scaled" || mTrackableBehaviour.TrackableName == "Sun_scaled" || mTrackableBehaviour.TrackableName == "Mercury_scaled")
            //    {
            //        //SpawnModel();
            //        ShowModelsOnScreen();
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

        /// <summary>
        /// hiển thị model lên màn hình khi chiếu cam vào tranh
        /// </summary>
        private void ShowModelsOnScreen()
        {
            if (IsTargetEmpty())
            {
                _loadSoundbundle.PlayNameSound(tagSound);
                PlayAnimIntro();
                nameTargetSpace = mTrackableBehaviour.TrackableName.Substring(0, mTrackableBehaviour.TrackableName.Length - 7);
                nameTargetSpace.ToLower();
                ChangeKeyLocalization();
            }
        }

        /// <summary>
        /// bật nhạc khi có model
        /// </summary>
        private void PlayAudioOfObject()
        {
            if (_audiosrc != null)
            {
                _audiosrc.Play();
            }
        }

        /// <summary>
        /// tắt nhạc khi model bị tắt đi
        /// </summary>
        private void StopAudioOfObject()
        {
            if (_audiosrc != null)
            {
                _audiosrc.Stop();
            }
        }

        protected override void OnTrackingLost()
        {
            //foreach (Transform go in mTrackableBehaviour.transform)
            //{
            //    Destroy(go.gameObject);
            //}
            foreach (Transform a in mTrackableBehaviour.transform)
            {
                a.gameObject.SetActive(false);
            }

            NGUITools.SetActive(objectName, false);

            ClearKeyLocalization();
            FirecoalsSoundManager.StopAll();
            StopAudioOfObject();
            base.OnTrackingLost();
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
            //GameObject go = null;
            Task.WhenAll(Task.Run(() =>
            {
                Debug.Log("<color=turquoise>In background thread</color>");
                Task.RunInMainThread(() =>
                {
                    //assetloader.LoadGameObjectAsync(path, mTrackableBehaviour.transform);
                    //_loadSoundbundle.PlayNameSound(tagSound);
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

        private void PlayAnimIntro()
        {
            if (gameObject.GetComponentInChildren<Animator>() != null)
            {
                anim = gameObject.GetComponentInChildren<Animator>();
                anim.SetTrigger("Intro");
            }
        }

        //private bool _cached = false;

        //private void CloneModels(GameObject go)
        //{
        //    //StartCoroutine(InstantiationAsycnModels(go));
        //    //InstantiationAsync.InstantiateAsync(go, 100);
        //    PlayAnimIntro();

        //    nameTargetSpace = mTrackableBehaviour.TrackableName.Substring(0, mTrackableBehaviour.TrackableName.Length - 7);
        //    nameTargetSpace.ToLower();
        //    ChangeKeyLocalization();
        //}

        //private IEnumerator InstantiationAsycnModels(GameObject go)
        //{
        //    yield return new WaitForSeconds(0.2f);
        //    if (go != null)
        //    {
        //        Instantiate(go, mTrackableBehaviour.transform);
        //    }
        //    //_loadSoundbundle.PlayNameSound(tagSound);
        //}

        /// <summary>
        /// Check if there is no child on the target
        /// </summary>
        /// <returns></returns>
        private bool IsTargetEmpty()
        {
            return mTrackableBehaviour.transform.childCount <= 0;
        }



        /// <summary>
        /// thông tin thành phần con của hành tinh
        /// </summary>
        [Header("Panel information")]
        [SerializeField] private Animator panelInforAnim;

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