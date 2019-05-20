﻿using Firecoals.Augmentation;
using Firecoals.MagicBook;
using Loxodon.Framework.Bundles;
using Loxodon.Framework.Contexts;
using UnityEngine;
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

        private GameObject _gameobjectLoaded;

        /// <summary>
        /// Các imagetargets
        /// </summary>
        private GameObject[] _originalObjectTransform;

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

            //if (ActiveManager.IsActiveOfflineOk(ActiveManager.NameToProjectID(ThemeController.instance.Theme))
            //     || mTrackableBehaviour.TrackableName == "Solarsystem_scaled"
            //     || mTrackableBehaviour.TrackableName == "Sun_scaled"
            //     || mTrackableBehaviour.TrackableName == "Mercury_scaled")
            //{
                _gameobjectLoaded = assetloader.LoadGameObjectAsync(path, mTrackableBehaviour.transform);
            //}

            Dispatcher.Initialize();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        protected override void OnTrackingFound()
        {
           
            foreach (Transform a in mTrackableBehaviour.transform)
            {
                a.gameObject.SetActive(true);
            }
            NGUITools.SetActive(objectName, true);
            ShowModelsOnScreen();
            ResetTranformGame1();
            

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
            base.OnTrackingLost();
        }

        private void ShowModelsOnScreen()
        {
            if (IsTargetEmpty())
            {
                nameTargetSpace = mTrackableBehaviour.TrackableName.Substring(0, mTrackableBehaviour.TrackableName.Length - 7);
                nameTargetSpace.ToLower();
                ChangeKeyLocalization();
            }
        }

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

                objectName.GetComponentInChildren<UILabel>().text = Localization.Get(nameKeySpace);
                objectInfo.GetComponent<UILabel>().text = Localization.Get(inforKeySpace);
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

        /// <summary>
        /// nếu là Game1 thì reset lại transform lúc xuất hiện
        /// </summary>
        private void ResetTranformGame1()
        {
            if (mTrackableBehaviour.name == "Game1_scaled")
            {
                GetOriginalTransform();
            }
        }

        /// <summary>
        /// Lấy transform ban đầu từ khi instantiate object ra
        /// </summary>
        private void GetOriginalTransform()
        {
            _originalObjectTransform = GameObject.FindGameObjectsWithTag("Leanscale");
            foreach (var a in _originalObjectTransform)
            {
                if (a != null && a.activeSelf)
                {
                    a.transform.localPosition = Vector3.zero;
                    a.transform.localRotation = new Quaternion(0, 0, 0, 0);
                    a.transform.localScale = _originalLocalScale;
                }
            }
        }
    }
}