using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firecoals.Augmentation;
using Firecoals.AssetBundles.Sound;

namespace Firecoals.Color
{
	public class MatchTrackableHandler : DefaultTrackableEventHandler
	{
		public GameObject renderCam;
        private LoadSoundBundlesColor _loadSoundBundles;
        public string tagSound;

        private AssetLoader _assetLoader;
        // Start is called before the first frame update
        protected override void Start()
		{
			base.Start();
            _assetLoader = FindObjectOfType<AssetLoader>();
        }

		protected override void OnDestroy()
		{
			base.OnDestroy();
		}

		protected override void OnTrackingFound()
		{
            GameObject go = _assetLoader.LoadGameObjectSync("color/model/dabong", "Assets/ColorAR/Prefabs/DaBong/DaBong_Group.prefab");
            _loadSoundBundles = GameObject.FindObjectOfType<LoadSoundBundlesColor>();
            if (go)
            {
                GameObject match = Instantiate(go, mTrackableBehaviour.transform);
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

    }
}

