using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireCoals.Space
{
    public class Autorotate : MonoBehaviour
    {
        public float speed;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(new Vector3(0, 0, 1) * speed * 10 * Time.deltaTime);
        }
    }
}