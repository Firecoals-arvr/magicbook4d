using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Color
{
	/// <summary>
	/// 
	/// </summary>
	public class ActionsController : MonoBehaviour
	{
		public GameObject propeller;
        public new AudioClip audio;
        AudioSource audioSource;
		// Start is called before the first frame update
		void Start()
		{
			//CreateAnimation();
		}

		// Update is called once per frame
		void Update()
		{
			//HideObjectInfo();
			RotatePropeller();
		}

		public void CreateAnimation()
		{
			Debug.Log("May bay was clicked!");
            gameObject.GetComponent<BoxCollider>().enabled = false;
			StartCoroutine(RotateAirplane());
            PlayMusic();
            this.GetComponent<Animator>().SetTrigger("isWave");
		}

		public void RotatePropeller()
		{
			propeller.transform.Rotate(new Vector3(0, 0, 500) * Time.deltaTime, UnityEngine.Space.Self);
		}

		public IEnumerator RotateAirplane()
		{
			Debug.Log("May bay was clicked!");
			float duration = 3f;

			float counter = 0f;

			while (counter < duration)
			{
				counter += Time.deltaTime;
				this.transform.RotateAround(new Vector3(0, 0.1f, 0), Vector3.right, 120 * Time.deltaTime);
				yield return null;
			}
            yield return new WaitForSeconds(3f);
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
        public void PlayMusic()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(audio);
        }

	}
}


