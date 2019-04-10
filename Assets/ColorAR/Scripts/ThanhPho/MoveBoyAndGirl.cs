using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Color
{
	public class MoveBoyAndGirl : MonoBehaviour
	{
		public GameObject boy;
		public GameObject girl;

		// Start is called before the first frame update
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}

		public void TouchToBoy()
		{
			boy.GetComponent<Animator>().SetTrigger("isTalk");
		}

		public void TouchToGirl()
		{
			girl.GetComponent<Animator>().SetTrigger("isTalk");
		}
	}
}

