using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllButtonController : MonoBehaviour
{
    public GameObject panelBT;
    public GameObject closeBt,openBt;
    public void OpenBtPanel()
    {
        NGUITools.SetActive(panelBT, true);
        NGUITools.SetActive(closeBt, true);
        NGUITools.SetActive(openBt, false);
    }
    public void CloseBtPanel()
    {
        NGUITools.SetActive(panelBT, false);
        NGUITools.SetActive(closeBt, false);
        NGUITools.SetActive(openBt, true);
    }
}
