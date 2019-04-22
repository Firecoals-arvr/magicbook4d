using Firecoals.AssetBundles.Sound;
using Firecoals.Augmentation;
using Firecoals.Threading.Tasks;
using Loxodon.Framework.Bundles;
using System.Collections;
using UnityEngine;
using Dispatcher = Firecoals.Threading.Dispatcher;

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
        public string bundleName;

        /// <summary>
        /// đường dẫn của object trong thư mục
        /// </summary>
        public string bundlePath;

        /// <summary>
        /// đường dẫn sound
        /// </summary>
        ///
        public float TimeStartEffect;
        private AssetLoader assetLoader { get; set; }
        private LoadSoundbundles _loadsoundbundles;
        private AudioSource audio;
        private IResources _resources;
        public string tagInfo;
        public string tagSound;
        public string tagName;
        private GameObject _textInfo;
        /// <summary>
        /// key cho name object
        /// </summary>
        [Header("Name key Load Bundles")]
        public string TextInfoName;
        /// <summary>
        /// key cho info object
        /// <summary>
        /// Key để load name và info của models
        /// </summary>
        private string _nameModelTracking;

        private UILabel InformationLabel;
        [Header("Name Animal")]
        public string nameAnimal;
        public UILabel objName;
        protected override void Start()
        {
            base.Start();
            audio = gameObject.GetComponent<AudioSource>();
            _loadsoundbundles = GameObject.FindObjectOfType<LoadSoundbundles>();
            //PlayerPrefs.SetString("AnimalLanguage", "EN");
            Dispatcher.Initialize();
            assetLoader = GameObject.FindObjectOfType<AssetLoader>();
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            //   Debug.Log("Destroy" + mTrackableBehaviour.name);
            foreach (Transform go in mTrackableBehaviour.transform)
            {
                Destroy(go.gameObject);
            }
        }

        private IEnumerator StarEffect(GameObject go)
        {
            RandomEffect.Instance.Onfound(TimeStartEffect);
            yield return new WaitForSeconds(0.1f);
            //GameObject.Instantiate(go, mTrackableBehaviour.transform);
            InstantiationAsync.InstantiateAsync(go, mTrackableBehaviour.transform, 300);
            _loadsoundbundles.PlaySound(tagSound);
            _loadsoundbundles.PlayName(tagName);
            StartCoroutine("LoadNameAnimal");
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
            Task loadTask= Dispatcher.instance.TaskToMainThread(() =>
            {                                                                              
                go = assetLoader.LoadGameObjectSync(bundleName, bundlePath);
                //_cached = true;
                Debug.Log("<color=red> GameObject created in background thread: " + go.name + "<color>");

            });
            loadTask.ContinueInMainThreadWith((task) =>
            {
                if (task.IsCompleted)
                {
                    if (go != null)
                        LoadModelBundles(go);
                }
            });
        }
        protected override void OnTrackingFound()
        {
            ClearAllOtherTargetContents();
            //if (IsTargetEmpty() && !_cached)
            //{
            //    CachingArContents();
            //}
            //if (IsTargetEmpty() && _cached)
            //{
            //    LoadingCache();
            //}
            if(IsTargetEmpty())
                Execute();
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
            Debug.Log("<color=orange>mTrackableBehaviour</color>" + mTrackableBehaviour);
            //LoadModelBundles(go);
            base.OnTrackingFound();

        }

        public void LoadModelBundles(GameObject go)
        {
            StartCoroutine(StarEffect(go));
            _textInfo = GameObject.Find("UI Root/PanelButtons/PanelInfor/Scroll View/Info");
            if (_textInfo != null)
            {
                ChangeKeyAnimalLocalization();
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
        private void EnableAllChildOfTheTarget()
        {
            foreach (Transform go in mTrackableBehaviour.transform)
            {
                go.gameObject.SetActive(true);
            }
        }
        private void DesableAllChildOfTheTarget()
        {
            foreach (Transform go in mTrackableBehaviour.transform)
            {
                go.gameObject.SetActive(false);
            }
        }
        protected override void OnTrackingLost()
        {
            FirecoalsSoundManager.StopAll();
            RandomEffect.Instance.Onlost();
            //   Debug.Log("Destroy" + mTrackableBehaviour.name);
            foreach (Transform go in mTrackableBehaviour.transform)
            {
                Destroy(go.gameObject);
            }
            //if (!IsTargetEmpty())
            //{
            //    DesableAllChildOfTheTarget();
            //}

            // DisActive TextNameAnimal trên scene
            NGUITools.SetActive(objName.gameObject, false);
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
        }
        private void ClearKeyLocalization()
        {
            if (_textInfo != null)
            {
                _textInfo.GetComponent<UILocalize>().key = string.Empty;
            }
        }
        // hiển thị text trên scene khi load sound name
        public IEnumerator LoadNameAnimal()
        {
            objName.text = nameAnimal;
            NGUITools.SetActive(objName.gameObject, true);
            yield return new WaitForSeconds(2);
            NGUITools.SetActive(objName.gameObject, false);
        }
    }
}
