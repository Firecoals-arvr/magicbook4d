using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firecoals.Augmentation;
using System;
using Firecoals.AssetBundles;

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
        public string objName;

        /// <summary>
        /// đường dẫn của object trong thư mục
        /// </summary>
        public string path;

        private AssetHandler assethandler;
        private InstantiationAsync asyncObj;
        private AssetBundlesLoader loader;
        protected override void Start()
        {
            base.Start();
            asyncObj = GameObject.FindObjectOfType<InstantiationAsync>();
            assethandler = new AssetHandler(mTrackableBehaviour.transform);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        protected override void OnTrackingFound()
        {
            var statTime = DateTime.Now;
            var go = assethandler.CreateUnique(objName, path);
            Debug.Log("load in: " + (DateTime.Now - statTime).Milliseconds);
            if (go != null)
            {
                var startTime = DateTime.Now;
                //Instantiate(go, mTrackableBehaviour.transform);
                asyncObj.InstantiateAsync(go, mTrackableBehaviour.transform, 50);
                loader.LoadAsset("Assets/SpaceAR/MyPrefabs/AllPlanet/solarSystem.prefab", mTrackableBehaviour.transform);
                Debug.Log("instantiate in: " + (DateTime.Now - startTime).Milliseconds);

                //asyncObj.InstantiateAsync(go, mTrackableBehaviour.transform, delayTimeInMillisecond: 1);
            }
            //RunAnimationIntro();
            base.OnTrackingFound();
        }

        protected override void OnTrackingLost()
        {
            assethandler?.ClearAll();
            assethandler?.Content.ClearAll();

            foreach (Transform go in mTrackableBehaviour.transform)
            {
                //asyncObj.DestroyAfterSpawnInSecond(go.gameObject, 0.5f);
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
