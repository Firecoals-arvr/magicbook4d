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
        private void Start()
        {
            foreach (var projectId in new[] { "A", "B", "C" })
            {
                if (ActiveManager.IsActiveOfflineOk(projectId))
                {
                    //TODO if have a copyright show key unlock, opposite show lock
                }
            }
        }
    }
}
