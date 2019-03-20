﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firecoals.AssetBundles;
using Loxodon.Framework.Bundles;
using Vuforia;
namespace Firecoals.Augmentation
{
    /// <summary>
    /// Đây là tiếng việt có dấu ễ ố ồ í
    /// </summary>
    public class AssetHandler
    {
        #region PRIVATE_VARIABLE

        //private TargetContent content;
        private Dictionary<string, GameObject> spawnedObject;
        #endregion
        #region PUBLIC_VARIABLE
        public static AssetBundlesLoader assetBundlesLoader;

        #endregion
        public TargetContent Content { get; }
        #region PUBLIC_VARIABLE

        #endregion
        #region PUBLIC_CONSTRUCTOR
        /// <summary>
        /// Asset contents under a image target
        /// </summary>
        /// <param name="parent"></param>
        public AssetHandler(Transform parent)
        {
            Content = new TargetContent(parent);
            spawnedObject = new Dictionary<string, GameObject>();
        }

        public AssetHandler() { }

        #endregion
        #region PRIVATE_METHOD
        public static IEnumerator PreLoad(string bundleRoot, string[] bundleNames)
        {
            BundleSetting bundleSettings = new BundleSetting(bundleRoot);
            assetBundlesLoader = new AssetBundlesLoader();
            /*Preload asset bundle*/

            yield return assetBundlesLoader.Preload(bundleNames, 1);
        }

        #endregion
        #region PUBLIC_METHOD

        /// <summary>
        /// Create an unique game object which is child of the target by name
        /// </summary>
        /// <param name="bundleName">name of the bundle which was </param>
        /// <param name="bundlePath"></param>
        public GameObject CreateUnique(string bundleName, string bundlePath)
        {
            Debug.LogWarning("prepare Create unique game object");
            GameObject goTemplate;
            if (assetBundlesLoader.bundles.ContainsKey(bundleName))
            {
                IBundle bundle = assetBundlesLoader.bundles[bundleName];
                goTemplate = bundle.LoadAsset<GameObject>(bundlePath);
                if (!spawnedObject.ContainsKey(bundlePath))
                {
                    //var clone = Instantiate(goTemplate, content.parent) as GameObject;

                    Content.Create(goTemplate, TargetContent.ContentType.UNIQUE);
                    spawnedObject.Add(bundlePath, goTemplate);
                    Debug.LogWarning("unique object created");
                    return goTemplate;
                }
                else
                {
                    goTemplate = null;
                    return null;
                }
            }
            else
            {
                Debug.LogError("Can not create null game object, Please wait for load asset bundle is done!");
                return null;
            }

        }
        /// <summary>
        /// You can create many game object from the game object
        /// </summary>
        /// <param name="bundleName"></param>
        /// <param name="bundlePath"></param>
        /// <returns></returns>
        public GameObject CreateClone(string bundleName, string bundlePath)
        {
            Debug.LogWarning("prepare Create unique game object");
            GameObject goTemplate;
            if (assetBundlesLoader.bundles.ContainsKey(bundleName))
            {
                IBundle bundle = assetBundlesLoader.bundles[bundleName];
                goTemplate = bundle.LoadAsset<GameObject>(bundlePath);
                var clone = Content.Create(goTemplate, TargetContent.ContentType.CLONE);
                spawnedObject.Add(bundlePath, goTemplate);
                Debug.LogWarning("unique object created");
                return goTemplate;
            }
            else
            {
                Debug.LogError("Can not create null game object, Please wait for load asset bundle is done!");
                return null;
            }
        }

        #endregion
    }

}