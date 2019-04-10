using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    /// <summary>
    /// class này để chạy 2 animation là nhật thực và nguyệt thực
    /// </summary>
    public class EclipseAndLunar : MonoBehaviour
    {
        public Animator anim;
        bool isLunar, isEclipse;
        public GameObject moon;
        public GameObject earth;
        private void Start()
        {
            // tắt animator để earth chạy quanh sun
            anim.GetComponent<Animator>().enabled = false;
            // biến để check cho chạy animation
            isLunar = true;
            isEclipse = true;
        }
        public void Eclipse()
        {
            //bật animator để chạy animation 
            anim.GetComponent<Animator>().enabled = true;
            //nếu ấn vào nút nhật thực
            if (isEclipse == true)
            {
                //cho earth rotate từ vector3(0,0,0)
                earth.transform.localRotation = Quaternion.identity;
                // tắt các script của earth và moon để chạy anim dc chính xác
                moon.GetComponent<Autorun>().enabled = false;
                moon.GetComponent<SelfRotate>().enabled = false;
                earth.GetComponent<Autorun>().enabled = false;
                earth.GetComponent<SelfRotate>().enabled = false;
                // chạy anim
                anim.SetTrigger("SetEclipse");
                StartCoroutine(WaitASecond("EclipseOpen", new Quaternion(0, 0, 0,0),9f));

            }
            //nếu ấn lại vào nút nhật thực lần 2
            else
            {
                //chạy anim 
                anim.SetTrigger("EclipseClose");
                //đợi cho chạy hết anim rồi cho các script kia chạy bt
                StartCoroutine(WaitForEndAnim());
                moon.GetComponent<Autorun>().enabled = true;
                moon.GetComponent<SelfRotate>().enabled = true;
                earth.GetComponent<Autorun>().enabled = true;
                earth.GetComponent<SelfRotate>().enabled = true;
            }
            
            isEclipse = !isEclipse;
        }
        // check nguyệt thực giống vs nhật thực
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
                // đợi chạy hết anim setlunar rồi chạy anim tiếp theo
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