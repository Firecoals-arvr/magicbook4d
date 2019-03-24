using System.Collections;
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
        /// <summary>
        /// tên object trong asset bundles
        /// </summary>
        public string objName;

        /// <summary>
        /// đường dẫn của object trong thư mục
        /// </summary>
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
            if (this.gameObject.transform.GetChild(0).GetComponent<Animation>() != null)
            {
                this.gameObject.transform.GetChild(0).GetComponent<Animation>().Play("intro");
            }
            else
            {
                Debug.LogWarning("This object hasn't intro animation.");
            }

            var go = assethandler.CreateUnique(objName, path);
            if (go != null)
            {
                Instantiate(go, mTrackableBehaviour.transform);
            }
            base.OnTrackingFound();
        }

        protected override void OnTrackingLost()
        {
            assethandler?.ClearAll();
            assethandler?.Content.ClearAll();
            foreach (Transform go in mTrackableBehaviour.transform)
            {
                Destroy(go.gameObject);
            }
            base.OnTrackingLost();
        }
    }
}
