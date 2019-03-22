using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireCoals.Space
{
    public class Autorun : MonoBehaviour
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
            //transform.Rotate(center.transform.position);
            transform.RotateAround(center.transform.position, Vector3.up, speed * 10 * Time.deltaTime);

        }
    }
}
