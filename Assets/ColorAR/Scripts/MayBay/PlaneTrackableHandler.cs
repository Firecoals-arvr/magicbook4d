using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firecoals.Augmentation;
using Firecoals.AssetBundles.Sound;

namespace Firecoals.Color
{
    /// <summary>
    /// class để sinh model khi found tranh
    /// </summary>
	public class PlaneTrackableHandler : DefaultTrackableEventHandler
	{
		AssetHandler handler;
		public GameObject renderCam;
        private LoadSoundBundlesColor _loadSoundBundles;
        public string tagSound;
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
            GameObject go = handler.CreateUnique("color/model/maybay", "Assets/ColorAR/Prefabs/MayBay/MayBay_Group.prefab");
            _loadSoundBundles = GameObject.FindObjectOfType<LoadSoundBundlesColor>();
            if (go)
            {
                GameObject maybay = Instantiate(go, mTrackableBehaviour.transform);
                CreateCloud(maybay.transform);
                List<RC_Get_Texture> lst = new List<RC_Get_Texture>();
                maybay.GetComponentsInChildren<RC_Get_Texture>(true, lst);
                foreach (var child in lst)
                {
                    child.RenderCamera = renderCam.GetComponent<Camera>();
                }
            }
            _loadSoundBundles.PlaySound(tagSound);
            base.OnTrackingFound();
		}
		protected override void OnTrackingLost()
		{
            handler?.ClearAll();
            handler?.Content.ClearAll();
            foreach (Transform trans in mTrackableBehaviour.transform)
            {
                Destroy(trans.gameObject);
            }
            FirecoalsSoundManager.StopAll();
            base.OnTrackingLost();
		}

		public void CreateCloud(Transform parent)
		{
			GameObject cloud_1 = handler.CreateUnique("color/model/cloud/maybay", "Assets/ColorAR/Prefabs/Cloud/Maybay/c1.prefab");
			Instantiate(cloud_1, parent.transform.GetChild(1));
			GameObject cloud_2 = handler.CreateUnique("color/model/cloud/maybay", "Assets/ColorAR/Prefabs/Cloud/Maybay/c2.prefab");
			Instantiate(cloud_2, parent.transform.GetChild(1));
			GameObject cloud_3 = handler.CreateUnique("color/model/cloud/maybay", "Assets/ColorAR/Prefabs/Cloud/Maybay/c3.prefab");
			Instantiate(cloud_3, parent.transform.GetChild(1));
			GameObject cloud_4 = handler.CreateUnique("color/model/cloud/maybay", "Assets/ColorAR/Prefabs/Cloud/Maybay/c4.prefab");
			Instantiate(cloud_4, parent.transform.GetChild(1));
		}
	}
}
