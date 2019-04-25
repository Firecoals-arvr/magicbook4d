using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;

namespace Firecoals.Space
{
    public class ChangeAnim : MonoBehaviour
    {
        public static ChangeAnim Instance;
        public GameObject open;
        public Animator anim;
        public bool checkOpen = true;
        public static bool changAnim;
        void Awake()
        {
            if (Instance == null) Instance = this;
            else
            {
                Destroy(this);
            }
        }
        void Start()
        {
            open = GameObject.Find("ChangeAnimOpen");
            anim.GetComponent<Animator>();
            changAnim = true;
        }

        public void OpenAnim()
        {
            if (changAnim == true)
            {
                anim.Play("Open");
                changAnim = !changAnim;
                checkOpen = true;
            }
            else
            {
                checkOpen = false;
                anim.Play("Close");
                changAnim = !changAnim;
            }
        }


    }
}