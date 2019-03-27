using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    public class SpilitPlanet : MonoBehaviour
    {
        /// <summary>
        /// nút tách hành tinh
        /// </summary>
        [SerializeField] GameObject spilitButton;

        private GameObject[] planet;
        private GameObject[] _parentPlanet;
        bool check = false;

        // Start is called before the first frame update
        private void Start()
        {
            check = true;
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
            if (CheckIfObjectIsPlanet() == true)
            {
                NGUITools.SetActive(spilitButton, true);
            }
            else
            {
                NGUITools.SetActive(spilitButton, false);
            }
        }

        //check object on scene is planet, spilitbutton must be deactive
        private bool CheckIfObjectIsPlanet()
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

        //protected override void OnTrackingFound()
        //{
        //    base.OnTrackingFound();
        //    check = false;
        //}

        //protected override void OnTrackingLost()
        //{
        //    base.OnTrackingLost();
        //    check = true;
        //}

        /// <summary>
        /// mở/đóng hành tinh
        /// </summary>
        void RunAnimationOpenPlanet()
        {
            if (check == true)
            {
                for (int i = 0; i < planet.Length; i++)
                {
                    if (planet[i].gameObject.transform.parent.name == _parentPlanet[i].name)
                    {
                        planet[i].GetComponent<Animation>().Play("Open");
                        check = false;
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
                        check = true;
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
