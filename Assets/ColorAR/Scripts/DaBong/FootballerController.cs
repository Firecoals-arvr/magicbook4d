using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Color
{
	public class FootballerController : MonoBehaviour
	{
		Animator animator;

		private void Awake()
		{

		}

		private void Start()
		{
			animator = this.gameObject.GetComponent<Animator>();
		}

		public void KickBall()
		{
			animator.SetTrigger("isKick");
		}

		public void ReturnInitPos()
		{

		}

		bool isMoving = false;
		public IEnumerator ActionShoot(GameObject obj, Vector3 startPos, Vector3 endPos, float duration)
		{
			if (isMoving)
			{
				yield break;
			}

			isMoving = true;

			float counter = 0;

			while (counter < duration)
			{
				counter += Time.deltaTime;
				obj.transform.position = Vector3.Lerp(startPos, endPos, counter / duration);
				yield return null;
			}

			isMoving = false;
		}


	}
}

