using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    /// <summary>
    /// class này để xoay prefabs
    /// </summary>
    public class RotateAround : MonoBehaviour
    {
        float rotSpeed = 3f;
        bool stt = false;

        
        void OnMouseDrag()
        {
            //lấy hướng của chuột hướng x 
            float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
            // rotate
            transform.Rotate(Autorun.defaultvt3, -rotX);
        }
    }
}
