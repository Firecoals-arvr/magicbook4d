using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    public class LookAtCamera_C : MonoBehaviour
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

            Vector3 vt = new Vector3((2 * transform.position.x - Camera.main.transform.position.x), 0.0f, (2 * transform.position.z - Camera.main.transform.position.z));
            transform.LookAt(vt);
        }
    }
}