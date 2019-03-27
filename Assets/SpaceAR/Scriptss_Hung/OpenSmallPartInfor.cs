using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    public class OpenSmallPartInfor : MonoBehaviour
    {
        private GameObject[] inforBtn;
        [SerializeField] private GameObject panelPartInfo;

        // Start is called before the first frame update
        void Start()
        {
            inforBtn = GameObject.FindGameObjectsWithTag("infor");
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
                inforBtn[i].GetComponent<UIButton>().onClick.Clear();
                EventDelegate del = new EventDelegate(this, "PlayTweenPosition");
                EventDelegate.Set(inforBtn[i].GetComponent<UIButton>().onClick, del);
            }
        }

        void PlayTweenPosition()
        {
            for (int i = 0; i < inforBtn.Length; i++)
            {
                UIPlayTween tw = inforBtn[i].AddComponent<UIPlayTween>();
                if (panelPartInfo.GetComponent<TweenPosition>().to != new Vector3(1068f, 303f, 0f))
                {
                    tw.tweenTarget = panelPartInfo;
                    tw.includeChildren = true;
                    tw.trigger = AnimationOrTween.Trigger.OnClick;
                    tw.playDirection = AnimationOrTween.Direction.Forward;
                }
            }
        }
    }
}