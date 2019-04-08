using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Color
{
	/// <summary>
	/// 
	/// </summary>
	public class ActionsController : MonoBehaviour
	{
		public GameObject propeller;

		// Start is called before the first frame update
		void Start()
		{
			//CreateAnimation();
		}

		// Update is called once per frame
		void Update()
		{
			//HideObjectInfo();
			RotatePropeller();
		}

		public void CreateAnimation()
		{
			Debug.Log("May bay was clicked!");
			StartCoroutine(RotateAirplane());
			this.GetComponent<Animator>().SetTrigger("isWave");
		}

		public void RotatePropeller()
		{
			propeller.transform.Rotate(new Vector3(0, 0, 500) * Time.deltaTime, Space.Self);
		}

		//private void HideObjectInfo()
		//{
		//	if (Input.GetMouseButtonDown(0))
		//	{
		//		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		//		if (Physics.Raycast(ray, out RaycastHit hit))
		//		{
		//			if (hit.transform.tag == "Airplane")
		//			{
		//				CreateAnimation();
		//			}
		//		}
		//	}
		//}

		public IEnumerator RotateAirplane()
		{
			//this.gameObject.GetComponent<BoxCollider>().enabled = false;
			Debug.Log("May bay was clicked!");
			float duration = 3f;

			float counter = 0f;

			while (counter < duration)
			{
				counter += Time.deltaTime;
				this.transform.RotateAround(new Vector3(0, 0.1f, 0), Vector3.right, 120 * Time.deltaTime);
				yield return null;
			}
			//this.gameObject.GetComponent<BoxCollider>().enabled = true;

		}

	}
}


