using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireCoals.Space
{
    public class Isscontroller : MonoBehaviour
    {
        // Use this for initialization

        void Start()
        {
            anima = GetComponent<Animation>();
        }
        Animation anima;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.transform.tag == "Emu")
                    {
                        anima.Play("openemu");
                    }
                    else if (hit.transform.tag == "Iss")
                    {
                        anima.Play("openiss");
                    }
                }
            }

        }
        public void Returnmain()
        {
            anima.Play("intro");
        }
    }
}