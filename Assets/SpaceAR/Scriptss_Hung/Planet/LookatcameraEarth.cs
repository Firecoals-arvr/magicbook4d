using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookatcameraEarth : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        if ((int)gameObject.transform.parent.parent.parent.localEulerAngles.x == 90)
        {
            lookat = Vector3.forward;
        }
    }
    //check target dung hay nam
    private Vector3 lookat;
    // Update is called once per frame
    void Update () {

        if ((int)gameObject.transform.parent.parent.parent.localEulerAngles.x == 90)
        {
            transform.LookAt(transform.Find("/ARCamera/Camera"), Vector3.forward);
        }
        else
        {
            transform.LookAt(transform.Find("/ARCamera/Camera"));
        }
       
        //transform.LookAt(transform.Find("/Camera"));
    }
}
