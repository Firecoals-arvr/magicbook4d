using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    public class EclipseAndLunar : MonoBehaviour
    {
        public Animator anim;
        bool isLunar, isEclipse;
        public GameObject moon;
        public GameObject earth;
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
                earth.transform.localRotation = Quaternion.identity;
                moon.GetComponent<Autorun>().enabled = false;
                moon.GetComponent<SelfRotate>().enabled = false;
                earth.GetComponent<Autorun>().enabled = false;
                earth.GetComponent<SelfRotate>().enabled = false;
                
                anim.SetTrigger("SetEclipse");
                StartCoroutine(WaitASecond("EclipseOpen", new Quaternion(0, 0, 0,0),9f));

            }
            else
            {
                anim.SetTrigger("EclipseClose");
                StartCoroutine(WaitForEndAnim());
                moon.GetComponent<Autorun>().enabled = true;
                moon.GetComponent<SelfRotate>().enabled = true;
                earth.GetComponent<Autorun>().enabled = true;
                earth.GetComponent<SelfRotate>().enabled = true;
            }
            isEclipse = !isEclipse;
        }

        public void Lunar()
        {
            anim.GetComponent<Animator>().enabled = true;
            if (isLunar == true)
            {
                earth.transform.localRotation = Quaternion.identity;
                moon.GetComponent<Autorun>().enabled = false;
                moon.GetComponent<SelfRotate>().enabled = false;
                earth.GetComponent<Autorun>().enabled = false;
                earth.GetComponent<SelfRotate>().enabled = false;
                anim.SetTrigger("SetLunar");
                StartCoroutine(WaitASecond("LunarOpen", new Quaternion(0, 180, 0,0),9.2f));

            }
            else
            {
                anim.SetTrigger("LunarClose");
                StartCoroutine(WaitForEndAnim());
                moon.GetComponent<Autorun>().enabled = true;
                moon.GetComponent<SelfRotate>().enabled = true;
                earth.GetComponent<Autorun>().enabled = true;
                earth.GetComponent<SelfRotate>().enabled = true;
            }
            isLunar = !isLunar;
        }
        IEnumerator WaitASecond(string name, Quaternion a,float time)
        {
            yield return new WaitForSeconds(time);
            earth.transform.localRotation = a;
            anim.SetTrigger(name);
        }
        IEnumerator WaitForEndAnim()
        {
            yield return new WaitForSeconds(1f);
            anim.GetComponent<Animator>().enabled = false;
        }

    }
}