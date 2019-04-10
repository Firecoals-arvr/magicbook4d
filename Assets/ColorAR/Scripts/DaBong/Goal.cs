using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Color
{
	public class Goal : MonoBehaviour
	{
		public static int score;

		public void OnTriggerEnter(Collider ball)
		{
			score += 1;
		}
	}
}
