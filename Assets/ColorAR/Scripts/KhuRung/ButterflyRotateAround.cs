using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Color
{
    public class ButterflyRotateAround : MonoBehaviour
    {
        public GameObject deer;
        float speed = 10;
        // Update is called once per frame
        void Update()
        {
            
            gameObject.transform.RotateAround(deer.transform.localPosition,Vector3.up,speed*10*Time.deltaTime);
        }
    }
}