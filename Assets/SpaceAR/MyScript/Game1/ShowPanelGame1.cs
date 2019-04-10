using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    public class ShowPanelGame1 : DefaultTrackableEventHandler
    {
        public GameObject panelGame1;
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
            NGUITools.SetActive(panelGame1, true);
        }
        protected override void OnTrackingLost()
        {
            base.OnTrackingLost();
            NGUITools.SetActive(panelGame1, false);
        }
    }
}
