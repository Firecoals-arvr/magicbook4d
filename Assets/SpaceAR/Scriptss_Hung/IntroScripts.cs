using System.Collections;
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
        /// tên object trong asset bundles
        /// </summary>
        //public string objName;

        /// <summary>
        /// đường dẫn của object trong thư mục
        /// </summary>
        public string path;

        /// <summary>
        /// đường dẫn sound
        /// </summary>
        private AssetHandler _assethandler;
        private TestLoadSoundbundles _loadSoundBundles;
        //public int soundNumber;

        private LoadSoundFromBundle _soundFromBundles;

        private AssetLoader assetloader;

        private IResources _resources;
        protected override void Start()
        {
            base.Start();
            assetloader = GameObject.FindObjectOfType<AssetLoader>();
            _assethandler = new AssetHandler(mTrackableBehaviour.transform);
            _loadSoundBundles = GameObject.FindObjectOfType<TestLoadSoundbundles>();
            _soundFromBundles = GameObject.FindObjectOfType<LoadSoundFromBundle>();
            ApplicationContext context = Context.GetApplicationContext();
            this._resources = context.GetService<IResources>();
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
            //_loadSoundBundles.PlayNameSound(soundNumber);
            //_soundFromBundles.PlayObjectSound();
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

    }
}
