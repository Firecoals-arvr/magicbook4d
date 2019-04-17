using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Color
{
	public class LockColor : MonoBehaviour
	{
		public static bool isLockColor;

		public GameObject renderCam;

		private void Awake()
		{
			isLockColor = true;
		}

		public void ToggleLockButton()
		{
			if (!isLockColor)
			{
				renderCam.SetActive(true);
				isLockColor = true;
			}
			else
			{
				renderCam.SetActive(false);
				isLockColor = false;
			}
		}
	}
}
