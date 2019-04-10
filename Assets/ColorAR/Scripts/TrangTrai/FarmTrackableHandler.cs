using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firecoals.Augmentation;

namespace Firecoals.Color
{
	public class FarmTrackableHandler : DefaultTrackableEventHandler
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
			//GameObject go = handler.CreateUnique("color/model/trangtrai", "Assets/ColorAR/Prefabs/TrangTrai/TrangTrai_Group.prefab");
			//if (go)
			//{
			//	GameObject farm = Instantiate(go, mTrackableBehaviour.transform);
			//	List<RC_Get_Texture> lst = new List<RC_Get_Texture>();
			//	farm.GetComponentsInChildren<RC_Get_Texture>(true, lst);
			//	foreach (var child in lst)
			//	{
			//		child.RenderCamera = renderCam.GetComponent<Camera>();
			//	}
			//}
			base.OnTrackingFound();
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
		}
	}
}

