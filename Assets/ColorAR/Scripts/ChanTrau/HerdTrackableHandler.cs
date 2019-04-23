using Firecoals.AssetBundles.Sound;
using Firecoals.Augmentation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Color
{
	public class HerdTrackableHandler : DefaultTrackableEventHandler
	{
		public GameObject renderCam;
        private LoadSoundBundlesColor _loadSoundBundles;
        private AssetLoader _assetLoader;
        public string tagSound;

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
            GameObject go = _assetLoader.LoadGameObjectSync("color/model/chantrau", "Assets/ColorAR/Prefabs/ChanTrau/ChanTrau_Group.prefab");
            _loadSoundBundles = GameObject.FindObjectOfType<LoadSoundBundlesColor>();
            if (go)
            {
                GameObject group = Instantiate(go, mTrackableBehaviour.transform);
                List<RC_Get_Texture> lst = new List<RC_Get_Texture>();
                group.GetComponentsInChildren<RC_Get_Texture>(true, lst);
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
            foreach (Transform trans in mTrackableBehaviour.transform)
            {
                Destroy(trans.gameObject);
            }
            FirecoalsSoundManager.StopAll();
            base.OnTrackingLost();
		}
	}

}
