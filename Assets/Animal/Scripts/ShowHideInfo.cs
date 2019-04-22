using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Animal
{

    public class ShowHideInfo : MonoBehaviour
    {
        /// <summary>
        /// bảng chứa thông tin về hành tinh
        /// </summary>
        public GameObject panelinfo;

        bool checkOpen;
        Animator anim;

        private void Start()
        {
            checkOpen = false;
            anim = panelinfo.GetComponent<Animator>();
        }

        /// <summary>
        /// click để show bảng thông tin 
        /// </summary>
        public void ShowObjectInfo()
        {
            checkOpen = true;
            anim.SetBool("isOpen", true);
        }

        public void HideObjectInfo()
        {
            checkOpen = false;
            anim.SetBool("isOpen", false);
        }

        public void ClickToShow()
        {
            if (checkOpen == false)
            {
                ShowObjectInfo();
            }
            else
            {
                HideObjectInfo();
            }
        }
    }
}