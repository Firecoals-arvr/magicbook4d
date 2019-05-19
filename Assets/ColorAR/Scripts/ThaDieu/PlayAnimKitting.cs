using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Color
{
    public class PlayAnimKitting : MonoBehaviour
    {
        public GameObject character;
        public new AudioClip audio;
        AudioSource audioSource;
        public void PlayAnimBoy()
        {
            this.gameObject.GetComponent<Animator>().SetTrigger("Move");
            character.GetComponent<Animator>().SetTrigger("Move");
            gameObject.GetComponent<BoxCollider>().enabled = false;
            PlayMusic();
            StartCoroutine(StopAnim("Idle"));
        }
        
        IEnumerator StopAnim(string nameAnim)
        {
            yield return new WaitForSeconds(10f);
            character.GetComponent<Animator>().SetTrigger(nameAnim);
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
        public void PlayMusic()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(audio);
        }
    }
}