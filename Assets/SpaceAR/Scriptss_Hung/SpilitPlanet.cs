using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireCoals.Space
{
    public class SpilitPlanet : DefaultTrackableEventHandler
    {
        [SerializeField] GameObject spilitButton;
        private GameObject[] planet;
        private GameObject[] _parentPlanet;
        bool check = false;

        // Start is called before the first frame update
        protected override void Start()
        {
            planet = GameObject.FindGameObjectsWithTag("childplanet");
            _parentPlanet = GameObject.FindGameObjectsWithTag("Planet");
            check = true;
            NGUITools.SetActive(spilitButton, false);
        }

        // Update is called once per frame
        void Update()
        {
            AutoClick();
            if (CheckIfObjectIsPlanet() == false)
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
            //foreach (var objectInactive in planet)
            //{
            //    if (objectInactive.activeInHierarchy == true)
            //    {
            //        return false;
            //    }
            //    else
            //        return true;
            //}

            for (int i = 0; i < planet.Length; i++)
            {
                if (_parentPlanet[i].activeInHierarchy == true)
                {
                    return false;
                }
                //return true;
            }
            return true;
        }

        protected override void OnTrackingFound()
        {
            //base.OnTrackingFound();
            //if (CheckIfObjectIsPlanet() == false)
            //{
            //    NGUITools.SetActive(spilitButton, true);
            //}
            //else
            //{
            //    NGUITools.SetActive(spilitButton, false);
            //}
            check = false;
        }

        protected override void OnTrackingLost()
        {
            base.OnTrackingLost();
            check = true;
        }

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
