using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

namespace Firecoals.Space
{
    public class ShowPanelElipse : DefaultTrackableEventHandler
    {
        
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
            NGUITools.SetActive(gameObject.transform.GetChild(0).transform.GetChild(1).gameObject, true);

            
            
        }
        protected override void OnTrackingLost()
        {
            base.OnTrackingLost();
        }
    }
}
