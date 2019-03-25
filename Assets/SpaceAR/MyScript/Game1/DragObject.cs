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
            leanSelect.OnSelect.AddListener((p) => SelectObject());
            leanSelect.OnDeselect.AddListener(() => DeSelectObject());
            leanSelect.OnSelectUp.AddListener((a) => SelectUp());

        }
        

        public void SelectObject()
        {
            
            transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
            
            transform.localPosition = new Vector3(transform.localPosition.x, 0f, transform.localPosition.z);
            

        }
        public void DeSelectObject()
        {
            transform.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
            
        }
        public void SelectUp()
        {
            
            
            if (touchObject == true)
            {
                
            }
            else
            {
                
                this.gameObject.transform.localPosition = new Vector3(0, 0, 0);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            Debug.LogError("other = " + other.name);
            if (other.tag == "planetcontainer")
            {
                
                if (other.transform.childCount == 0)
                {
                    touchObject = true;

                    transform.position = other.transform.position;
                    transform.parent = other.gameObject.transform;

                    gameObject.transform.localScale = new Vector3(2f, 2f, 2f);
                    
                }
                else
                {
                    touchObject = false;
                }
                
            }
        }





    }

}
