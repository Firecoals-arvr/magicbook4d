﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firecoals.Augmentation;
using System;
using Firecoals.AssetBundles;
using Loxodon.Framework.Bundles;
using Loxodon.Framework.Contexts;
using Firecoals.AssetBundles.Sound;

namespace Firecoals.Space
{
    /// <summary>
    /// class dùng thay cho DefaultTrackableEventHandler
    /// </summary>
    public class IntroScripts : DefaultTrackableEventHandler
    {
        /// <summary>
        /// đường dẫn của object trong thư mục
        /// </summary>
        public string path;

        /// <summary>
        /// đường dẫn sound
        /// </summary>
        private AssetHandler _assethandler;
        //private LoadSoundFromBundle _soundFromBundles;
        private AssetLoader assetloader;
        private IResources _resources;

        /// <summary>
        /// thông tin chung về object
        /// </summary>
        [SerializeField] UILocalize objectInfo;

        /// <summary>
        /// thông tin về các thành phần của object (nếu có)
        /// </summary>
        [SerializeField] UILocalize componentInfo;

        /// <summary>
        /// tên của object
        /// </summary>
        [SerializeField] UILocalize objectName;

        /// <summary>
        /// biến để so sánh tên object với key localization
        /// </summary>
        private string st;

        /// <summary>
        /// key cho name object
        /// </summary>
        [Header("Name key")]
        public string st1;

        /// <summary>
        /// key cho info object
        /// </summary>
        [Header("Information key")]
        public string st2;

        protected override void Start()
        {
            base.Start();
            assetloader = GameObject.FindObjectOfType<AssetLoader>();
            _assethandler = new AssetHandler(mTrackableBehaviour.transform);
            //_soundFromBundles = GameObject.FindObjectOfType<LoadSoundFromBundle>();
            ApplicationContext context = Context.GetApplicationContext();
            this._resources = context.GetService<IResources>();

            st = mTrackableBehaviour.TrackableName.Substring(0, mTrackableBehaviour.TrackableName.Length - 7);
            st.ToLower();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        protected override void OnTrackingFound()
        {
            var statTime = DateTime.Now;
            GameObject go1 = assetloader.LoadGameObjectAsync(path);
            //GameObject go = _resources.LoadAsset<GameObject>(path) as GameObject;
            Debug.Log("load in: " + (DateTime.Now - statTime).Milliseconds);
            if (go1 != null)
            {
                var startTime = DateTime.Now;
                //Instantiate(go, mTrackableBehaviour.transform);
                GameObject.Instantiate(go1, mTrackableBehaviour.transform);
                Debug.Log("instantiate in: " + (DateTime.Now - startTime).Milliseconds);
            }

            ChangeKeyLocalization();
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

            //ClearKeyLocalization();
            base.OnTrackingLost();
        }

        private void RunAnimationIntro()
        {
            if (this.gameObject.transform.GetChild(0).GetComponent<Animation>() != null)
            {
                this.gameObject.transform.GetChild(0).GetComponent<Animation>().Play("intro");
            }
            else
            {
                Debug.LogWarning("This object hasn't intro animation.");
            }
        }

        private void ChangeKeyLocalization()
        {
            if (st1.Contains(st) && st2.Contains(st))
            {
                objectName.key = st1;
                objectInfo.key = st2;

                objectName.GetComponent<UILabel>().text = Localization.Get(st1);
                objectInfo.GetComponent<UILabel>().text = Localization.Get(st2);
            }
        }

        private void ClearKeyLocalization()
        {
            //objectName.GetComponent<UILabel>().text = Localization.Get(string.Empty);
            //objectInfo.GetComponent<UILabel>().text = Localization.Get(string.Empty);
            objectName.GetComponent<UILocalize>().key = string.Empty;
            objectInfo.GetComponent<UILocalize>().key = string.Empty;
        }
    }
}