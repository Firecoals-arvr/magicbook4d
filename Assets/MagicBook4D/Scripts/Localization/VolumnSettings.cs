using System.Collections;
using System.Collections.Generic;
using Firecoals.AssetBundles.Sound;
using UnityEngine;

public class VolumnSettings : MonoBehaviour
{
    public void OnVolumnChange()
    {
        var slider = GetComponent<UISlider>();
        FirecoalsSoundManager.GlobalVolume = slider.value;
        //Debug.Log("Global volume: "+ FirecoalsSoundManager.GlobalVolume);
        NGUITools.soundVolume = slider.value;
        //Debug.Log("NGUI Sound Volume "+NGUITools.soundVolume);
    }
}
