using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firecoals.Augmentation;
using System;
using Firecoals.AssetBundles;
using Loxodon.Framework.Bundles;
using Loxodon.Framework.Contexts;
using Firecoals.AssetBundles.Sound;
using UnityEngine.SceneManagement;
namespace Firecoals.Animal
{
    /// <summary>
    /// class dùng thay cho DefaultTrackableEventHandler
    /// </summary>
    public class LoadModleAnimal : DefaultTrackableEventHandler
    {
        /// <summary>
        /// tên object trong asset bundles
        /// </summary>
        public string BuildName;

        /// <summary>
        /// đường dẫn của object trong thư mục
        /// </summary>
        public string path;

        /// <summary>
        /// đường dẫn sound
        /// </summary>
        public float TimeStartEffect;
        private AssetHandler assetHandler { get; set; }
        private AssetLoader assetLoader { get; set; }
        private LoadSoundbundles _loadsoundbundles;
        private AudioSource audio;
        private IResources _resources;
        public string tagInfo;
        public string tagSound;
        public string tagName;

        protected override void Start()
        {

            base.Start();
            audio = gameObject.GetComponent<AudioSource>();
            assetHandler = new AssetHandler(mTrackableBehaviour.transform);
            _loadsoundbundles = GameObject.FindObjectOfType<LoadSoundbundles>();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        IEnumerator starEffect(GameObject go)
        {
            assetLoader = GameObject.FindObjectOfType<AssetLoader>();
            Debug.Log("start Effect");
            RandomEffect.Instance.Onfound(TimeStartEffect);
            yield return new WaitForSeconds(0.5f);
            GameObject.Instantiate(go, mTrackableBehaviour.transform);
            _loadsoundbundles.PlaySound(tagSound);
            _loadsoundbundles.PlayName(tagName);

        }
        protected override void OnTrackingFound()
        {
            base.OnTrackingFound();
            GameObject go = assetHandler.CreateUnique(BuildName, path);
            if (go != null)
            {
                //if (ActiveManager.IsActiveOfflineOk("A"))
                //{
                //    StartCoroutine(starEffect(go));
                //}
                //else

                //{
                //    if (mTrackableBehaviour.name == "Lion" || mTrackableBehaviour.name == "Elephant" || mTrackableBehaviour.name == "Gorilla")
                //    {
                        StartCoroutine(starEffect(go));
                //    }
                //    else
                //    {
                //        //    PopupManager.PopUpDialog("", "", PopupManager.DialogType.YesNoDialog, () =>
                //        //    {
                //        //        SceneManager.LoadScene("Activate", LoadSceneMode.Additive);
                //        //    });
                //    }
                //}
             
            }
        }
        protected override void OnTrackingLost()
        {
            base.OnTrackingLost();
            FirecoalsSoundManager.StopAll();
            RandomEffect.Instance.Onlost();
            assetHandler?.ClearAll();
            assetHandler?.Content.ClearAll();
            foreach (Transform go in mTrackableBehaviour.transform)
            {
                Destroy(go.gameObject);
            }

        }

        //private void RunAnimationIntro()
        //{
        //    if (this.gameObject.transform.GetChild(0).GetComponent<Animation>() != null)
        //    {
        //        this.gameObject.transform.GetChild(0).GetComponent<Animation>().Play("intro");
        //    }
        //    else
        //    {
        //        Debug.LogWarning("This object hasn't intro animation.");
        //    }
        //}

    }
}
