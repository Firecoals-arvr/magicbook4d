using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Color
{
	public class MoveManAndCamel : MonoBehaviour
	{
		public GameObject manAndCamel;

		// Start is called before the first frame update
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}

		public void TouchToCharacter()
		{
			manAndCamel.GetComponent<Animator>().SetTrigger("isWave");
		}
	}
}

