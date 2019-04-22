using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    public class Clickzoomplanet : MonoBehaviour
    {

        // Use this for initialization
        public GameObject Earthinfor;
        public GameObject Jupiterinfor;
        public GameObject Neptuneinfor;
        public GameObject Saturninfor;
        public GameObject Marsinfor;
        public GameObject Venusinfor;
        public GameObject Uranusinfor;
        public GameObject Mecuryinfor;
        //public GameObject //Suninfor;

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
                    //stack.Push(hit);
                    // hit.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

                    switch (hit.transform.name)
                    {
                        case "Earth":
                            Earthinfor.SetActive(true);
                            Jupiterinfor.SetActive(false);
                            Neptuneinfor.SetActive(false);
                            Saturninfor.SetActive(false);
                            Marsinfor.SetActive(false);
                            Venusinfor.SetActive(false);
                            Uranusinfor.SetActive(false);
                            Mecuryinfor.SetActive(false);
                            //Suninfor.SetActive(false);
                            break;
                        case "Jupiter":
                            Jupiterinfor.SetActive(true);
                            Earthinfor.SetActive(false);
                            Neptuneinfor.SetActive(false);
                            Saturninfor.SetActive(false);
                            Marsinfor.SetActive(false);
                            Venusinfor.SetActive(false);
                            Uranusinfor.SetActive(false);
                            Mecuryinfor.SetActive(false);
                            //Suninfor.SetActive(false);
                            break;
                        case "Neptune":
                            Neptuneinfor.SetActive(true);
                            Earthinfor.SetActive(false);
                            Jupiterinfor.SetActive(false);
                            Saturninfor.SetActive(false);
                            Marsinfor.SetActive(false);
                            Venusinfor.SetActive(false);
                            Uranusinfor.SetActive(false);
                            Mecuryinfor.SetActive(false);
                            //Suninfor.SetActive(false);
                            break;
                        case "Saturn":
                            Saturninfor.SetActive(true);
                            Earthinfor.SetActive(false);
                            Jupiterinfor.SetActive(false);
                            Neptuneinfor.SetActive(false);
                            Marsinfor.SetActive(false);
                            Venusinfor.SetActive(false);
                            Uranusinfor.SetActive(false);
                            Mecuryinfor.SetActive(false);
                            //Suninfor.SetActive(false);
                            break;
                        case "Mars":
                            Marsinfor.SetActive(true);
                            Earthinfor.SetActive(false);
                            Jupiterinfor.SetActive(false);
                            Neptuneinfor.SetActive(false);
                            Saturninfor.SetActive(false);
                            Venusinfor.SetActive(false);
                            Uranusinfor.SetActive(false);
                            Mecuryinfor.SetActive(false);
                            //Suninfor.SetActive(false);
                            break;
                        case "Venus":
                            Venusinfor.SetActive(true);
                            Earthinfor.SetActive(false);
                            Jupiterinfor.SetActive(false);
                            Neptuneinfor.SetActive(false);
                            Saturninfor.SetActive(false);
                            Marsinfor.SetActive(false);
                            Uranusinfor.SetActive(false);
                            Mecuryinfor.SetActive(false);
                            //Suninfor.SetActive(false);
                            break;
                        case "Mercury":
                            Mecuryinfor.SetActive(true);
                            Earthinfor.SetActive(false);
                            Jupiterinfor.SetActive(false);
                            Neptuneinfor.SetActive(false);
                            Saturninfor.SetActive(false);
                            Marsinfor.SetActive(false);
                            Venusinfor.SetActive(false);
                            Uranusinfor.SetActive(false);
                            //Suninfor.SetActive(false);
                            break;
                        case "Uranus":
                            Uranusinfor.SetActive(true);
                            Earthinfor.SetActive(false);
                            Jupiterinfor.SetActive(false);
                            Neptuneinfor.SetActive(false);
                            Saturninfor.SetActive(false);
                            Marsinfor.SetActive(false);
                            Venusinfor.SetActive(false);
                            Mecuryinfor.SetActive(false);
                            //Suninfor.SetActive(false);
                            break;
                        case "Sun":
                            //Suninfor.SetActive(true);
                            Uranusinfor.SetActive(false);
                            Earthinfor.SetActive(false);
                            Jupiterinfor.SetActive(false);
                            Neptuneinfor.SetActive(false);
                            Saturninfor.SetActive(false);
                            Marsinfor.SetActive(false);
                            Venusinfor.SetActive(false);
                            Mecuryinfor.SetActive(false);
                            break;


                        default:
                            {
                                break;
                            }


                    }
                }
                else
                {
                    Earthinfor.SetActive(false);
                    Jupiterinfor.SetActive(false);
                    Neptuneinfor.SetActive(false);
                    Saturninfor.SetActive(false);
                    Marsinfor.SetActive(false);
                    Venusinfor.SetActive(false);
                    Uranusinfor.SetActive(false);
                    Mecuryinfor.SetActive(false);
                    //Suninfor.SetActive(false);
                    if (temp != null)
                    {
                        temp.transform.localScale -= new Vector3(0.04f, 0.04f, 0.04f);
                        temp = null;
                    }

                }
            }
        }
        private bool statusaligment = false;
        //planet aligment
        public void buttonclick()
        {
            if (statusaligment == false)
            {
                Earthinfor.SetActive(true);
                Jupiterinfor.SetActive(true);
                Neptuneinfor.SetActive(true);
                Saturninfor.SetActive(true);
                Marsinfor.SetActive(true);
                Venusinfor.SetActive(true);
                Uranusinfor.SetActive(true);
                Mecuryinfor.SetActive(true);
                //Suninfor.SetActive(true);
                statusaligment = true;
                //DialogAligment.SetActive(true);
            }
            else
            {
                Earthinfor.SetActive(false);
                Jupiterinfor.SetActive(false);
                Neptuneinfor.SetActive(false);
                Saturninfor.SetActive(false);
                Marsinfor.SetActive(false);
                Venusinfor.SetActive(false);
                Uranusinfor.SetActive(false);
                Mecuryinfor.SetActive(false);
                //Suninfor.SetActive(false);
                statusaligment = false;
                //DialogAligment.SetActive(false);
            }
        }

    }
}