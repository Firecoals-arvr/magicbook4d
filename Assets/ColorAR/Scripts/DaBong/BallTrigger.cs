using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Color
{
	public class BallTrigger : MonoBehaviour
	{
		public GoalkeeperController goalkeeper;
		public Collider footballer;

		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject == footballer.gameObject)
			{
				goalkeeper.Catch();
			}
		}
	}
}