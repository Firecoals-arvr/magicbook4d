using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firecoals.Augmentation;
using Firecoals.AssetBundles.Sound;

namespace Firecoals.Color
{
	public class CampingTrackableHandler : DefaultTrackableEventHandler
	{
		AssetHandler handler;
		public GameObject renderCam;
        private LoadSoundBundlesColor _loadSoundBundles;
        public string tagSound;
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
            GameObject go = handler.CreateUnique("color/model/camtrai", "Assets/ColorAR/Prefabs/CamTrai/CamTrai_Group.prefab");
            _loadSoundBundles = GameObject.FindObjectOfType<LoadSoundBundlesColor>();
            if (go)
            {
                GameObject camp = Instantiate(go, mTrackableBehaviour.transform);
                CreateCloud(camp.transform);
                List<RC_Get_Texture> lst = new List<RC_Get_Texture>();
                camp.GetComponentsInChildren<RC_Get_Texture>(true, lst);
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
            GameObject cloud_1 = handler.CreateUnique("color/model/cloud/camtrai", "Assets/ColorAR/Prefabs/Cloud/Camtrai/c1.prefab");
            Instantiate(cloud_1, parent.transform.GetChild(1));
            GameObject cloud_2 = handler.CreateUnique("color/model/cloud/camtrai", "Assets/ColorAR/Prefabs/Cloud/Camtrai/c2.prefab");
            Instantiate(cloud_2, parent.transform.GetChild(1));
        }
    }
}

