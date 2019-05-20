using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    /// <summary>
    /// Dùng để mở bảng tên của các hành tinh trong hệ mặt trời
    /// </summary>
    public class Clickzoomplanet : MonoBehaviour
    {
        GameObject temp;

        void Update()
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
                        hit.transform.GetChild(0).gameObject.SetActive(true);
                        temp = hit.transform.gameObject;
                        
                    }
                    else
                    {
                        if (temp.transform.name != hit.transform.name)
                        {
                            temp.transform.localScale -= new Vector3(0.04f, 0.04f, 0.04f);
                            temp.transform.GetChild(0).gameObject.SetActive(false);
                            hit.transform.localScale += new Vector3(0.04f, 0.04f, 0.04f);
                            hit.transform.GetChild(0).gameObject.SetActive(true);
                            temp = hit.transform.gameObject;
                        }
                    }
                    
                }
                else
                {
                   
                    if (temp != null)
                    {
                        temp.transform.localScale -= new Vector3(0.04f, 0.04f, 0.04f);
                        temp.transform.GetChild(0).gameObject.SetActive(false);
                        temp = null;
                    }

                }
            }
        }
    }
}