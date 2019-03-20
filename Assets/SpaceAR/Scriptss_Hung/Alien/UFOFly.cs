using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireCoals.Space
{
    public class UFOFly : MonoBehaviour
    {
        //float rotY;
        //float rotSpeed;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(new Vector3(0f, 5f, 0f) * 20f * Time.deltaTime);
        }
    }
}
