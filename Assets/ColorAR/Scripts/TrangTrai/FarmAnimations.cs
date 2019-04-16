using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Color
{
    public class FarmAnimations : MonoBehaviour
    {
        public GameObject pig, cow, all;
        public AudioClip audio;
        AudioSource audioSource;
        public void PlayAnimFarm()
        {
            all.GetComponent<Animator>().SetTrigger("Move");
            pig.GetComponent<Animator>().SetTrigger("Move");
            cow.GetComponent<Animator>().SetTrigger("Move");
            PlayMusic();
            StartCoroutine(StopAnim());
        }
        IEnumerator StopAnim()
        {
            yield return new WaitForSeconds(30f);
            pig.GetComponent<Animator>().SetTrigger("Idle");
            cow.GetComponent<Animator>().SetTrigger("Idle");
        }
        public void PlayMusic()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(audio);
        }
    }
}