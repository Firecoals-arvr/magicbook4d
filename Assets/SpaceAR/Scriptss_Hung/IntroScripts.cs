﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firecoals.Augmentation;

namespace Firecoals.Space
{
    /// <summary>
    /// class dùng thay cho DefaultTrackableEventHandler
    /// </summary>
    public class IntroScripts : DefaultTrackableEventHandler
    {
        public string objName;
        public string path;
        private AssetHandler assethandler;
        
        protected override void Start()
        {
            
            base.Start();
            assethandler = new AssetHandler(mTrackableBehaviour.transform);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        protected override void OnTrackingFound()
        {
            //if (this.gameObject.transform.GetChild(0).GetComponent<Animation>() != null)
            //{
            //    this.gameObject.transform.GetChild(0).GetComponent<Animation>().Play("intro");
            //}
            //else
            //{
            //    Debug.LogWarning("This object hasn't intro animation.");
            //}

            var go = assethandler.CreateUnique(objName, path);
            if (go != null)
            {
                Instantiate(go,mTrackableBehaviour.transform);
            }
            base.OnTrackingFound();
        }

        protected override void OnTrackingLost()
        {
            // Destroy(go);
            base.OnTrackingLost();
            
        }
    }
}
