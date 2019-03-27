using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    public class SpilitPlanet : DefaultTrackableEventHandler
    {
        /// <summary>
        /// nút tách hành tinh
        /// </summary>
        [SerializeField] GameObject spilitButton;

        private GameObject[] planet;
        private GameObject[] _parentPlanet;

        /// <summary>
        /// check trạng thái đóng/mở của hành tinh
        /// </summary>
        static bool isOpen;

        // Start is called before the first frame update
        protected override void Start()
        {
            isOpen = false;
            NGUITools.SetActive(spilitButton, false);
        }

        // Update is called once per frame
        void Update()
        {
            planet = GameObject.FindGameObjectsWithTag("childplanet");
            _parentPlanet = GameObject.FindGameObjectsWithTag("Planet");
            AutoClick();
            SetActiveSpilitButton();
        }

        private void SetActiveSpilitButton()
        {
            if (IsOpenIfObjectIsPlanet() == true)
            {
                NGUITools.SetActive(spilitButton, true);
            }
            else
            {
                NGUITools.SetActive(spilitButton, false);
            }
        }

        //isOpen object on scene is planet, spilitbutton must be deactive
        private bool IsOpenIfObjectIsPlanet()
        {
            for (int i = 0; i < planet.Length; i++)
            {
                if (_parentPlanet[i].activeInHierarchy == true)
                {
                    return true;
                }
                //return true;
            }
            return false;
        }

        /// <summary>
        /// mở/đóng hành tinh
        /// </summary>
        void RunAnimationOpenPlanet()
        {
            if (isOpen == false)
            {
                for (int i = 0; i < planet.Length; i++)
                {
                    if (planet[i].gameObject.transform.parent.name == _parentPlanet[i].name)
                    {
                        planet[i].GetComponent<Animation>().Play("Open");
                        isOpen = true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < planet.Length; i++)
                {
                    if (planet[i].gameObject.transform.parent.name == _parentPlanet[i].name)
                    {
                        planet[i].GetComponent<Animation>().Play("Close");
                        isOpen = false;
                    }
                }
            }
        }

        public void AutoClick()
        {
            spilitButton.GetComponent<UIButton>().onClick.Clear();
            EventDelegate eventdel = new EventDelegate(this, "RunAnimationOpenPlanet");
            EventDelegate.Set(spilitButton.GetComponent<UIButton>().onClick, eventdel);
            //EventDelegate.Add(spilitButton.onClick, eventdel);
        }
    }
}
