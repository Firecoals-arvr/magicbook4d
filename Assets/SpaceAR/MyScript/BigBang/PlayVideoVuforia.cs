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

        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
        protected override void OnTrackingFound()
        {
            base.OnTrackingFound();
            if (transform.childCount > 0)
            {
                video = gameObject.transform.GetChild(1).GetComponent<UnityEngine.Video.VideoPlayer>();
                video.Play();
            }
            else
            {

            }

        }
        protected override void OnTrackingLost()
        {
            base.OnTrackingLost();
            if (video != null)
                video.Stop();
        }
    }
}
