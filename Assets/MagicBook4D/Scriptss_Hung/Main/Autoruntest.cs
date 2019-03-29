using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    public class Autoruntest : MonoBehaviour
    {

        // Use this for initialization
        Autorun runmoon;
        void Start()
        {
            runmoon = new Autorun();
            runmoon = transform.GetComponent<Autorun>();
        }
        void Update()
        {
            if (transform.position.x > 0 && transform.position.x < 0.1f)
            {
                if (transform.position.z < -0.7f)
                    runmoon.speed = 0;
            }
        }
    }
}
