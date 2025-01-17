﻿using Firecoals.Augmentation;
using Firecoals.MagicBook;
using Loxodon.Framework.Asynchronous;
using Loxodon.Framework.Bundles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Firecoals.AssetBundles
{
    public class AssetBundlesLoader
    {

        public Dictionary<string, IBundle> bundles = new Dictionary<string, IBundle>();
        public UISlider slider { get; set; }
        /// <summary>
        /// Load GameObject Asynchronous
        /// Return an GameObject
        /// Return null if fail
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parent"></param>
        public GameObject LoadAsset(string name, Transform parent)
        {
            //TODO Get Resource from AssetLoader DONE
            //var myResources = GetResources();
            var myResources = GameObject.FindObjectOfType<AssetLoader>().Resources;

            IProgressResult<float, GameObject> result = myResources.LoadAssetAsync<GameObject>(name);
            GameObject tempGameObject = null;
            result.Callbackable().OnCallback((r) =>
            {
                try
                {
                    if (r.Exception != null)
                        throw r.Exception;

                    tempGameObject = r.Result;
                }
                catch (Exception e)
                {
                    Debug.LogErrorFormat("Load failure.Error:{0}", e);
                }
            });
            return tempGameObject;
        }
        /// <summary>
        /// Load An Object from asset bundle
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public UnityEngine.Object LoadAssetObject(string name)
        {

            var myResources = GameObject.FindObjectOfType<AssetLoader>().Resources;// this.GetResources();
            IProgressResult<float, UnityEngine.Object> result = myResources.LoadAssetAsync<UnityEngine.Object>(name);
            UnityEngine.Object @object = null;

            result.Callbackable().OnCallback((r) =>
            {
                try
                {
                    if (r.Exception != null)
                        throw r.Exception;

                    @object = r.Result;

                }
                catch (Exception e)
                {
                    Debug.LogErrorFormat("Load failure.Error:{0}", e);
                }
            });
            return @object;
        }

        /// <summary>
        /// Preloaded AssetBundle.
        /// </summary>
        /// <param name="bundleNames"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        public IEnumerator Preload(string[] bundleNames, int priority)
        {
            Dispose();
            var assetLoader = GameObject.FindObjectOfType<AssetLoader>();
            var myResources = assetLoader.Resources;//= GetResources();
            IProgressResult<float, IBundle[]> result = myResources.LoadBundle(bundleNames, priority);
            
            result.Callbackable().OnProgressCallback(p =>
            {
                Debug.LogFormat("PreLoading {0:F1}%", (p * 100).ToString(CultureInfo.InvariantCulture));
                slider.value = p;
                slider.transform.GetChild(2).gameObject.GetComponent<UILabel>().text = "Vui lòng đợi";
            });
            yield return result.WaitForDone();
            if (result.IsDone)
            {
                Debug.Log("<color=red>" + ThemeController.instance.Theme + "</color>");
                    //Color or Animal
                SceneManager.LoadScene(ThemeController.instance.Theme, LoadSceneMode.Single);
 
            }
            if (result.Exception != null)
            {
                Debug.LogWarningFormat("Loads failure.Error:{0}", result.Exception);
                yield break;
            }

            foreach (IBundle bundle in result.Result)
            {
                bundles.Add(bundle.Name, bundle);
            }
        }

        public void OnDestroy()
        {
            if (bundles == null)
                return;

            foreach (IBundle bundle in bundles.Values)
                bundle.Dispose();

            bundles = null;

        }

        private void Dispose()
        {
            if (bundles == null || bundles.Count == 0)
                return;

            foreach (IBundle bundle in bundles.Values)
                bundle.Dispose();
            bundles = new Dictionary<string, IBundle>();
        }
    }
}

