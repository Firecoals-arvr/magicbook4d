using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

namespace Firecoals.Space
{
    /// <summary>
    /// class này dùng để kéo các hành tinh trong solar system
    /// sử dụng lean scale để drag multiple planet
    /// </summary>
    public class DragObject : MonoBehaviour
    {
        LeanSelectable leanSelect;
        bool touchObject;

        private void Start()
        {
            leanSelect = gameObject.GetComponent<LeanSelectable>();
            // truyền sự kiện khi chạm, bỏ chạm hoặc chạm vào hành tinh khác 
            leanSelect.OnSelect.AddListener((p) => SelectObject());
            leanSelect.OnDeselect.AddListener(() => DeSelectObject());
            leanSelect.OnSelectUp.AddListener((a) => SelectUp());

        }


        void Update()
        {
            
            

        }


        public void SelectObject()
        {
            Debug.Log("<color=red>dragable object: " + gameObject.name + transform.position + "</color>");
            // khi chạm vào hành tinh thì cho to nó lên
            transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
            // khóa trục y khi chạm vào hành tinh
            transform.localPosition = new Vector3(0, 0, transform.localPosition.z);
        }
        public void DeSelectObject()
        {
            //khi chạm vào hành tinh khác thì cho hành tinh đó nhỏ xuống
            transform.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
        }
        /// <summary>
        /// khi bỏ tay ra khỏi hành tinh có 2 trường hợp
        /// th1 là khi đưa hành tinh vào hành tinh "?"
        /// th2 là khi đag kéo mà bỏ tay ra khi chưa kéo hành tinh vào trong hành tinh "?"
        /// </summary>
        public void SelectUp()
        {
            
            //th1
            if (touchObject == true)
            {
                touchObject = false;
            }
            //th2 
            else
            {
                this.gameObject.transform.localPosition = new Vector3(0, 0, 0);
            }
        }
        /// <summary>
        /// check va chạm 
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {

            Debug.Log("<color=green>other object: " + other.name + other.transform.position + "</color>");
            // nếu chạm vào các hành tinh "?"
            if (other.tag == "planetcontainer")
            {
                // nếu hành tinh "?" chưa có thằng nào ở trong
                if (other.transform.childCount == 0)
                {
                    touchObject = true;
                    transform.position = other.transform.position;
                    transform.parent = other.gameObject.transform;
                    gameObject.transform.localScale = new Vector3(2f, 2f, 2f);
                }
                // ngược lại
                else
                {
                    var temp = other.transform.GetChild(0);
                    var obj = gameObject.transform.parent.gameObject.transform;
                    temp.transform.parent = obj;
                    temp.transform.localPosition = Vector3.zero;
                    temp.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                    transform.position = other.transform.position;
                    transform.parent = other.gameObject.transform;
                }

            }
        }
    }



}




