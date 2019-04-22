using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Color
{
	public class BallTrigger : MonoBehaviour
	{
		public GoalkeeperController goalkeeper;
		public Collider footballer;
        public new AudioClip audio;
        AudioSource audioSource;
		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject == footballer.gameObject)
			{
                audioSource = GetComponent<AudioSource>();
                audioSource.PlayOneShot(audio);
				goalkeeper.Catch();
			}
		}
	}
}