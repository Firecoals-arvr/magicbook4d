using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Color
{
    public class ForestAnimController : MonoBehaviour
    {
        public GameObject deer,all;
        public void PlayAnimForest()
        {
            all.GetComponent<Animator>().SetTrigger("Move");
            deer.GetComponent<Animator>().SetTrigger("Move");

        }
        IEnumerator StopAnim()
        {
            yield return new WaitForSeconds(20f);
            deer.GetComponent<Animator>().SetTrigger("Idle");
        }
    }
}
