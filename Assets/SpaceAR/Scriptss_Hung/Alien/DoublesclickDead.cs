using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoublesclickDead : MonoBehaviour
{

    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.5f;
    bool DoubleClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Alien")
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
    [SerializeField]
    private GameObject AlienMain;
    private void Start()
    {
        anima = GetComponent<Animation>();
    }
    private void Update()
    {
        if (AlienMain != null)
        {
            //if (AlienMain.GetComponent<Animation>().IsPlaying("Dance"))
            //{
            //    anima.Play("Dance");
            //}
            //else
            //{
            if (!anima.IsPlaying("Idle1"))
            {
                anima.Play("Idle1");
            }
            //}
        }

        if (DoubleClick())
        {
            anima.Play("Dead");
        }
    }
    private Animation anima;
}
