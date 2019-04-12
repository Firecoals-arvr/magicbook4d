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

        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
        protected override void OnTrackingFound()
        {
            base.OnTrackingFound();
            // nếu chưa lật đến trang bigbang thì ko cho chạy video
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
