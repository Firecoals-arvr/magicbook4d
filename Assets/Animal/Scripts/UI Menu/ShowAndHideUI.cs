using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Animal
{
    public class ShowAndHideUI : MonoBehaviour
    {
        public GameObject hideMenu;

        private bool isOpening;

        // Start is called before the first frame update
        void Start()
        {
            isOpening = true;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ShowMenu()
        {
            var offset = hideMenu.transform.localPosition.x;
            var x = TweenPosition.Begin(hideMenu, .5f, new Vector3(offset + 120f, 14f, 0f));
            x.method = UITweener.Method.EaseInOut;
            x.PlayForward();
            x.ResetToBeginning();

            var y = TweenPosition.Begin(this.gameObject, .5f, new Vector3(-670f, 0, 0f));
            y.method = UITweener.Method.EaseInOut;
            y.PlayForward();
            y.ResetToBeginning();

            isOpening = true;
        }

        public void HideMenu()
        {
            var offset = hideMenu.transform.localPosition.x;
            var x = TweenPosition.Begin(hideMenu, .5f, new Vector3(offset - 120f, 14f, 0f));
            x.method = UITweener.Method.EaseInOut;
            x.PlayForward();
            x.ResetToBeginning();

            var y = TweenPosition.Begin(this.gameObject, .5f, new Vector3(-780f, 0, 0f));
            y.method = UITweener.Method.EaseInOut;
            y.PlayForward();
            y.ResetToBeginning();

            isOpening = false;
        }

        public void ClickClick()
        {
            if (isOpening == true)
            {
                HideMenu();
                //this.gameObject.transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
            }
            else
            {
                ShowMenu();
                //this.gameObject.transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
            }
        }
    }
}