using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    public class ActiveSpilitButton : MonoBehaviour
    {
        /// <summary>
        /// nút mở/đóng hành tinh
        /// </summary>
        [SerializeField] GameObject spilitButton;

        private GameObject planet;
        private GameObject _parentPlanet;

        /// <summary>
        /// check trạng thái để chạy animation mở/đóng hành tinh
        /// </summary>
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
            planet = GameObject.FindGameObjectWithTag("childplanet");
            _parentPlanet = GameObject.FindGameObjectWithTag("Planet");
            SetActiveSpilitButton();
            AutoClick();
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
            if (_parentPlanet.activeSelf == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //protected override void OnTrackingFound()
        //{
        //    base.OnTrackingFound();
        //    check = false;
        //}

        //protected override void OnTrackingLost()
        //{
        //    base.OnTrackingLost();
        //    //NGUITools.SetActive(spilitButton, false);
        //    check = true;
        //}

        /// <summary>
        /// mở/đóng hành tinh
        /// </summary>
        void RunAnimationOpenPlanet()
        {
            if (check == true)
            {
                if (planet.gameObject.transform.parent.name == _parentPlanet.name)
                {
                    planet.GetComponent<Animation>().Play("Open");
                    check = false;
                }
            }
            else
            {
                if (planet.gameObject.transform.parent.name == _parentPlanet.name)
                {
                    planet.GetComponent<Animation>().Play("Close");
                    check = true;
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
