using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    public class RotateTouch : MonoBehaviour
    {
        public float speed;

        private void OnMouseDrag()
        {
            float rotX = Input.GetAxis("Mouse X") * Mathf.Deg2Rad * speed;
            transform.RotateAround(new Vector3(0f, 5f, 0f), -rotX);
        }
    }
}
