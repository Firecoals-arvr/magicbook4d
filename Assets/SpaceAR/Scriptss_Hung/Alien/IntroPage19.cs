using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firecoals.Augmentation;

namespace FireCoals.Space
{
    public class IntroPage19 : DefaultTrackableEventHandler
    {
        GameObject alienBtn;
        private AssetHandler assetHandler;
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            assetHandler = new AssetHandler(mTrackableBehaviour.transform);
            //alienBtn = GameObject.Find("Page 19/Alienware/Panel button/Dance & Talk");
        }

        protected override void OnTrackingFound()
        {
            base.OnTrackingFound();
            //NGUITools.SetActive(alienBtn, true);
            this.gameObject.transform.GetChild(0).GetComponent<Animation>().Play("intro");
        }

        protected override void OnTrackingLost()
        {
            NGUITools.SetActive(alienBtn, false);
            base.OnTrackingLost();
        }
    }
}