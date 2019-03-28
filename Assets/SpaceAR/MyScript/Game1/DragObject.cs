﻿using System.Collections;
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


        void Update()
        {
            
            //if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
            //{
            //    // create ray from the camera and passing through the touch position:
            //    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            //    // create a logical plane at this object's position
            //    // and perpendicular to world Y:
            //    Plane plane = new Plane(Vector3.up, transform.position);
            //    float distance = 0; // this will return the distance from the camera
            //    if (plane.Raycast(ray, out distance))
            //    { // if plane hit...
            //        Vector3 pos = ray.GetPoint(distance); // get the point
            //        transform.position = pos; // pos has the position in the plane you've touched
            //    }
            //}

        }


        public void SelectObject()
        {
            Debug.Log("<color=red>dragable object: " + gameObject.name + transform.position + "</color>");
            transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
            transform.localPosition = new Vector3(0, 0, transform.localPosition.z);
        }
        public void DeSelectObject()
        {
            transform.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
        }
        public void SelectUp()
        {
            if (touchObject == true)
            {
                touchObject = false;
            }
            else
            {
                this.gameObject.transform.localPosition = new Vector3(0, 0, 0);
            }
        }
        private void OnTriggerEnter(Collider other)
        {

            Debug.Log("<color=green>other object: " + other.name + other.transform.position + "</color>");
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



