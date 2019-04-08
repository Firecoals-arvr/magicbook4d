using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Color
{
	public class GirlActionController : MonoBehaviour
	{
		public void PlayBall()
		{
			this.GetComponent<Animator>().SetTrigger("isPlay");
		}
	}
}

