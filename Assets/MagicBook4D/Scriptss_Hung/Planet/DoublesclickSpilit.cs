using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoublesclickSpilit : MonoBehaviour
{

    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.5f;
    public GameObject scenserobot;
    bool DoubleClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "System")
                {
                    clicked++;
                    if (clicked == 1) clicktime = Time.time;
                }

            }
        }
        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            clicked = 0;
            clicktime = 0;
            return true;
        }
        else if (clicked > 2 || Time.time - clicktime > 1) clicked = 0;
        return false;
    }

    private void Start()
    {
        temp = true;
    }
    private void Update()
    {

        if (DoubleClick())
        {
            Spilit();
        }
    }
    private bool temp;
    public void Spilit()
    {
        if (temp == true)
        {
            if (GetComponent<Animation>().IsPlaying("Open"))
            {
                GetComponent<Animation>().Play("Close");
                temp = true;
                //case target robot

                scenserobot.SetActive(true);

            }
            else
            {
                GetComponent<Animation>().Play("Open");
                temp = false;
                //case target robot

                scenserobot.SetActive(false);

            }
        }
        else
        {
            GetComponent<Animation>().Play("Close");
            temp = true;
            //case target robot
            scenserobot.SetActive(true);

        }
    }
}
