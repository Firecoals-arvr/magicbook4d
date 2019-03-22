using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireCoals.Space
{
    public class EclipseAndLunar : MonoBehaviour
    {
        public Animator anim;
        bool isLunar, isEclipse;
        public GameObject moon;
        private void Start()
        {
            anim.GetComponent<Animator>().enabled = false;

            isLunar = true;
            isEclipse = true;
        }
        public void Eclipse()
        {

            anim.GetComponent<Animator>().enabled = true;
            if (isEclipse == true)
            {
                moon.transform.localPosition = new Vector3(-2.21f, 0, 0);
                anim.SetTrigger("SetupEclipse");
                StartCoroutine(WaitASecond("EclipseOpen", new Vector3(-2.21f, 0, 0)));

            }
            else
            {
                anim.SetTrigger("EclipseClose");
                StartCoroutine(WaitForEndAnim());
            }
            isEclipse = !isEclipse;
        }

        public void Lunar()
        {
            anim.GetComponent<Animator>().enabled = true;
            if (isLunar == true)
            {
                moon.transform.localPosition = new Vector3(-2.21f, 0, 0);
                anim.SetTrigger("SetupLunar");
                StartCoroutine(WaitASecond("LunarOpen", new Vector3(2.21f, 0, 0)));

            }
            else
            {
                anim.SetTrigger("LunarClose");
                StartCoroutine(WaitForEndAnim());
            }
            isLunar = !isLunar;
        }
        IEnumerator WaitASecond(string name, Vector3 a)
        {
            yield return new WaitForSeconds(4.6f);
            moon.transform.localPosition = a;
            anim.SetTrigger(name);
        }
        IEnumerator WaitForEndAnim()
        {
            yield return new WaitForSeconds(1f);
            anim.GetComponent<Animator>().enabled = false;
        }

        //    bool isEclipse,isLunar;
        //    public GameObject Earth;
        //    public Animator anim1,anim2;

        //    private void Start()
        //    {
        //        isEclipse = false;
        //        isLunar = false;
        //    }

        //    public void Eclipse()
        //    {

        //        if (isEclipse == true)
        //        {
        //            EclipseClose();
        //        }
        //        else
        //        {
        //            this.gameObject.GetComponent<BoxCollider>().enabled = true;
        //            this.gameObject.GetComponent<BoxCollider>().size = new Vector3(2, 0.1f, 0);
        //            this.gameObject.GetComponent<BoxCollider>().center = new Vector3(0.37f, 0, 0);
        //            Earth.transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;

        //        }
        //        isEclipse = false;

        //    }
        //    public void Lunar()
        //    {
        //        if (isLunar == true)
        //        {
        //            LunarClose();
        //        }
        //        else
        //        {
        //            this.gameObject.GetComponent<BoxCollider>().enabled = true;
        //            this.gameObject.GetComponent<BoxCollider>().size = new Vector3(2.2f, 0.1f, 0);
        //            this.gameObject.GetComponent<BoxCollider>().center = new Vector3(-0.33f, 0, 0);
        //            Earth.transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
        //        }
        //        isLunar = false;

        //    }
        //    void EclipseClose()
        //    {

        //        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        //        Earth.GetComponent<SunSelfRotate>().enabled = true;
        //        Earth.GetComponent<Autorun>().enabled = true;
        //        Earth.transform.GetChild(0).GetComponent<BoxCollider>().enabled = true;
        //        Earth.transform.GetChild(0).GetComponent<MoonRotateAroundEarth>().enabled = true;
        //        anim1.GetComponent<Animator>().Play("EclipseClose");
        //    }
        //    void LunarClose()
        //    {

        //        this.gameObject.GetComponent<BoxCollider>().enabled = false;

        //        Earth.GetComponent<Autorun>().enabled = true;
        //        Earth.GetComponent<SunSelfRotate>().enabled = true;


        //        Earth.transform.GetChild(0).GetComponent<BoxCollider>().enabled = true;
        //        Earth.transform.GetChild(0).GetComponent<MoonRotateAroundEarth>().enabled = true;
        //        anim2.GetComponent<Animator>().Play("LunarClose");
        //    }
        //    private void OnTriggerEnter(Collider other)
        //    {
        //        if (other.gameObject.name == "Earth1")
        //        {
        //            Debug.Log("dkldakdka");
        //            Earth.GetComponent<Autorun>().enabled = false;
        //            Earth.transform.GetChild(0).GetComponent<BoxCollider>().enabled = true;


        //        }
        //        if (other.gameObject.name == "Moon")
        //        {
        //            Debug.Log(Earth.transform.GetChild(0).transform.position);
        //            isEclipse = true;
        //            isLunar = true;
        //            Earth.GetComponent<SunSelfRotate>().enabled = false;
        //            Earth.transform.GetChild(0).GetComponent<MoonRotateAroundEarth>().enabled = false;
        //            if (Earth.transform.GetChild(0).transform.localPosition.z>0)
        //            {
        //                Debug.Log("abc");
        //                anim2.GetComponent<Animator>().Play("LunarOpen");
        //            }
        //            else if (Earth.transform.GetChild(0).transform.localPosition.z<0)
        //            {
        //                Debug.Log("def");
        //                anim1.GetComponent<Animator>().Play("EclipseOpen");
        //            }
        //        }

        //    }

    }
}