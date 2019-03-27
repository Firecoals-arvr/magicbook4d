using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    public class SatelliteController : MonoBehaviour
    {
        private Animation anim;
        private bool stt = false;

        // Use this for initialization
        void Start()
        {
            anim = GetComponent<Animation>();
        }

        // Update is called once per frame
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
}