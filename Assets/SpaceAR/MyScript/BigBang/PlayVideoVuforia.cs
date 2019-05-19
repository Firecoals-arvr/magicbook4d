using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Firecoals.Space
{
    /// <summary>
    /// class này để chạy video 
    /// </summary>
    public class PlayVideoVuforia : DefaultTrackableEventHandler
    {
        UnityEngine.Video.VideoPlayer video;
        protected override void Start()
        {
            base.Start();
            video = gameObject.transform.GetChild(1).GetComponent<UnityEngine.Video.VideoPlayer>();
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
        protected override void OnTrackingFound()
        {
            base.OnTrackingFound();
        }
        protected override void OnTrackingLost()
        {
            base.OnTrackingLost();
            if (video != null)
                video.Stop();
        }

        public void ClickToPlayVideo()
        {
            if (transform.GetChild(0).gameObject.activeSelf)
            {
                video.Play();
            }
        }
    }
}
