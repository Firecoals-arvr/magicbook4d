using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireCoals.Space
{
    public class Phihanhgiacapture : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }
        public GameObject phg;
        public void enablephg()
        {
            phg.active = true;
        }
        public void disblephg()
        {
            phg.active = false;
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}