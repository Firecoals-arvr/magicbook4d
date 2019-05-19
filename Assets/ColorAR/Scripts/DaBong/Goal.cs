using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Color
{
    public class Goal : MonoBehaviour
    {
        public static int score;
        public GameObject ball;
        public Animator anim;
        public new AudioClip audio;
        AudioSource audioSource;

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == ball)
            {
                score += 1;
                anim.SetTrigger("AnMung");
                audioSource = GetComponent<AudioSource>();
                audioSource.PlayOneShot(audio);
            }
                
        }
    }
}
