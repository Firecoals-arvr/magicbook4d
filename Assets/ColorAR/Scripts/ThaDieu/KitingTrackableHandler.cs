using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firecoals.Augmentation;
using Firecoals.AssetBundles.Sound;
using UnityEngine.SceneManagement;

namespace Firecoals.Color
{
	public class KitingTrackableHandler : DefaultTrackableEventHandler
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
            if (ActiveManager.IsActiveOfflineOk("C"))
            {
                GameObject go = handler.CreateUnique("color/model/thadieu", "Assets/ColorAR/Prefabs/ThaDieu/ThaDieu_Group.prefab");
                _loadSoundBundles = GameObject.FindObjectOfType<LoadSoundBundlesColor>();
                if (go)
                {
                    GameObject kiting = Instantiate(go, mTrackableBehaviour.transform);
                    List<RC_Get_Texture> lst = new List<RC_Get_Texture>();
                    kiting.GetComponentsInChildren<RC_Get_Texture>(true, lst);
                    foreach (var child in lst)
                    {
                        child.RenderCamera = renderCam.GetComponent<Camera>();
                    }
                }
                _loadSoundBundles.PlaySound(tagSound);
            }
            else
            {
                PopupManager.PopUpDialog("", "Bạn cần kích hoạt để sử dụng hết các tranh", default, default, default, PopupManager.DialogType.YesNoDialog, () =>
                {
                    SceneManager.LoadScene("Activate", LoadSceneMode.Additive);
                });
            }
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
	}
}


