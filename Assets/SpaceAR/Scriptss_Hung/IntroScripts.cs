using Firecoals.AssetBundles.Sound;
using Firecoals.Augmentation;
using Firecoals.MagicBook;
using Firecoals.Threading.Tasks;
using Loxodon.Framework.Bundles;
using Loxodon.Framework.Contexts;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        //public string bundleName;

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

        /// <summary>
        /// check lúc OnTrackingFound() thì cho chạy âm thanh, OnTrackingLost() thì tắt
        /// </summary>
        private bool _playSound;

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
            }
            else
            {
                Debug.Log("NotPurchase");
                PopupManager.PopUpDialog("", "Bạn cần kích hoạt để sử dụng hết các tranh", "OK", "Yes", "No", PopupManager.DialogType.YesNoDialog, () =>
                {
                    SceneManager.LoadScene("Activate", LoadSceneMode.Single);
                });
            }

            ChangeAnim.checkOpen = false;
            ShowModelsOnScreen();

            base.OnTrackingFound();
        }

        /// <summary>
        /// hiển thị model lên màn hình khi chiếu cam vào tranh
        /// </summary>
        private void ShowModelsOnScreen()
        {
            NGUITools.SetActive(objectName, true);
            if (_playSound)
            {
                _loadSoundbundle.PlayNameSound(tagSound);
            }
            if (_backgroundMusic != string.Empty && _playSound)
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
            foreach (Transform a in mTrackableBehaviour.transform)
            {
                a.gameObject.SetActive(false);
            }

            NGUITools.SetActive(objectName, false);

            ClearKeyLocalization();
            FirecoalsSoundManager.StopAll();
            _playSound = true;

            base.OnTrackingLost();
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
                objectName.GetComponentInChildren<UILocalize>().key = nameKeySpace;
                objectInfo.GetComponent<UILocalize>().key = inforKeySpace;

                objectName.GetComponentInChildren<UILabel>().text = "[b]" + Localization.Get(nameKeySpace) + "[/b]";
                objectInfo.GetComponent<UILabel>().text = Localization.Get(inforKeySpace);
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

        /// <summary>
        /// Check if there is no child on the target
        /// </summary>
        /// <returns></returns>
        private bool IsTargetEmpty()
        {
            return mTrackableBehaviour.transform.childCount <= 0;
        }

        private void PlayAnimIntro()
        {
            ChangeAnim.changAnim = true;
            if (gameObject.GetComponentInChildren<Animator>() != null)
            {
                anim = gameObject.GetComponentInChildren<Animator>();
                //anim.Play("Idle");
                anim.SetTrigger("Intro");
            }
        }
    }
}