using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firecoals.Augmentation;
using Firecoals.AssetBundles.Sound;
using Firecoals.MagicBook;

namespace Firecoals.Color
{
	public class MatchTrackableHandler : DefaultTrackableEventHandler
	{
		public GameObject renderCam;
        private LoadSoundBundlesColor _loadSoundBundles;
        public string tagSound;
        bool playSound;
        private AssetLoader _assetLoader;
        // Start is called before the first frame update
        protected override void Start()
		{
			base.Start();
            _assetLoader = FindObjectOfType<AssetLoader>();
            if (ActiveManager.IsActiveOfflineOk(ActiveManager.NameToProjectID(ThemeController.instance.Theme))
                || mTrackableBehaviour.TrackableName.Equals("06MAYBAY_OK"))
            {
                _assetLoader.LoadGameObjectAsync("ColorAR/Prefabs/DaBong/DaBong_Group.prefab", mTrackableBehaviour.transform);
            }
            playSound = true;
        }

		protected override void OnDestroy()
		{
			base.OnDestroy();
		}

		protected override void OnTrackingFound()
		{
            _loadSoundBundles = GameObject.FindObjectOfType<LoadSoundBundlesColor>();
            if (playSound)
            {
                _loadSoundBundles.PlaySound(tagSound);
                playSound = false;
            }
            EnableObject();
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
                trans.gameObject.SetActive(false);
                trans.GetComponentInChildren<Animation>().Stop();
            }
            playSound = true;
            FirecoalsSoundManager.StopAll();
            base.OnTrackingLost();
			var colliderComponents = GetComponentsInChildren<Collider>(true);
			foreach (var component in colliderComponents)
				component.enabled = true;
		}
        void EnableObject()
        {
            foreach (Transform trans in mTrackableBehaviour.transform)
            {
                trans.gameObject.SetActive(true);
                trans.GetComponentInChildren<Animation>().Play();
                List<RC_Get_Texture> lst = new List<RC_Get_Texture>();
                trans.GetComponentsInChildren<RC_Get_Texture>(true, lst);
                foreach (var child in lst)
                {
                    child.RenderCamera = renderCam.GetComponent<Camera>();
                }
            }
        }
    }
}

