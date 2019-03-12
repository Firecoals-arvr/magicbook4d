using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EclipseAndLunar : MonoBehaviour
{
    bool isEclipse,isLunar;
    public GameObject Earth;
    public Animator anim1,anim2;

    private void Start()
    {
        isEclipse = false;
        isLunar = false;
    }

    public void Eclipse()
    {

        if (isEclipse == true)
        {
            EclipseClose();
        }
        else
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = true;
            this.gameObject.GetComponent<BoxCollider>().size = new Vector3(2, 0.1f, 0);
            this.gameObject.GetComponent<BoxCollider>().center = new Vector3(0.37f, 0, 0);
            Earth.transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
            
        }
        isEclipse = false;

    }
    public void Lunar()
    {
        if (isLunar == true)
        {
            LunarClose();
        }
        else
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = true;
            this.gameObject.GetComponent<BoxCollider>().size = new Vector3(2.2f, 0.1f, 0);
            this.gameObject.GetComponent<BoxCollider>().center = new Vector3(-0.33f, 0, 0);
            Earth.transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
        }
        isLunar = false;
        
    }
    void EclipseClose()
    {
        
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        Earth.GetComponent<SunSelfRotate>().enabled = true;
        Earth.GetComponent<Autorun>().enabled = true;
        Earth.transform.GetChild(0).GetComponent<BoxCollider>().enabled = true;
        anim1.GetComponent<Animator>().Play("EclipseClose");
    }
    void LunarClose()
    {
        
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        
        Earth.GetComponent<Autorun>().enabled = true;
        Earth.GetComponent<SunSelfRotate>().enabled = true;
       

        Earth.transform.GetChild(0).GetComponent<BoxCollider>().enabled = true;
        anim2.GetComponent<Animator>().Play("LunarClose");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Earth1")
        {
            Debug.Log("dkldakdka");
            Earth.GetComponent<Autorun>().enabled = false;
            Earth.transform.GetChild(0).GetComponent<BoxCollider>().enabled = true;


        }
        if (other.gameObject.name == "Moon")
        {
            Debug.Log(Earth.transform.GetChild(0).transform.position);
            isEclipse = true;
            isLunar = true;
            Earth.GetComponent<SunSelfRotate>().enabled = false;
            if (Earth.transform.GetChild(0).transform.position == new Vector3(0.5f, -0.9f, -3.8f))
            {
                Debug.Log("abc");
                anim2.GetComponent<Animator>().Play("EclipseOpen");
            }
            else if (Earth.transform.GetChild(0).transform.position == new Vector3(0.8f, -0.9f, -3.8f))
            {
                Debug.Log("def");
                anim1.GetComponent<Animator>().Play("LunarOpen");
            }
        }
        
    }
    
}
