using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    /// <summary>
    /// bảng tên của thành phần trong hành tinh hướng về phía camera
    /// </summary>
    public class LookAtCamera : MonoBehaviour
    {

        // Update is called once per frame
        void Update()
        {
            this.transform.LookAt(Camera.main.transform.position);
        }
    }
}
