using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    /// <summary>
    /// quay object theo chiều dọc và ngang
    /// </summary>
    public class Rotatetouchcfull : MonoBehaviour
    {
        float rotSpeed = 3f;
        void Start()
        {

        }

        void OnMouseDrag()
        {
            float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
            transform.RotateAround(Vector3.up, -rotX);
            float rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;
            transform.RotateAround(Vector3.right, rotY);
        }
    }
}
