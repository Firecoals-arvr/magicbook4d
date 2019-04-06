using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firecoals.Augmentation;

namespace Firecoals.Color
{
	public class MatchTrackableHandler : DefaultTrackableEventHandler
	{
		AssetHandler handler;
		public GameObject renderCam;

		// Start is called before the first frame update
		protected override void Start()
		{
			base.Start();
			handler = new AssetHandler(mTrackableBehaviour.transform);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
		}

		protected override void OnTrackingFound()
		{
			//GameObject go = handler.CreateUnique("color/model/dabong", "Assets/ColorAR/Prefabs/DaBong/DaBong_Group.prefab");
			//if (go)
			//{
			//	GameObject match = Instantiate(go, mTrackableBehaviour.transform);
			//	List<RC_Get_Texture> lst = new List<RC_Get_Texture>();
			//	match.GetComponentsInChildren<RC_Get_Texture>(true, lst);
			//	foreach (var child in lst)
			//	{
			//		child.RenderCamera = renderCam.GetComponent<Camera>();
			//	}
			//}
			base.OnTrackingFound();
			GameObject ground = GameObject.FindGameObjectWithTag("Ground");
			if (ground.activeSelf == true)
			{
				ground.GetComponent<BoxCollider>().enabled = true;
			}

			GameObject ball = GameObject.FindGameObjectWithTag("Ball");
			if (ball.activeSelf == true)
			{
				ball.transform.GetChild(0).GetComponent<BoxCollider>().enabled = true;
			}
		}

		protected override void OnTrackingLost()
		{
			//handler?.ClearAll();
			//handler?.Content.ClearAll();
			//foreach (Transform trans in mTrackableBehaviour.transform)
			//{
			//	Destroy(trans.gameObject);
			//}
			base.OnTrackingLost();
			var colliderComponents = GetComponentsInChildren<Collider>(true);
			foreach (var component in colliderComponents)
				component.enabled = true;
		}
	}
}

