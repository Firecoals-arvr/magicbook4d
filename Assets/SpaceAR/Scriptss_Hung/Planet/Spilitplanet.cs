using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    public class Spilitplanet : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            temp = true;
        }

        // Update is called once per frame
        void Update()
        {

        }
        public static bool temp;
        public void Spilit()
        {
            if (temp == true)
            {
                GetComponent<Animation>().Play("Open");
                temp = false;
            }
            else
            {
                GetComponent<Animation>().Play("Close");
                temp = true;
            }
        }
    }
}
