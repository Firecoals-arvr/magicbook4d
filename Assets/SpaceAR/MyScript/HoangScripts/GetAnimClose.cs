using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Firecoals.Space
{
    public class GetAnimClose : MonoBehaviour
    {
        public static GetAnimClose instance;
        Animator anim;

        void Start()
        {
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!ChangeAnim.Instance.checkOpen)
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