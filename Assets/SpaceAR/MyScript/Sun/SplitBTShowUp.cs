using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireCoals.Space
{
    public class SplitBTShowUp : DefaultTrackableEventHandler
    {
        public GameObject panelBt;
        protected override void Start()
        {
            base.Start();
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
        protected override void OnTrackingFound()
        {
            base.OnTrackingFound();
            NGUITools.SetActive(panelBt, true);
        }
        protected override void OnTrackingLost()
        {
            base.OnTrackingLost();
            NGUITools.SetActive(panelBt, false);
        }
    }
}