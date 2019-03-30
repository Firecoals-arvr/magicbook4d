using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firecoals.AssetBundles;
using Loxodon.Framework.Bundles;
using Vuforia;
namespace Firecoals.Augmentation
{
    /// <summary>
    /// 
    /// </summary>
    public class AssetHandler
    {
        #region PUBLIC_VARIABLE
        public static AssetBundlesLoader assetBundlesLoader;
        public Dictionary<string, GameObject> spawnedObject;
        #endregion
        public TargetContent Content { get; }
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

                    Content.Create(goTemplate, TargetContent.ContentType.Unique);
                    spawnedObject.Add(bundlePath, goTemplate);
                    Debug.LogWarning("<color=green>unique object created</color>");
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
            if (assetBundlesLoader.bundles.ContainsKey(bundleName))
            {
                var bundle = assetBundlesLoader.bundles[bundleName];
                var goTemplate = bundle.LoadAsset<GameObject>(bundlePath);
                var clone = Content.Create(goTemplate, TargetContent.ContentType.Clone);
                spawnedObject.Add(bundlePath, goTemplate);
                Debug.LogWarning("<color=green>clone object created</color>");
                return goTemplate;
            }
            else
            {
                Debug.LogError("Can not create null game object, Please wait for load asset bundle is done!");
                return null;
            }
        }
        /// <summary>
        /// Create random a GameObject in a list bundle paths as clone
        /// </summary>
        /// <param name="bundleName"></param>
        /// <param name="bundlePaths"></param>
        /// <returns></returns>
        public GameObject CreateRandom(string bundleName, string[] bundlePaths)
        {
            var bundlePath = bundlePaths[new System.Random().Next(0, bundlePaths.Length)];
            if (assetBundlesLoader.bundles.ContainsKey(bundleName))
            {
                var bundle = assetBundlesLoader.bundles[bundleName];
                var goTemplate = bundle.LoadAsset<GameObject>(bundlePath);
                var clone = Content.Create(goTemplate, TargetContent.ContentType.Clone);
                //spawnedObject.Add(bundlePath, goTemplate);
                Debug.LogWarning("<color=green>clone random object created</color>");
                return goTemplate;
            }
            else
            {
                Debug.LogError("Can not create null game object, Please wait for load asset bundle is done!");
                return null;
            }
        }
        public void ClearAll()
        {
            spawnedObject?.Clear();
            
        }
        #endregion
    }

}