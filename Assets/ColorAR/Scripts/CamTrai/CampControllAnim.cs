using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Color
{
    public class CampControllAnim : MonoBehaviour
    {
        public GameObject boyMain;
        public GameObject girlMain;
        public AudioClip audio;
        AudioSource audioSource;
        public void MoveToCamp()
        {
            this.gameObject.GetComponent<Animator>().SetTrigger("MoveToCamp");
            boyMain.GetComponent<Animator>().SetTrigger("FootMove");
            PlayMusic();
            StartCoroutine(EndMove("Talk"));
            StartCoroutine(EndAnim());
        }
        IEnumerator EndMove(string anim)
        {
            yield return new WaitForSeconds(2f);
            boyMain.GetComponent<Animator>().SetTrigger(anim);
            StartCoroutine(GirlMakeCamp());
        }
        IEnumerator GirlMakeCamp()
        {
            yield return new WaitForSeconds(2.12f);
            girlMain.GetComponent<Animator>().SetTrigger("Camp");
        }
        IEnumerator EndAnim()
        {
            yield return new WaitForSeconds(14f);
            girlMain.GetComponent<Animator>().SetTrigger("Idle");
            boyMain.GetComponent<Animator>().SetTrigger("Idle");
        }
        public void PlayMusic()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(audio);
        }
    }
}