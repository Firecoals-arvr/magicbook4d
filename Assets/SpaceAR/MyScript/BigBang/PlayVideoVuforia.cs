using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayVideoVuforia : DefaultTrackableEventHandler
{
    public UnityEngine.Video.VideoPlayer video;
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
        video.Play();
    }
    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        video.Stop();
    }
}
