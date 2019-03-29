using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCanvasButtons : MonoBehaviour
{
    bool changeAnim = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowOrHideButton()
    {
        if (changeAnim == true)
        {
            GetComponent<Animation>().Play("hide");
            changeAnim = false;
        }
        else
        {
            GetComponent<Animation>().Play("show");
            changeAnim = true;
        }
    }
}
