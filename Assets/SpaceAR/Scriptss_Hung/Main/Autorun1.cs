﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    public class Autorun1 : MonoBehaviour
    {
        public GameObject center;
        public float speed;
        public static Vector3 defaultvt3;
        // Use this for initialization
        void Start()
        {
            //defaultvt3 = new Vector3(0, 1, 0);
        }

        // Update is called once per frame
        void Update()
        {
            transform.RotateAround(center.transform.position, defaultvt3, speed * 10 * Time.deltaTime);
        }
    }
}
