using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    public class OpenSmallPartInfor : MonoBehaviour
    {
        private GameObject inforBtn;
        private GameObject panelPartInfo;

        // Start is called before the first frame update
        void Start()
        {
            panelPartInfo = GameObject.Find("/UIMenu Root/Panel part of object information/Text Information");
        }

        // Update is called once per frame
        void Update()
        {
            inforBtn = GameObject.FindGameObjectWithTag("infor");
        }

        void PlayTweenPosition()
        {
            UIPlayTween tw = inforBtn.AddComponent<UIPlayTween>();
            tw.tweenTarget = panelPartInfo;
        }
    }
}