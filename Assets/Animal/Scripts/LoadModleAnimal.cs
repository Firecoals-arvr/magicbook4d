﻿using Firecoals.AssetBundles.Sound;
using Firecoals.Augmentation;
using Firecoals.Threading.Tasks;
using Loxodon.Framework.Bundles;
using System.Collections;
using Firecoals.MagicBook;
using UnityEngine;
using Dispatcher = Firecoals.Threading.Dispatcher;
using UnityEngine.SceneManagement;

namespace Firecoals.Animal
{
    /// <summary>
    /// Author : Quang 
    /// Edit : Cường
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
        private IResources _resources;
        private GameObject gameObjectToload;
        private bool attached = false;
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
        //Biến chứa các ImageTarget để lấy vị trí của animal
        GameObject[] allCreature;
        public Vector3 itemPos;
        // biến check để ko bị load sound 2 lần
        private bool playSound;
        protected override void Start()
        {
            base.Start();
            _loadsoundbundles = GameObject.FindObjectOfType<LoadSoundbundles>();
            //PlayerPrefs.SetString("AnimalLanguage", "EN");
            //Dispatcher.Initialize();
            assetLoader = GameObject.FindObjectOfType<AssetLoader>();
            if (ActiveManager.IsActiveOfflineOk(ActiveManager.NameToProjectID(ThemeController.instance.Theme))
                || mTrackableBehaviour.TrackableName.Equals("1_free_lion")
                || mTrackableBehaviour.TrackableName.Equals("2_free_elephant")
                || mTrackableBehaviour.TrackableName.Equals("3_free_gorilla"))
            {
                gameObjectToload = assetLoader.LoadGameObjectAsync(bundlePath, mTrackableBehaviour.transform);
            }

            playSound = true;
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
            //InstantiationAsync.InstantiateAsync(go, mTrackableBehaviour.transform, 300);
            //go.transform.parent = this.transform;
            //Cho load Effect thì mở comment sound
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
            Task loadTask = Dispatcher.instance.TaskToMainThread(() =>
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
            //ClearAllOtherTargetContents();
            //gameObjectToload.SetActive(true);
            EnableAllChildOfTheTarget();
            if (IsTargetEmpty() && playSound)
            {
                LoadModelBundles(gameObjectToload);
                playSound = false;
            }
            //Debug.Log("<color=orange>mTrackableBehaviour</color>" + mTrackableBehaviour);
            base.OnTrackingFound();
        }
        protected override void OnTrackingLost()
        {
            FirecoalsSoundManager.StopAll();
            RandomEffect.Instance.Onlost();
            // DisActive TextNameAnimal trên scene
            NGUITools.SetActive(objName.gameObject, false);
            DesableAllChildOfTheTarget();
            playSound = true;
            base.OnTrackingLost();
        }
        public void LoadModelBundles(GameObject go)
        {
            StartCoroutine(StarEffect(go));

            //load text lên bảng thông tin
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
            return mTrackableBehaviour.transform.childCount >= 0;
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
            if (ActiveManager.IsActiveOfflineOk(ActiveManager.NameToProjectID(ThemeController.instance.Theme))
                || mTrackableBehaviour.TrackableName.Equals("1_free_lion")
                || mTrackableBehaviour.TrackableName.Equals("2_free_elephant")
                || mTrackableBehaviour.TrackableName.Equals("3_free_gorilla"))
            {
                foreach (Transform go in mTrackableBehaviour.transform)
                {
                    go.gameObject.SetActive(true);
                }
            }
            else
            {
                PopupManager.PopUpDialog("", "Bạn cần kích hoạt để sử dụng hết các tranh", "OK", "Yes", "No", PopupManager.DialogType.YesNoDialog, () =>
                {
                    SceneManager.LoadScene("Activate", LoadSceneMode.Single);
                });
            }
            ResetPosition();
            ResetMove();
            //Đoạn bên dưới để load sound + text trong trường hợp load bundles mà ko load effect lúc xuất hiện
            //_loadsoundbundles.PlaySound(tagSound);
            //_loadsoundbundles.PlayName(tagName);
            //_textInfo = GameObject.Find("UI Root/PanelButtons/PanelInfor/Scroll View/Info");
            //if (_textInfo != null)
            //{
            //    ChangeKeyAnimalLocalization();
            //}
        }
        private void DesableAllChildOfTheTarget()
        {
            foreach (Transform go in mTrackableBehaviour.transform)
            {
                go.GetComponentInChildren<Animation>().Stop();
                go.gameObject.SetActive(false);
            }
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
            objName.gameObject.GetComponent<UILocalize>().key = nameAnimal;
            objName.text = Localization.Get(nameAnimal);
            _textInfo.GetComponent<UILocalize>().key = TextInfoName;
            _textInfo.GetComponent<UILabel>().text = Localization.Get(TextInfoName);
        }
        private void ClearKeyLocalization()
        {
            if (_textInfo != null && objName != null)
            {
                _textInfo.GetComponent<UILocalize>().key = string.Empty;
                objName.GetComponent<UILocalize>().key = string.Empty;
            }
        }
        // hiển thị text trên scene khi load sound name
        public IEnumerator LoadNameAnimal()
        {
            NGUITools.SetActive(objName.gameObject, true);
            yield return new WaitForSeconds(2);
            NGUITools.SetActive(objName.gameObject, false);
        }
        // reset vị trí của animal khi tracking found
        void ResetPosition()
        {
            allCreature = GameObject.FindGameObjectsWithTag("ImageTarget");
            foreach (GameObject creature in allCreature)
            {
                if (creature.transform.childCount > 0 && creature.transform.GetChild(0).gameObject.activeSelf)
                {
                    GameObject go = GameObject.FindGameObjectWithTag("Creature");
                    if(go!=null)
                    {
                        go.transform.localPosition = Vector3.zero;
                        go.transform.localRotation = new Quaternion(0, 180, 0, 0);
                        go.GetComponent<Animation>().Play();
                        GameObject go1 = GameObject.FindGameObjectWithTag("Item");
                        go1.transform.localPosition = itemPos;
                        go1.transform.localRotation = new Quaternion(0, 180, 0, 0);
                    }

                }
            }
            transform.GetChild(0).localScale = Vector3.one;
        }
        //reset animal ko cho move khi tracking found lại
        void ResetMove()
        {
            allCreature = GameObject.FindGameObjectsWithTag("ImageTarget");
            foreach (GameObject creature in allCreature)
            {
                Debug.LogWarning("creature");
                if (creature.transform.childCount > 0 && creature.transform.GetChild(0).gameObject.activeSelf)
                {
                    switch (mTrackableBehaviour.TrackableName)
                    {
                        case "1_free_lion":
                            creature.GetComponentInChildren<LionMove>().CanMove = false;
                            break;
                        case "17_eagle":
                            creature.GetComponentInChildren<EagleMove>().CanMove = false;
                            break;
                        case "20_wolf":
                            creature.GetComponentInChildren<WolfMove>().CanMove = false;
                            break;
                        case "12_dolphin":
                            creature.GetComponentInChildren<LionController>().CanMove = false;
                            break;
                        case "13_kangaru":
                            creature.GetComponentInChildren<LionController>().CanMove = false;
                            break;
                        default:
                            creature.GetComponentInChildren<DefaultController>().CanMove = false;
                            break;
                    }
                }
            }
        }
    }
}
