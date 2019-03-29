using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireCoals.Space
{
    public class LookAtCamera : MonoBehaviour
    {
        Quaternion qua;
        // Start is called before the first frame update
        void Start()
        {
            qua = transform.localRotation;
        }

        // Update is called once per frame
        void Update()
        {
            transform.localRotation = Camera.main.transform.localRotation * qua;
            //transform.LookAt(Camera.main.transform);
        }
    }
}