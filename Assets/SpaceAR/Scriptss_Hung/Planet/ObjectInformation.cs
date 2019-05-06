using Firecoals.AssetBundles.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{

    public class ObjectInformation : MonoBehaviour
    {
        /// <summary>
        /// bảng chứa thông tin về hành tinh
        /// </summary>
        public GameObject panelinfo;

        /// <summary>
        /// object chứa các imagetargets
        /// </summary>
        public GameObject target;

        /// <summary>
        /// check bảng thông tin mở/đóng
        /// </summary>
        [HideInInspector] public bool checkOpen;

        /// <summary>
        /// animator của bảng thông tin
        /// </summary>
        public Animator anim;

        private LoadSoundbundles _loadSoundbundles;
        List<Transform> go = new List<Transform>();

        private void Start()
        {
            checkOpen = false;
            _loadSoundbundles = FindObjectOfType<LoadSoundbundles>();
            //anim = panelinfo.GetComponent<Animator>();
        }

        /// <summary>
        /// show bảng thông tin 
        /// </summary>
        public void ShowObjectInfo()
        {
            checkOpen = true;
            anim.SetBool("isOpen", true);
            _loadSoundbundles.ReplayInfoSound();
        }

        /// <summary>
        /// hide bảng thông tin
        /// </summary>
        public void HideObjectInfo()
        {
            checkOpen = false;
            anim.SetBool("isOpen", false);
            FirecoalsSoundManager.StopAll();
        }

        /// <summary>
        /// chạy show/hide bảng thông tin
        /// </summary>
        public void ClickToShow()
        {
            if (CheckTargetHasChildOrNot() == true)
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

        /// <summary>
        /// check có hành tinh trên scene thì mới mở bảng thông tin,
        /// không có thì không mở
        /// </summary>
        /// <returns></returns>
        private bool CheckTargetHasChildOrNot()
        {
            foreach (Transform a in target.transform)
            {
                go.Add(a);
            }
            foreach (Transform a in go)
            {
                if (a.childCount != 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
