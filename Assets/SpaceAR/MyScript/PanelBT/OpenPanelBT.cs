using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OpenPanelBT : MonoBehaviour
{
    public GameObject panel,panelInfo;
    public GameObject close, open;
    bool clickAgain;

    private void Start()
    {
        clickAgain = true;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickAgain = false;
            if (clickAgain == false)
            {
                panelInfo.transform.DOLocalMove(new Vector3(-10, 1300, 0), 1f);
                clickAgain = true;
            }

        }
    }
    public void ChangePositionOp()
    {
        NGUITools.SetActive(open, false);
        panel.transform.DOLocalMove(close.transform.localPosition-new Vector3(140,0,0),1f);
        NGUITools.SetActive(close, true);
    }
    public void ChangePositionCl()
    {
        NGUITools.SetActive(close, false);
        panel.transform.DOLocalMove(open.transform.localPosition-new Vector3(160,0,0), 1f);
        NGUITools.SetActive(open, true);
    }
    public void ChangePositionInfo()
    {

        if (clickAgain == true)
        {
            panelInfo.transform.DOMove(Vector3.zero, 1f);
           
        }
        else
        {
            panelInfo.transform.DOLocalMove(new Vector3(-10, 1300, 0), 1f);
           
        }
        
        
    }

}
