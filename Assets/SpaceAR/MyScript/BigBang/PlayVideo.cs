using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using UnityEngine.Video;

public class PlayVideo : MonoBehaviour
{
    public VideoPlayer video;
    public void PlayInQuad()
    {
        video.Play();
        this.gameObject.SetActive(false);
    }
    public void PauseInQuad()
    {
        video.Pause();
    }
}
