using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    /// <summary>
    /// class này d? ch?y 2 animation là nh?t th?c và nguy?t th?c
    /// </summary>
    public class EclipseAndLunar : MonoBehaviour
    {
        public Animator anim;
        bool isLunar, isEclipse;
        public GameObject moon;
        public GameObject earth;
        public GameObject Notice;
        private void Start()
        {
            // t?t animator d? earth ch?y quanh sun
            anim.GetComponent<Animator>().enabled = false;
            // bi?n d? check cho ch?y animation
            isLunar = true;
            isEclipse = true;
        }
        public void Eclipse()
        {
            //b?t animator d? ch?y animation 
            anim.GetComponent<Animator>().enabled = true;
            //n?u ?n vào nút nh?t th?c
            if (isEclipse == true)
            {
                Notice.SetActive(true);
                ChangeAnim.checkOpen = false;
                //cho earth rotate t? vector3(0,0,0)
                earth.transform.localRotation = Quaternion.identity;
                // t?t các script c?a earth và moon d? ch?y anim dc chính xác
                Idling();
                // ch?y anim
                anim.Play("SetEclipse");
                StartCoroutine(WaitASecond());

            }
            //n?u ?n l?i vào nút nh?t th?c l?n 2
            else
            {
                ChangeAnim.checkOpen = true;
                //ch?y anim 
                anim.Play("Idle");
                //d?i cho ch?y h?t anim r?i cho các script kia ch?y bt
                StartCoroutine(WaitForEndAnim());
                rotating();
            }

            isEclipse = !isEclipse;
        }
        // check nguy?t th?c gi?ng vs nh?t th?c
        public void Lunar()
        {
            anim.GetComponent<Animator>().enabled = true;
            if (isLunar == true)
            {
                Notice.SetActive(true);
                ChangeAnim.checkOpen=false;
                earth.transform.localRotation = Quaternion.identity;
                Idling();
                anim.Play("SetLunar");
                // d?i ch?y h?t anim setlunar r?i ch?y anim ti?p theo
                StartCoroutine(WaitASecond());

            }
            else
            {
                ChangeAnim.checkOpen = true;
                anim.Play("Idle");
                StartCoroutine(WaitForEndAnim());
                rotating();
            }
            isLunar = !isLunar;
        }
        IEnumerator WaitASecond()
        {
            yield return new WaitForSeconds(9.2f);
            Notice.SetActive(false);
        }
        IEnumerator WaitForEndAnim()
        {
            yield return new WaitForSeconds(1f);
            anim.GetComponent<Animator>().enabled = false;
        }
        public void rotating()
        {
            moon.GetComponent<Autorun>().enabled = true;
            moon.GetComponent<SelfRotate>().enabled = true;
            earth.GetComponent<Autorun>().enabled = true;
            earth.GetComponent<SelfRotate>().enabled = true;
        }
        public void Idling()
        {
            moon.GetComponent<Autorun>().enabled = false;
            moon.GetComponent<SelfRotate>().enabled = false;
            earth.GetComponent<Autorun>().enabled = false;
            earth.GetComponent<SelfRotate>().enabled = false;
        }
    }
}