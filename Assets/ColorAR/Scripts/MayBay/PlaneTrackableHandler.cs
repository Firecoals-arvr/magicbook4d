using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firecoals.Augmentation;
using Firecoals.AssetBundles.Sound;
using Firecoals.MagicBook;

namespace Firecoals.Color
{
    public class PlaneTrackableHandler : DefaultTrackableEventHandler
    {
        AssetLoader _assetLoader;
        public GameObject renderCam;
        private LoadSoundBundlesColor _loadSoundBundles;
        public string tagSound;
        bool playSound;
        /// <summary>
        /// scale ban ban đầu của object,
        /// các object khác nhau scale ban đầu khác nhau
        /// </summary>
        [Header("Original scale of object")]
        public Vector3 _originalLocalScale;
        protected override void Start()
        {
            base.Start();
            _assetLoader = FindObjectOfType<AssetLoader>();
            //if (ActiveManager.IsActiveOfflineOk(ActiveManager.NameToProjectID(ThemeController.instance.Theme))
            //    || mTrackableBehaviour.TrackableName.Equals("06MAYBAY_OK"))
            //{
            //    _assetLoader.LoadGameObjectAsync("ColorAR/Prefabs/MayBay/MayBay_Group.prefab", mTrackableBehaviour.transform);
            //}
            playSound = true;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }


        protected override void OnTrackingFound()
        {
            _loadSoundBundles = GameObject.FindObjectOfType<LoadSoundBundlesColor>();
            EnableObject();
            GetOriginalTransform();
            if (playSound)
            {
                //_loadSoundBundles.PlaySound(tagSound);
                playSound = false;
            }
            base.OnTrackingFound();
        }
        protected override void OnTrackingLost()
        {
            foreach (Transform trans in mTrackableBehaviour.transform)
            {
                trans.gameObject.SetActive(false);
            }
            playSound = true;
            FirecoalsSoundManager.StopAll();
            base.OnTrackingLost();
        }
        private void GetOriginalTransform()
        {
            GameObject go = mTrackableBehaviour.transform.gameObject.transform.GetChild(0).gameObject;
            go.transform.localScale = _originalLocalScale;
        }
        void EnableObject()
        {
            foreach (Transform trans in mTrackableBehaviour.transform)
            {
                trans.gameObject.SetActive(true);
                trans.GetChild(0).localPosition = new Vector3(0, 0, 0);
                trans.GetChild(0).localRotation = new Quaternion(0, 0, 0, 0);
                trans.GetComponentInChildren<Animator>().Play("Idle", 0, 0f);
                List<RC_Get_Texture> lst = new List<RC_Get_Texture>();
                trans.GetComponentsInChildren<RC_Get_Texture>(true, lst);
                foreach (var child in lst)
                {
                    child.RenderCamera = renderCam.GetComponent<Camera>();
                }
            }
        }
    }
}
