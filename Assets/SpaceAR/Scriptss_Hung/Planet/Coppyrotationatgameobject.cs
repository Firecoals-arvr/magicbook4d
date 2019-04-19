using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    public class Coppyrotationatgameobject : MonoBehaviour
    {
        public GameObject target;
        // Update is called once per frame
        void Update()
        {
            transform.localEulerAngles = target.transform.localEulerAngles;
        }
    }
}