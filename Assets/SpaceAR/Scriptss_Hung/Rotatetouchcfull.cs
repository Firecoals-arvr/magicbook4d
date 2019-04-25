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

        void OnMouseDrag()
        {
            float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
#pragma warning disable CS0618 // Type or member is obsolete
            transform.RotateAround(Vector3.up, -rotX);
#pragma warning restore CS0618 // Type or member is obsolete
            float rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;
#pragma warning disable CS0618 // Type or member is obsolete
            transform.RotateAround(Vector3.right, rotY);
#pragma warning restore CS0618 // Type or member is obsolete
        }
    }
}
