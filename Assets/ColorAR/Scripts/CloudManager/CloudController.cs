using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Color
{
	public class CloudController : MonoBehaviour
	{
		public Vector3 center = new Vector3(0, 0.1f, 0);
		public Vector3 size = new Vector3(0.3f, 0.3f, 0.3f);


		private void OnDrawGizmosSelected()
		{
			Gizmos.color = new UnityEngine.Color(0.8f, 0, 0, 1f);
			Gizmos.DrawCube(center, size);
		}


	}
}

