using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Firecoals.Space
{
    public class PlayVideoVuforia : DefaultTrackableEventHandler
    {
        UnityEngine.Video.VideoPlayer video;
        protected override void Start()
        {
            base.Start();
            if (transform.childCount > 0)
            {
                video =  gameObject.transform.GetChild(1).GetComponent<UnityEngine.Video.VideoPlayer>();
            }
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
        protected override void OnTrackingFound()
        {
            base.OnTrackingFound();
            video.Play();
        }
        protected override void OnTrackingLost()
        {
            base.OnTrackingLost();
            video.Stop();
        }
    }
}
