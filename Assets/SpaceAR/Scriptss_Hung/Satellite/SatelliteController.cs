using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteController : MonoBehaviour
{
    [SerializeField] Animation anim;
    private bool stt = false;


    public void ClickToOpen()
    {
        anim.Play("open");
    }

    public void Armode()
    {
        anim.Play("close");
        stt = false;
    }

    public void Spilit()
    {
        if (stt == false)
        {
            anim.Play("spilit");
            stt = true;
        }
        else
        {
            anim.Play("closespilit");
            stt = false;
        }

    }
}
