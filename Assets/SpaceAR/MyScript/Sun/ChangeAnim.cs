using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;

namespace Firecoals.Space
{
    public class ChangeAnim : MonoBehaviour
    {
        public GameObject open;
        public Animator anim;
        bool changAnim;
        void Start()
        {
            //anim.GetComponent<Animator>();
            changAnim = true;
        }

        public void OpenAnim()
        {
            if (changAnim == true)
            {
                anim.Play("Open");
            }
            else
                anim.Play("Close");
            changAnim = !changAnim;
        }


    }
}