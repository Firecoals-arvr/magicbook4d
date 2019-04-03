using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    /// <summary>
    /// chạy animation mở/đóng hành tinh
    /// </summary>
    public class Spilitplanet : MonoBehaviour
    {
        /// <summary>
        /// biên check trạng thái đóng/mở hành tinh
        /// </summary>
        public static bool temp;

        void Start()
        {
            temp = true;
        }

        public void Spilit()
        {
            if (temp == true)
            {
                GetComponent<Animation>().Play("Open");
                temp = false;
            }
            else
            {
                GetComponent<Animation>().Play("Close");
                temp = true;
            }
        }
    }
}