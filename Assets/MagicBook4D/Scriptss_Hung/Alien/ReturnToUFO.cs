using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    /// <summary>
    /// chạy animation đĩa bay đón alien
    /// </summary>
    public class ReturnToUFO : MonoBehaviour
    {
        public void CallUFO()
        {
            this.gameObject.GetComponent<Animation>().Play("AlienReturnToUFO");
        }
    }
}