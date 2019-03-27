using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;

namespace Firecoals.Space
{
    public class ChangeAnim : MonoBehaviour
    {
        public GameObject open, close;
        public Animator anim;
        //public Button bt;
        void Start()
        {

            anim.GetComponent<Animator>();

        }



        public void OpenAnim()
        {
            NGUITools.SetActive(open, false);
            NGUITools.SetActive(close, true);

            anim.Play("Open");
        }
        public void CloseAnim()
        {
            NGUITools.SetActive(open, true);
            NGUITools.SetActive(close, false);
            anim.Play("Close");
        }

    }
}