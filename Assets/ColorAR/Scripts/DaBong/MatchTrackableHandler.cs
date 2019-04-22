using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firecoals.Augmentation;
using Firecoals.AssetBundles.Sound;

namespace Firecoals.Color
{
	public class MatchTrackableHandler : DefaultTrackableEventHandler
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
            GameObject go = handler.CreateUnique("color/model/dabong", "Assets/ColorAR/Prefabs/DaBong/DaBong_Group.prefab");
            _loadSoundBundles = GameObject.FindObjectOfType<LoadSoundBundlesColor>();
            if (go)
            {
                GameObject match = Instantiate(go, mTrackableBehaviour.transform);
                CreateCloud(match.transform);
                List<RC_Get_Texture> lst = new List<RC_Get_Texture>();
                match.GetComponentsInChildren<RC_Get_Texture>(true, lst);
                foreach (var child in lst)
                {
                    child.RenderCamera = renderCam.GetComponent<Camera>();
                }
            }
            _loadSoundBundles.PlaySound(tagSound);
            base.OnTrackingFound();
			GameObject ground = GameObject.FindGameObjectWithTag("Ground");
			if (ground.activeSelf == true)
			{
				ground.GetComponent<BoxCollider>().enabled = true;
			}

			GameObject ball = GameObject.FindGameObjectWithTag("Ball");
			if (ball.activeSelf == true)
			{
				ball.transform.GetChild(0).GetComponent<SphereCollider>().enabled = true;
			}
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
			var colliderComponents = GetComponentsInChildren<Collider>(true);
			foreach (var component in colliderComponents)
				component.enabled = true;
		}
        public void CreateCloud(Transform parent)
        {
            GameObject cloud_1 = handler.CreateUnique("color/model/cloud/dabong", "Assets/ColorAR/Prefabs/Cloud/Dabong/c1.prefab");
            Instantiate(cloud_1, parent.transform.GetChild(1));
            GameObject cloud_2 = handler.CreateUnique("color/model/cloud/dabong", "Assets/ColorAR/Prefabs/Cloud/Dabong/c2.prefab");
            Instantiate(cloud_2, parent.transform.GetChild(1));
            GameObject cloud_3 = handler.CreateUnique("color/model/cloud/dabong", "Assets/ColorAR/Prefabs/Cloud/Dabong/c3.prefab");
            Instantiate(cloud_3, parent.transform.GetChild(1));
        }
    }
}

