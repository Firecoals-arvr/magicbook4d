using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Color
{
    public class FarmAnimations : MonoBehaviour
    {
        public GameObject pig, cow, all;
        public new AudioClip audio;
        AudioSource audioSource;
        public void PlayAnimFarm()
        {
            all.GetComponent<Animator>().SetTrigger("Move");
            pig.GetComponent<Animator>().SetTrigger("Move");
            cow.GetComponent<Animator>().SetTrigger("Move");
            gameObject.GetComponent<BoxCollider>().enabled = false;
            PlayMusic();
            StartCoroutine(StopAnim());
        }
        IEnumerator StopAnim()
        {
            yield return new WaitForSeconds(40f);
            pig.GetComponent<Animator>().SetTrigger("Idle");
            cow.GetComponent<Animator>().SetTrigger("Idle");
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
        public void PlayMusic()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(audio);
        }
    }
}