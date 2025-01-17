﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firecoals.Augmentation;
using Firecoals.MagicBook;

namespace Firecoals.Color
{
	public class BeachTrackableHandler : DefaultTrackableEventHandler
	{
		AssetLoader _assetLoader;
		public GameObject renderCam;

		// Start is called before the first frame update
		protected override void Start()
		{
			base.Start();
            _assetLoader = FindObjectOfType<AssetLoader>();
            if (ActiveManager.IsActiveOfflineOk(ActiveManager.NameToProjectID(ThemeController.instance.Theme))
                || mTrackableBehaviour.TrackableName.Equals("06MAYBAY_OK"))
            {
                _assetLoader.LoadGameObjectAsync("ColorAR/Prefabs/TamBien/TamBien_Group.prefab", mTrackableBehaviour.transform);
            }
        }

		protected override void OnDestroy()
		{
			base.OnDestroy();
		}

		protected override void OnTrackingFound()
		{
            EnableObject();
            base.OnTrackingFound();
		}

		protected override void OnTrackingLost()
		{
            foreach (Transform trans in mTrackableBehaviour.transform)
            {
                trans.gameObject.SetActive(false);
                trans.GetComponentInChildren<Animation>().Stop();
            }
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

