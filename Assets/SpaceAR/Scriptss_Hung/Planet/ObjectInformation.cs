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

        bool isOpening;

        UIPlayTween tw;

        private void Start()
        {
            isOpening = false;
            tw = this.GetComponent<UIPlayTween>();
        }

        /// <summary>
        /// click để show bảng thông tin 
        /// </summary>
        public void ShowObjectInfo()
        {
            tw.includeChildren = true;
            //tw.playDirection = AnimationOrTween.Direction.Forward;
            isOpening = true;
        }

        public void HideObjectInfo()
        {
            tw.includeChildren = true;
            //tw.playDirection = AnimationOrTween.Direction.Reverse;
            isOpening = false;
        }

        public void ClickClick()
        {
            if (tw.playDirection == AnimationOrTween.Direction.Forward)
            {
                this.GetComponent<UIButton>().onClick.Clear();
                tw.playDirection = AnimationOrTween.Direction.Reverse;
                EventDelegate eventdel = new EventDelegate(this, "ShowObjectInfo");
                EventDelegate.Set(this.GetComponent<UIButton>().onClick, eventdel);

            }
            if (tw.playDirection == AnimationOrTween.Direction.Reverse)
            {
                this.GetComponent<UIButton>().onClick.Clear();
                tw.playDirection = AnimationOrTween.Direction.Forward;
                EventDelegate eventdel = new EventDelegate(this, "HideObjectInfo");
                EventDelegate.Set(this.GetComponent<UIButton>().onClick, eventdel);

            }
        }
    }
}
