using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    public class UFOFly : MonoBehaviour
    {
        void Update()
        {
            transform.Rotate(new Vector3(0f, 5f, 0f) * 20f * Time.deltaTime);
        }
    }
}
