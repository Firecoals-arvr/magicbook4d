﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Firecoals.Space
{
    public class SatelliteController : MonoBehaviour
    {
        [SerializeField] Animation anim = default;
        private bool stt = false;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.tag == "vetinh")
                    {
                        Open();
                    }
                }
            }
        }
        void Open()
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
                ChangeAnim.checkOpen = false;
            }
            else
            {
                ChangeAnim.checkOpen = true;
                anim.Play("closespilit");
                stt = false;
            }

        }
    }
}