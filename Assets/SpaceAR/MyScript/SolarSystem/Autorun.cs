using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
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
            Debug.Log("center + " +center.name + center.transform.position);
            Debug.Log("this + " + gameObject.name + transform.position);
        }

        // Update is called once per frame
        void Update()
        {
            //transform.Rotate(center.transform.position);
            transform.RotateAround(center.transform.position, Vector3.up, speed * 10 * Time.deltaTime);
            //transform.RotateAround(Vector3.zero, Vector3.up, speed * 10 * Time.deltaTime);
        }
    }
}
