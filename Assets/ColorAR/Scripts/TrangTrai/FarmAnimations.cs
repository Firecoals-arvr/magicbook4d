﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Color
{
    public class FarmAnimations : MonoBehaviour
    {
        public GameObject pig, cow, all;
        public void PlayAnimFarm()
        {
            all.GetComponent<Animator>().SetTrigger("Move");
            pig.GetComponent<Animator>().SetTrigger("Move");
            cow.GetComponent<Animator>().SetTrigger("Move");
            StartCoroutine(PlayAnim2());
            StartCoroutine(StopAnim());

        }
        
        IEnumerator PlayAnim2()
        {
            yield return new WaitForSeconds(6.2f);
            all.GetComponent<Animator>().SetTrigger("Move2");
            pig.GetComponent<Animator>().SetTrigger("Move");
            cow.GetComponent<Animator>().SetTrigger("Move");
        }
        IEnumerator StopAnim()
        {
            yield return new WaitForSeconds(5f);
            Debug.Log("abc");
            all.GetComponent<Animator>().SetTrigger("Idle");
            pig.GetComponent<Animator>().SetTrigger("Idle");
            cow.GetComponent<Animator>().SetTrigger("Idle");
        }
    }
}