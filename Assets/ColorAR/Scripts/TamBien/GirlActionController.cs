using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Color
{
	public class GirlActionController : MonoBehaviour
	{
        public AudioClip audio;
        AudioSource audioSource;
        public void PlayBall()
		{
			this.GetComponent<Animator>().SetTrigger("isPlay");
            PlayMusic();

        }
        public void PlayMusic()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(audio);
        }
    }
}

