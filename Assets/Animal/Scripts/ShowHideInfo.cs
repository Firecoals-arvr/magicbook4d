using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firecoals.AssetBundles.Sound;

namespace Firecoals.Animal
{

    public class ShowHideInfo : MonoBehaviour
    {
        /// <summary>
        /// bảng chứa thông tin về hành tinh
        /// </summary>
        public GameObject panelinfo;
        LoadSoundbundles _loadSoundbundles;
        bool checkOpen;
        Animator anim;

        private void Start()
        {
            checkOpen = false;
            anim = panelinfo.GetComponent<Animator>();
            _loadSoundbundles = FindObjectOfType<LoadSoundbundles>();
        }

        /// <summary>
        /// click để show bảng thông tin 
        /// </summary>
        public void ShowObjectInfo()
        {
            checkOpen = true;
            NGUITools.SetActive(panelinfo, true);
            anim.SetTrigger("Open");
            _loadSoundbundles.ReplayInfo();
        }

        public IEnumerator HideObjectInfo()
        {
            checkOpen = false;
            anim.SetTrigger("Close");
            yield return new WaitForSeconds(1f);
            NGUITools.SetActive(panelinfo, false);
            FirecoalsSoundManager.StopAll();
        }

        public void ClickToShow()
        {
            if (checkOpen == false)
            {
                ShowObjectInfo();
            }
            else
            {
                StartCoroutine( HideObjectInfo());
            }
        }
    }
}