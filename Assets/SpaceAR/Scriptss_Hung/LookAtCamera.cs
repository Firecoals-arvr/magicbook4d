﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireCoals.Space
{
    public class LookAtCamera : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            this.transform.LookAt(Camera.main.transform.position);
        }
    }
}