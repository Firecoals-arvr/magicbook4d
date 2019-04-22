using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firecoals.Augmentation;
using System;
using Firecoals.AssetBundles;
using Loxodon.Framework.Bundles;
using Loxodon.Framework.Contexts;
using UnityEngine.SceneManagement;
using Vuforia;

namespace Firecoals.Space
{
    /// <summary>
    /// class dùng thay cho DefaultTrackableEventHandler
    /// </summary>
    public class GameTracking : DefaultTrackableEventHandler
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
        /// anim để chạy animation intro lúc tracking found models
        /// </summary>
        Animator anim;

        private GameObject[] inforBtn;
        bool checkOpen;


        protected override void Start()
        {
            ApplicationContext context = Context.GetApplicationContext();
            this._resources = context.GetService<IResources>();
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
            NGUITools.SetActive(objectName, true);

            SpawnModel();
            //nếu đã purchase thì vào phần này
            //if (ActiveManager.IsActiveOfflineOk("B"))
            //{
            //    SpawnModel();
            //}
            //// nếu chưa purchase thì vào phần này
            //else
            //{
            //    //nếu là 3 trang đầu thì cho xem model
            //    if (mTrackableBehaviour.TrackableName == "Solarsystem_scaled" || mTrackableBehaviour.TrackableName == "Sun_scaled" || mTrackableBehaviour.TrackableName == "Mercury_scaled")
            //    {
            //        SpawnModel();
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
            base.OnTrackingLost();
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