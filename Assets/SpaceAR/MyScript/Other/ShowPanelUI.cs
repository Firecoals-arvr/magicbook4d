using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    public class ShowPanelUI : DefaultTrackableEventHandler
    {
        Animator anim;
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
            int i = 0;
            base.OnTrackingFound();
            Debug.Log("how many time = "+i);
            anim = this.gameObject.GetComponentInChildren<Animator>();
            anim.SetTrigger("Intro");
            //NGUITools.SetActive(gameObject.transform.GetChild(0).transform.GetChild(1).gameObject, true);
            i++;

        }
        protected override void OnTrackingLost()
        {
            base.OnTrackingLost();
        }
    }
}
