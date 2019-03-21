using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    public class ReturnToUFO : MonoBehaviour
    {
        public void CallUFO()
        {
            this.gameObject.GetComponent<Animation>().Play("AlienReturnToUFO");
        }
    }
}