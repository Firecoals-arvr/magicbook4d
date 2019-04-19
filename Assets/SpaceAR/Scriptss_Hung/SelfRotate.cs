using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    /// <summary>
    /// class này dùng để xoay vật thể
    /// </summary>
    public class SelfRotate : MonoBehaviour
    {

        public float speed;

        void Update()
        {
            transform.Rotate(new Vector3(0f, 5f, 0f) * speed * Time.deltaTime);
        }
    }
}