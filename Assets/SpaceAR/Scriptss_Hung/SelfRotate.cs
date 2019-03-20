using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireCoals.Space
{
    /// <summary>
    /// class này dùng để xoay vật thể
    /// </summary>
    public class SelfRotate : MonoBehaviour
    {
        public float speed;

        // Start is called before the first frame update
        void Start()
        {
            Add(1, 2);
        }


        // Update is called once per frame
        void Update()
        {
            transform.Rotate(new Vector3(0f, 5f, 0f) * speed * Time.deltaTime);
        }
        /// <summary>
        /// Add 2 interger
        /// </summary>
        /// <param name="a">a is the first number</param>
        /// <param name="b">b is the second number</param>
        /// <returns></returns>
        private int Add(int a, int b)
        {
            return a + b;
        }
    }
}