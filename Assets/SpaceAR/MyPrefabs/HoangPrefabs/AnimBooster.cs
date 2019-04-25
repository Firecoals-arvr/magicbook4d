using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Firecoals.Space
{

    public class AnimBooster : MonoBehaviour
    {
        Animator anim;
        bool checkStatusIdle;
        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
        }
        public void Launch()
        {
            //if (!checkStatusIdle)
            //{
            //    anim.Play("New State");
            //    GetComponent<AudioSource>().Stop();
            //}
            anim.Play("Lauch");
            GetComponent<AudioSource>().Stop();
        }
        public void Open4D()
        {
            //if (!checkStatusIdle)
            //{
            //    anim.Play("New State");
            //    GetComponent<AudioSource>().Stop();
            //}
            anim.Play("4D");
            GetComponent<AudioSource>().Stop();
        }
    }
}
