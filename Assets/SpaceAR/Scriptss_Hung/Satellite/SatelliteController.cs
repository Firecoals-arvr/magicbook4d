using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteController : MonoBehaviour
{
    [SerializeField] Animation anim;
    private bool stt = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Debug.Log(hit.transform.name);
                if (hit.transform.tag == "vetinh")
                {
                    anim.Play("open");
                }
            }
        }
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
