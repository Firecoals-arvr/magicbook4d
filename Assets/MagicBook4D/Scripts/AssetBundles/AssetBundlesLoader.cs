using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Loxodon.Framework.Asynchronous;
using Loxodon.Framework.Bundles;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Firecoals.AssetBundles
{
    public class AssetBundlesLoader : MyBundleResources
    {

        public Dictionary<string, IBundle> bundles = new Dictionary<string, IBundle>();

        /// <summary>
        /// Load GameObject
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parent"></param>
        public void LoadAsset(string name, Transform parent )
        {
            var myResources = this.GetResources();
            IProgressResult<float, GameObject> result = myResources.LoadAssetAsync<GameObject>(name);
            result.Callbackable().OnCallback((r) =>
            {
                try
                {
                    if (r.Exception != null)
                        throw r.Exception;

                    GameObject.Instantiate(r.Result, parent);
                }
                catch (Exception e)
                {
                    Debug.LogErrorFormat("Load failure.Error:{0}", e);
                }
            });
        }
        /// <summary>
        /// Load An Object from asset bundle
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public UnityEngine.Object LoadAssetObject(string name)
        {
            var myResources = this.GetResources();
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
        /// Load all Objects of a assetbundle name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public UnityEngine.Object[] LoadAssetObjects(string name)
        {
            var myResources = this.GetResources();
            IProgressResult<float, UnityEngine.Object[]> result = myResources.LoadAllAssetsAsync<UnityEngine.Object>(name);
            UnityEngine.Object[] @object = null;
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
            var myResources = GetResources();
            IProgressResult<float, IBundle[]> result = myResources.LoadBundle(bundleNames, priority);
            result.Callbackable().OnProgressCallback(p =>
            {
                Debug.LogFormat("PreLoading {0:F1}%", (p*100).ToString(CultureInfo.InvariantCulture));
            });
            yield return result.WaitForDone();

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

        void OnDestroy()
        {
            if (bundles == null)
                return;

            foreach (IBundle bundle in bundles.Values)
                bundle.Dispose();

            this.bundles = null;
        }
    }
}

