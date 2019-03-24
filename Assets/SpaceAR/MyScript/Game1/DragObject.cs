using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

namespace Firecoals.Space
{
    public class DragObject : MonoBehaviour
    {
        LeanSelectable leanSelect;
        bool touchObject;
        private void Start()
        {
            leanSelect = gameObject.GetComponent<LeanSelectable>();
            leanSelect.OnSelect.AddListener((p) => ScaleObject());
            leanSelect.OnDeselect.AddListener(() => DeScaleObject());
            leanSelect.OnSelectUp.AddListener((a) => DeSelectUp());
        }

        void ScaleObject()
        {
            transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
            

        }
        void DeScaleObject()
        {
            transform.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
            //if (touchObject == true)
            //{

            //}
            //else
            //{
            //    Debug.Log("this is ");
            //    this.gameObject.transform.localPosition = new Vector3(0, 0, 0);
            //}
        }
        void DeSelectUp()
        {
            transform.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
            if (touchObject == true)
            {

            }
            else
            {
                Debug.Log("asjhfkaf");
                this.gameObject.transform.localPosition = new Vector3(0, 0, 0);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.name == "Saturn")
            {
                touchObject = true;
            }
        }





    }

}
