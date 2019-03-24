using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

namespace FireCoals.Space
{
    public class DragObject : MonoBehaviour
    {
        GameObject temp;
        //LeanSelectable leanSelectable;
        private void Start()
        {
            //leanSelectable =  this.gameObject.GetComponent<LeanSelectable>();
            
        }
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log(Autorun.defaultvt3);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit) && (hit.collider.tag == "planetcontainer"))
                {
                    if (temp == null)
                    {
                        hit.transform.localScale += new Vector3(0.04f, 0.04f, 0.04f);
                        temp = hit.transform.gameObject;
                    }
                    else
                    {
                        if (temp.transform.name != hit.transform.name)
                        {
                            temp.transform.localScale -= new Vector3(0.04f, 0.04f, 0.04f);
                            hit.transform.localScale += new Vector3(0.04f, 0.04f, 0.04f);
                            temp = hit.transform.gameObject;
                        }

                    }
                    //Debug.Log("is this true" + leanSelectable.IsSelected);
                    //if (leanSelectable.IsSelected == true)
                    //{
                    //    transform.localScale += new Vector3(0.04f, 0.04f, 0.04f);
                    //}
                    //else
                    //    transform.localScale -= new Vector3(0.04f, 0.04f, 0.04f);
                }
                else
                {
                    if (temp != null)
                    {
                        temp.transform.localScale -= new Vector3(0.04f, 0.04f, 0.04f);
                        temp = null;
                    }

                }
            }
        }

        



    }

}
