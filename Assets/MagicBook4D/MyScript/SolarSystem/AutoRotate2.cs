using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    public class AutoRotate2 : MonoBehaviour
    {
        public float speed;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(new Vector3(0, 1, 0) * speed * 10 * Time.deltaTime);
        }
    }
}
