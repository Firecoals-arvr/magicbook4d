using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    public class EclipseAndLunar : MonoBehaviour
    {
        public Animator anim;
        bool isLunar, isEclipse;
        public GameObject moon;

        private void Start()
        {
            isEclipse = true;
            isLunar = true;
        }

        public void Eclise()
        {
            if(isEclipse == true)
            {
                isLunar = false;
                anim.SetTrigger("SetEclipse");
            }else
            {
                anim.SetTrigger("EclipseClose");
            }
            isEclipse = !isEclipse;
        }
        public void Lunar()
        {
            if (isEclipse == true)
            {
                
                anim.SetTrigger("SetEclipse");
            }
            else
            {
                anim.SetTrigger("EclipseClose");
            }
            isEclipse = !isEclipse;

        }
        //private void OnTriggerEnter(Collider other)
        //{
        //    Debug.Log("other = " + other.name);
        //    if (other.tag == "Player")
        //    {
        //        other.GetComponent<Autorun>().enabled = false;
        //        other.GetComponent<SelfRotate>().enabled = false;
        //        moon.GetComponent<BoxCollider>().enabled = true;
        //    }
        //    if (other.name == "Planet")
        //    {
        //        moon.GetComponent<Autorun>().enabled = false;
        //    }
        //}

    }
}