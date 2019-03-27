using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    public class RotateAround : MonoBehaviour
    {
        float rotSpeed = 3f;
        bool stt = false;


        void OnMouseDrag()
        {

            float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
            transform.Rotate(Autorun.defaultvt3, -rotX);
        }
    }
}
