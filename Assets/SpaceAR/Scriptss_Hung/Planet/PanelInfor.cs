using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Firecoals.Space
{
    public class PanelInfor : MonoBehaviour
    {
        [SerializeField] private GameObject panelInfor;
        [SerializeField] private Text textInforPanel;
        [SerializeField] private TextMesh textObject;

        //private GameObject panelInfor;

        public static GameObject select;
        //check tinh trang cua panelinfor
        bool stt = false;

        private void Start()
        {
            //panelInfor = this.gameObject.transform.Find("Canvas/PanelInfo").gameObject;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.transform.tag == "infor")
                    {
                        if (select != null)
                        {
                            select.GetComponent<Renderer>().material.color = new UnityEngine.Color(1, 1, 1);
                        }
                        select = hit.transform.gameObject;
                        hit.transform.GetComponent<Renderer>().material.color = new UnityEngine.Color(1, 0, 1);
                        if (stt == false)
                        {
                            panelInfor.GetComponent<Animation>().Play("Open");
                            stt = true;
                        }
                        //if (textObject.transform.parent.name == hit.transform.name)
                        //{
                        //    textInforPanel.text = textObject.text;
                        //}
                        //panelinfor.GetComponentInChildren<Text>().text = DatabaseController.loaddatabaseinforshell(Uicontroller.nametargetGolbal, hit.transform.name);
                        //panelInfor.GetComponentInChildren<Text>().text = select.GetComponentInChildren<Text>().text;
                    }
                    else
                    {
                        if (stt == true)
                        {
                            panelInfor.GetComponent<Animation>().Play("Close");
                            stt = false;
                        }
                    }
                }
                else
                {
                    if (stt == true)
                    {
                        panelInfor.GetComponent<Animation>().Play("Close");
                        stt = false;
                    }
                }
            }
        }
    }
}