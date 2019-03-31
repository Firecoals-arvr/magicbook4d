using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    public class OpenSmallPartInfor : MonoBehaviour
    {
        /// <summary>
        /// mở panel thông tin thành phần của object
        /// </summary>
        private GameObject[] inforBtn;
        [SerializeField] private GameObject panelPartInfo;

        // Start is called before the first frame update
        void Start()
        {
            //inforBtn = GameObject.FindGameObjectsWithTag("infor");
        }

        // Update is called once per frame
        void Update()
        {
            inforBtn = GameObject.FindGameObjectsWithTag("infor");
            AutoTriggerInforButton();
        }

        void AutoTriggerInforButton()
        {
            for (int i = 0; i < inforBtn.Length; i++)
            {
                if (inforBtn[i].activeInHierarchy)
                {
                    inforBtn[i].GetComponent<UIButton>().onClick.Clear();
                    EventDelegate del = new EventDelegate(this, "PlayTweenPosition");
                    EventDelegate.Set(inforBtn[i].GetComponent<UIButton>().onClick, del);
                }
            }
        }

        public void PlayTweenPosition()
        {
            for (int i = 0; i < inforBtn.Length; i++)
            {
                UIPlayTween tw = inforBtn[i].AddComponent<UIPlayTween>();
                tw.tweenTarget = panelPartInfo;
                tw.includeChildren = true;
                tw.trigger = AnimationOrTween.Trigger.OnClick;
                if (panelPartInfo.transform.position == new Vector3(1069f, 194f, 0f))
                {
                    tw.playDirection = AnimationOrTween.Direction.Forward;
                }
                if (panelPartInfo.transform.position == new Vector3(2216f, 194f, 0f))
                {
                    tw.playDirection = AnimationOrTween.Direction.Reverse;
                }
            }
        }
    }
}