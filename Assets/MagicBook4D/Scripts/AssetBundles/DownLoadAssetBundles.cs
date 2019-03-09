using Loxodon.Framework.Asynchronous;
using Loxodon.Framework.Bundles;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Firecoals.AssetBundles
{
    public class DownLoadAssetBundles
    {
        private IResources resources;
        private IDownloader downloader;
        private bool downloading = false;
        private static string platformName;
        //private string bundleUrl;
        //private void Start()
        //{
        //    Uri baseUri = new Uri(bundleUrl);
        //    this.downloader = new WWWDownloader(baseUri, true);
        //}
        public DownLoadAssetBundles(string bundleUrl)
        {
            Uri baseUri = new Uri(bundleUrl);//"https://s3-ap-southeast-1.amazonaws.com/magicbook4d/Android/Animal/bundles/"
            this.downloader = new WWWDownloader(baseUri, false);
        }

        public IEnumerator Download()
        {
            this.downloading = true;
            try
            {
                IProgressResult<Progress, BundleManifest> manifestResult = this.downloader.DownloadManifest(BundleSetting.ManifestFilename);

                yield return manifestResult.WaitForDone();

                if (manifestResult.Exception != null)
                {
                    Debug.LogFormat("Downloads BundleManifest failure.Error:{0}", manifestResult.Exception);
                    yield break;
                }

                BundleManifest manifest = manifestResult.Result;

                IProgressResult<float, List<BundleInfo>> bundlesResult = this.downloader.GetDownloadList(manifest);
                yield return bundlesResult.WaitForDone();

                List<BundleInfo> bundles = bundlesResult.Result;

                if (bundles == null || bundles.Count <= 0)
                {
                    Debug.LogFormat("Please clear cache and remove StreamingAssets,try again.");
                    yield break;
                }

                IProgressResult<Progress, bool> downloadResult = this.downloader.DownloadBundles(bundles);
                downloadResult.Callbackable().OnProgressCallback(p =>
                {
                    Debug.LogFormat("Downloading {0:F2}KB/{1:F2}KB {2:F3}KB/S", p.GetCompletedSize(UNIT.KB), p.GetTotalSize(UNIT.KB), p.GetSpeed(UNIT.KB));
                    
                });

                yield return downloadResult.WaitForDone();

                if (downloadResult.Exception != null)
                {
                    Debug.LogFormat("Downloads AssetBundle failure.Error:{0}", downloadResult.Exception);
                    yield break;
                }

                Debug.Log("OK");

                if (this.resources != null)
                {
                    //update BundleManager's manifest
                    BundleManager manager = (this.resources as BundleResources).BundleManager as BundleManager;
                    manager.BundleManifest = manifest;
                }

#if UNITY_EDITOR
                UnityEditor.EditorUtility.OpenWithDefaultApp(BundleUtil.GetStorableDirectory());
#endif

            }
            finally
            {
                this.downloading = false;
            }
        }

        IResources GetResources()
        {
            if (this.resources != null)
                return this.resources;

            /* Create a BundleManifestLoader. */
            IBundleManifestLoader manifestLoader = new BundleManifestLoader();

            /* Loads BundleManifest. */
            BundleManifest manifest = manifestLoader.Load(BundleUtil.GetStorableDirectory() + BundleSetting.ManifestFilename);

            //manifest.ActiveVariants = new string[] { "", "sd" };
            //manifest.ActiveVariants = new string[] { "", "hd" };

            /* Create a PathInfoParser. */
            IPathInfoParser pathInfoParser = new AutoMappingPathInfoParser(manifest);

            /* Use a custom BundleLoaderBuilder */
            ILoaderBuilder builder = new CustomBundleLoaderBuilder(new Uri(BundleUtil.GetReadOnlyDirectory()), false);

            /* Create a BundleManager */
            IBundleManager manager = new BundleManager(manifest, builder);

            /* Create a BundleResources */
            this.resources = new BundleResources(pathInfoParser, manager);
            return this.resources;
        }

        public void LoadAsset(string name)
        {
            var resources = this.GetResources();
            IProgressResult<float, GameObject> result = resources.LoadAssetAsync<GameObject>(name);
            result.Callbackable().OnCallback((r) =>
            {
                try
                {
                    if (r.Exception != null)
                        throw r.Exception;

                    GameObject.Instantiate(r.Result, new Vector3(Random.Range(-1,1), Random.Range(-1, 1)), Quaternion.identity);

                }
                catch (Exception e)
                {
                    Debug.LogErrorFormat("Load failure.Error:{0}", e);
                }
            });
        }

        /// <summary>
        /// Return the S3 Amazon Storage Url 
        /// </summary>
        /// <param name="bookName">Animal, Space, Color</param>
        /// <returns></returns>
        public static string GetDataUrl(string bookName)
        {
#if UNITY_ANDROID
            platformName = "Android";
#endif
#if UNITY_IOS
        platformName = "IOS";
#endif
            var currentVersion = Application.version;
            var url = "https://s3-ap-southeast-1.amazonaws.com/magicbook4d/" + currentVersion + "/" + platformName + "/" + bookName + "/bundles/";
            return url;
        }
    }
}

