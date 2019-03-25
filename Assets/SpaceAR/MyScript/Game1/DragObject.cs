using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

namespace Firecoals.Space
{
    public class DragObject : MonoBehaviour
    {
        LeanSelectable leanSelect;

        /// <summary>
        /// biến check trạng thái chọn object
        /// </summary>
        bool touchObject;

        /// <summary>
        /// vị trí ban đầu của hành tinh
        /// </summary>
        Vector3 oldPostition;

        /// <summary>
        /// vị trí mới của hành tinh đó
        /// </summary>
        Vector3 newPosition;

        private void Start()
        {
            leanSelect = gameObject.GetComponent<LeanSelectable>();
            leanSelect.OnSelect.AddListener((p) => SelectObject());
            leanSelect.OnDeselect.AddListener(() => DeSelectObject());
            leanSelect.OnSelectUp.AddListener((a) => SelectUp());

            //oldPostition = this.transform.position;
        }

        /// <summary>
        /// chọn object
        /// </summary>
        public void SelectObject()
        {
            transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
            transform.localPosition = new Vector3(transform.localPosition.x, 0f, transform.localPosition.z);

            //newPosition = transform.localPosition;
        }

        /// <summary>
        /// chọn hành tinh khác
        /// </summary>
        public void DeSelectObject()
        {
            transform.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
            //this.transform.position = oldPostition;
        }

        /// <summary>
        /// bỏ chạm hành tinh ra
        /// </summary>
        public void SelectUp()
        {
            this.gameObject.transform.localPosition = new Vector3(0, 0, 0);
            //transform.position = oldPostition;
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
