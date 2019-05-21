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
        /// <summary>
        /// nút play video
        /// </summary>
        private GameObject playbutton;

        /// <summary>
        /// component video player
        /// </summary>
        UnityEngine.Video.VideoPlayer video;

        protected override void Start()
        {
            base.Start();
        }

        protected override void OnTrackingFound()
        {
            base.OnTrackingFound();
            playbutton = transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject;
            NGUITools.SetActive(playbutton, true);
            video = transform.GetChild(0).transform.GetChild(1).GetComponent<UnityEngine.Video.VideoPlayer>();
            EventForPlayButton();
        }

        protected override void OnTrackingLost()
        {
            base.OnTrackingLost();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        private void EventForPlayButton()
        {
            EventDelegate _delegate = new EventDelegate(this, "ClickToPlayVideo");
            EventDelegate.Set(playbutton.GetComponent<UIButton>().onClick, _delegate);
        }

        private void ClickToPlayVideo()
        {
            NGUITools.SetActive(playbutton, false);
            video.Play();
            Debug.Log("<color=red> Play Button Clicked! </color>");
            InvokeRepeating("CheckVideoOVer", 1f, 1f);
        }

        /// <summary>
        /// check video chạy xong chưa
        /// </summary>
        private void CheckVideoOVer()
        {
            if (video.frame == (long)video.frameCount)
            {
                NGUITools.SetActive(playbutton, true);
            }
        }
    }
}
