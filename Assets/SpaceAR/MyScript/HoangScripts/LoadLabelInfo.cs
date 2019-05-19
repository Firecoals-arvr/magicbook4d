using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{

    public class LoadLabelInfo : MonoBehaviour
    {
        // Start is called before the first frame update
        public UILabel label;
        InforObject inforObject;
        UIButton button;
        UIPlayTween target;

        GameObject pnl;

        // Start is called before the first frame update
        void Start()
        {

            Onclicked();
        }

        // Update is called once per frame
        void Update()
        {
            label.text = Localization.Get(transform.parent.parent.name);
        }

        public void SetKey()
        {
            string key = transform.parent.parent.name.Remove(transform.parent.parent.name.Length - 4, 4);
            Debug.Log(key + "Info");
            pnl.GetComponentInChildren<UILocalize>().key = key + "Info";
            pnl.GetComponentInChildren<UILabel>().text = Localization.Get(key + "Info");

        }

        private void Onclicked()
        {
            pnl = GameObject.Find("UI Root/PanelComponentInfor");
            target = GetComponent<UIPlayTween>();
            target.tweenTarget = pnl;
            EventDelegate eventDelegate = new EventDelegate(this, "SetKey");
            this.GetComponent<UIButton>().onClick.Add(eventDelegate);
        }
    }
}