using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireCoals.Space
{
    public class IntroPage19 : DefaultTrackableEventHandler
    {
        GameObject alienBtn;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            alienBtn = GameObject.Find("Page 19/Alienware/Panel button/Dance & Talk");
        }

        protected override void OnTrackingFound()
        {
            base.OnTrackingFound();
            NGUITools.SetActive(alienBtn, true);
            this.gameObject.transform.GetChild(0).GetComponent<Animation>().Play("intro");
        }

        protected override void OnTrackingLost()
        {
            NGUITools.SetActive(alienBtn, false);
            base.OnTrackingLost();
        }
    }
}