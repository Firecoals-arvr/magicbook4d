using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;

namespace Firecoals.Space
{
    public class ChangeAnim : MonoBehaviour
    {
        public Animator anim;
        public static bool checkOpen=false; 
        public static bool changAnim;
        void Start()
        {
            anim.GetComponent<Animator>();
            changAnim = true;
            checkOpen = false;
        }

        public void OpenAnim()
        {
            if (changAnim == true)
            {
                anim.Play("Open");
                changAnim = !changAnim;
                checkOpen = false;
                Debug.Log("CheckOpen: " + checkOpen);
            }
            else
            {
                
                checkOpen = true;
                Debug.Log("CheckOpen : " + checkOpen);
                anim.Play("Close");
                changAnim = !changAnim;
            }
        }


    }
}