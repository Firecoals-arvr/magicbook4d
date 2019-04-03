using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firecoals.Augmentation;
using System;
using Firecoals.AssetBundles;
using Loxodon.Framework.Bundles;
using Loxodon.Framework.Contexts;
using Firecoals.AssetBundles.Sound;

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
        private  GameObject go;
        public float TimeStartEffect;
        private AssetHandler assethandler;
        private  AudioSource audio;
        private IResources _resources;
        public string tagInfo;
        protected override void Start()
        {
            
            base.Start();
            audio = gameObject.GetComponent<AudioSource>();
            assethandler = new AssetHandler(mTrackableBehaviour.transform);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

      IEnumerator starEffect(GameObject go)
        {
            Debug.Log("start Effect");
           RandomEffect.Instance.Onfound(TimeStartEffect);
           yield return new WaitForSeconds(0.5f);
           GameObject.Instantiate(go, mTrackableBehaviour.transform);
        }
        protected override void OnTrackingFound()
        {
            base.OnTrackingFound();
            var statTime = DateTime.Now;
            GameObject go = assethandler.CreateUnique(BuildName, path);    
            Debug.Log("load in: " + (DateTime.Now - statTime).Milliseconds);
            if (go != null)
            {
                var startTime = DateTime.Now;
                //Instantiate(go, mTrackableBehaviour.transform);
                StartCoroutine(starEffect(go));
              
                Debug.Log("instantiate in: " + (DateTime.Now - startTime).Milliseconds);
            }
        }       
        protected override void OnTrackingLost()
        {
            base.OnTrackingLost();
           // Debug.LogWarning(mTrackableBehaviour.TrackableName.ToString() + "xx");
                RandomEffect.Instance.Onlost();
            assethandler?.ClearAll();
            assethandler?.Content.ClearAll();      
            foreach (Transform go in mTrackableBehaviour.transform)
            {
               Destroy(go.gameObject);
            }
          
        }

        private void RunAnimationIntro()
        {
            if (this.gameObject.transform.GetChild(0).GetComponent<Animation>() != null)
            {
                this.gameObject.transform.GetChild(0).GetComponent<Animation>().Play("intro");
            }
            else
            {
                Debug.LogWarning("This object hasn't intro animation.");
            }
        }
      
    }
}
