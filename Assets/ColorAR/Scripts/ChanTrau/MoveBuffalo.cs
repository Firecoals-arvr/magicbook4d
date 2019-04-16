using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Color
{
	public class MoveBuffalo : MonoBehaviour
	{
		public GameObject realCharacter;

		Vector3[] posArray = new Vector3[21];

		Vector3[] rotArray = new Vector3[21];
        public AudioClip audio;
        AudioSource audioSource;

        // Start is called before the first frame update
        void Start()
		{
			//posArray[0] = new Vector3(0, 0, 0);
			//posArray[1] = new Vector3(-0.154f, 0f, 0.378f);
			//posArray[2] = new Vector3(-0.348f, 0f, 0.53f);

			//rotArray[0] = new Vector3(0, 0, 0);
			//rotArray[1] = new Vector3(0, -40f, 0);
			//rotArray[2] = new Vector3(0, -67.782f, 0);

			//MoveOnPath();
		}

		// Update is called once per frame
		void Update()
		{

		}



		public void MoveOnPath()
		{
			//Sequence s = DOTween.Sequence();
			//for (int i = 0; i < posArray.Length; ++i)
			//{
			//	Vector3 toPos = posArray[i];
			//	Vector3 toRot = rotArray[i];
			//	s.Append(this.transform.DOLocalMove(toPos, 1.5f));
			//	s.Join(this.transform.DOLocalRotate(toRot, 1.5f));
			//}
		}

		public void MoveCharacter()
		{
			this.gameObject.GetComponent<Animator>().SetTrigger("isWalk");

			realCharacter.GetComponent<Animator>().SetTrigger("isMove");
            PlayMusic();
			StartCoroutine(EndMove());

		}
		IEnumerator EndMove()
		{
			yield return new WaitForSeconds(28.5f);
			realCharacter.GetComponent<Animator>().SetTrigger("isStay");
		}
        public void PlayMusic()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(audio);
        }
    }
}
