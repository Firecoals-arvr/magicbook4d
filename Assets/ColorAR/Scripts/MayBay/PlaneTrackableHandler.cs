﻿using System.Collections;
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
