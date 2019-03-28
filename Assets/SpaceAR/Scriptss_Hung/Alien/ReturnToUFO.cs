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
        [SerializeField] GameObject alienMain;
        private Animation alAnim;

        private void Start()
        {
            alAnim = alienMain.GetComponent<Animation>();
        }

        public void CallUFO()
        {
            if (alAnim.IsPlaying("Dancing"))
            {
                this.gameObject.GetComponent<Animation>().Play("AlienReturnToUFO");
            }
            else
            {
                this.gameObject.GetComponent<Animation>().Play("AlienReturnToUFO");
            }
        }
    }
}