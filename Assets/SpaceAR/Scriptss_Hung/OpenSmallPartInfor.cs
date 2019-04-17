using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    public class OpenSmallPartInfor : MonoBehaviour
    {
        /// <summary>
        /// mở panel thông tin thành phần của object
        /// </summary>
        private GameObject[] inforBtn;
        [SerializeField] private GameObject panelPartInfo;
        bool checkOpen;
        Animator anim;

        // Start is called before the first frame update
        void Start()
        {
            //inforBtn = GameObject.FindGameObjectsWithTag("infor");
            anim = panelPartInfo.GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            inforBtn = GameObject.FindGameObjectsWithTag("infor");
            AutoTriggerInforButton();
        }

        void AutoTriggerInforButton()
        {
            for (int i = 0; i < inforBtn.Length; i++)
            {
                if (inforBtn[i].activeInHierarchy)
                {
                    inforBtn[i].GetComponent<UIButton>().onClick.Clear();
                    EventDelegate del = new EventDelegate(this, "ShowSmallInfo");
                    EventDelegate.Set(inforBtn[i].GetComponent<UIButton>().onClick, del);
                }
            }
        }

        public void ShowSmallInfo()
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

        private void ShowObjectInfo()
        {
            checkOpen = true;
            anim.SetBool("isOpen", true);
        }

        private void HideObjectInfo()
        {
            checkOpen = false;
            anim.SetBool("isOpen", false);
        }
    }
}