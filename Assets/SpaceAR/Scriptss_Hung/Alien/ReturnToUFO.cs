using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireCoals.Space
{
    public class ReturnToUFO : MonoBehaviour
    {
        public void CallUFO()
        {
            this.gameObject.GetComponent<Animation>().Play("AlienReturnToUFO");
        }
    }
}