//-------------------------------------------------
//            Magic Color
// Copyright © 2016-2018 Firecoals.,JSC
//-------------------------------------------------

using UnityEngine;

namespace Firecoals.Purchasing
{
    class ActivateChanged : MonoBehaviour
    {
        public GameObject lockColor, lockSpace, lockAnimal;

        private void Awake()
        {
            //PlayerPrefs.SetString("Project_A", "ACTIVED");
            //PlayerPrefs.SetString("Project_B", "ACTIVED");
            //PlayerPrefs.SetString("Project_C", "ACTIVED");
            //PlayerPrefs.DeleteAll();
        }
        private void Start()
        {
            NGUITools.SetActive(lockAnimal, !ActiveManager.IsActiveOfflineOk("A"));
            NGUITools.SetActive(lockSpace, !ActiveManager.IsActiveOfflineOk("B"));
            NGUITools.SetActive(lockColor, !ActiveManager.IsActiveOfflineOk("C"));
        }
    }
}
