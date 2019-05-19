using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Firecoals.Space
{
    public class GetAnimClose : MonoBehaviour
    {
        Animator anim;

        void Start()
        {
           
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {

            if (ChangeAnim.checkOpen==true)
            {

                CloseCallOut();
            }
        }
        public void CloseCallOut()
        {
            anim.Play("CloseInfo");
        }
    }
}