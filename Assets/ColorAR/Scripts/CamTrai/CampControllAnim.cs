using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Color
{
    public class CampControllAnim : MonoBehaviour
    {
        public GameObject boyMain;
        public GameObject girlMain;
        public new AudioClip audio;
        AudioSource audioSource;
        public void MoveToCamp()
        {
            this.gameObject.GetComponent<Animator>().SetTrigger("MoveToCamp");
            gameObject.GetComponent<BoxCollider>().enabled = false;
            boyMain.GetComponent<Animator>().SetTrigger("FootMove");
            PlayMusic();
            StartCoroutine(EndMove("Talk"));
            StartCoroutine(EndAnim());
        }
        IEnumerator EndMove(string anim)
        {
            yield return new WaitForSeconds(4f);
            boyMain.GetComponent<Animator>().SetTrigger(anim);
            StartCoroutine(GirlMakeCamp());
        }
        IEnumerator GirlMakeCamp()
        {
            yield return new WaitForSeconds(3.5f);
            girlMain.GetComponent<Animator>().SetTrigger("Camp");
        }
        IEnumerator EndAnim()
        {
            yield return new WaitForSeconds(18f);
            girlMain.GetComponent<Animator>().SetTrigger("Idle");
            boyMain.GetComponent<Animator>().SetTrigger("Idle");
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
        public void PlayMusic()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(audio);
        }
    }
}