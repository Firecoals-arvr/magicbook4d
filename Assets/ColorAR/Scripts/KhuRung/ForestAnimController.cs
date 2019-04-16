using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Color
{
    public class ForestAnimController : MonoBehaviour
    {
        public GameObject deer,all;
        public AudioClip audio;
        AudioSource audioSource;
        public void PlayAnimForest()
        {
            all.GetComponent<Animator>().SetTrigger("Move");
            deer.GetComponent<Animator>().SetTrigger("Move");
            PlayMusic();
            StartCoroutine(StopAnim());
        }
        IEnumerator StopAnim()
        {
            yield return new WaitForSeconds(20f);
            deer.GetComponent<Animator>().SetTrigger("Idle");
        }
        public void PlayMusic()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(audio);
        }
    }
}
