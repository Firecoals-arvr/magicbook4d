using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Color
{
    public class ForestAnimController : MonoBehaviour
    {
        public GameObject deer,all;
        public new AudioClip audio;
        AudioSource audioSource;
        public void PlayAnimForest()
        {
            all.GetComponent<Animator>().SetTrigger("Move");
            deer.GetComponent<Animator>().SetTrigger("Move");
            gameObject.GetComponent<BoxCollider>().enabled = false;
            PlayMusic();
            StartCoroutine(StopAnim());
        }
        IEnumerator StopAnim()
        {
            yield return new WaitForSeconds(30f);
            deer.GetComponent<Animator>().SetTrigger("Idle");
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
        public void PlayMusic()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(audio);
        }
    }
}
