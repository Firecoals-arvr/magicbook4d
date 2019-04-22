using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Color
{
	public class MoveManAndCamel : MonoBehaviour
	{
		public GameObject manAndCamel;
        public void TouchToCharacter()
		{
			manAndCamel.GetComponent<Animator>().SetTrigger("isWave");
        }
        
    }
}

