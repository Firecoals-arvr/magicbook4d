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
		public GameObject groupMayBay, propeller;

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
			//groupMayBay.transform.RotateAround(new Vector3(0, 20f, 0), Vector3.left, 90 * Time.deltaTime);
			StartCoroutine(RotateAirplane());
		}

		public void RotatePropeller()
		{
			propeller.transform.Rotate(new Vector3(0, 0, 200) * Time.deltaTime, Space.Self);
		}

		private void HideObjectInfo()
		{
			if (Input.GetMouseButtonDown(0))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out RaycastHit hit))
				{
					if (hit.transform.tag == "Airplane")
					{
						CreateAnimation();
					}
				}
			}
		}

		public IEnumerator RotateAirplane()
		{
			float duration = 2f;

			float counter = 0f;

			while (counter < duration)
			{
				counter += Time.deltaTime;
				groupMayBay.transform.RotateAround(new Vector3(0, 0.1f, 0), Vector3.right, 180 * Time.deltaTime);
				yield return null;
			}
		}
	}
}


