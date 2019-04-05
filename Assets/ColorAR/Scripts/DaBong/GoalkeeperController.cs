using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Firecoals.Color
{
	public class GoalkeeperController : MonoBehaviour
	{
		// Start is called before the first frame update
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}

		public void Catch()
		{
			var randomAction = Random.Range(0, 2);
			//Debug.LogWarning(randomAction);
			GetComponent<Animator>().SetInteger("Catch", randomAction);
		}
	}
}

