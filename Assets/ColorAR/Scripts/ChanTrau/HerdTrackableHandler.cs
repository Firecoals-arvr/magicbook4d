using Firecoals.AssetBundles.Sound;
using Firecoals.Augmentation;
using Firecoals.MagicBook;
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
        private bool playSound;
        // Start is called before the first frame update
        protected override void Start()
		{
			base.Start();
            _assetLoader = FindObjectOfType<AssetLoader>();
            if (ActiveManager.IsActiveOfflineOk(ActiveManager.NameToProjectID(ThemeController.instance.Theme))
                || mTrackableBehaviour.TrackableName.Equals("06MAYBAY_OK"))
            {
                _assetLoader.LoadGameObjectAsync("ColorAR/Prefabs/ChanTrau/ChanTrau_Group.prefab", mTrackableBehaviour.transform);
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
            EnableObject();
            if (playSound)
            {
                _loadSoundBundles.PlaySound(tagSound);
                playSound = false;
            }
            base.OnTrackingFound();
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
