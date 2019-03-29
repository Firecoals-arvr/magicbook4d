using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    public class CheckGame2 : DefaultTrackableEventHandler
    {
        public GameObject panelGameObject2;
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
            NGUITools.SetActive(panelGameObject2, true);
        }
        protected override void OnTrackingLost()
        {
            base.OnTrackingLost();
            NGUITools.SetActive(panelGameObject2, false);
        }
    }
}
