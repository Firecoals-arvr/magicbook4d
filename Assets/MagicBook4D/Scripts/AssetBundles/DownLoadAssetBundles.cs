using Loxodon.Framework.Asynchronous;
using Loxodon.Framework.Bundles;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Firecoals.AssetBundles
{
    public class DownLoadAssetBundles : MyBundleResources
    {
        private IDownloader _downloader;
        public bool downloading { get; private set; }
        private static string _platformName;
        private Dictionary<string, IBundle> bundles = new Dictionary<string, IBundle>();

        public UISlider slider { get; set; }
        //private string bundleUrl;
        //private void Start()
        //{
        //    Uri baseUri = new Uri(bundleUrl);
        //    this._downloader = new WWWDownloader(baseUri, true);
        //}
        #region DownLoadAssetBundle
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bookName">link from amazon s3, for example: https://s3-ap-southeast-1.amazonaws.com/magicbook4d/Android/Animal/bundles/ Animal is book name </param>
        /// <param name="bundleRoot"></param>
        public DownLoadAssetBundles(string bookName, string bundleRoot)
        {
            BundleSetting bundleSetting = new BundleSetting(bundleRoot);
            Uri baseUri = new Uri(GetDataUrl(bookName));
            _downloader = new WWWDownloader(baseUri, false);
        }

        /// <summary>
        /// Return the S3 Amazon Storage Url 
        /// </summary>
        /// <param name="bookName">Animal, Space, Color</param>
        /// <returns></returns>
        private string GetDataUrl(string bookName)
        {
#if UNITY_ANDROID
            _platformName = "Android";
#endif
#if UNITY_IOS
        platformName = "IOS";
#endif
            var currentVersion = Application.version;
            var url = "https://s3-ap-southeast-1.amazonaws.com/magicbook4d/" + currentVersion + "/" + _platformName + "/" + bookName + "/bundles/";
            return url;
        }
        public IEnumerator Download()
        {
            downloading = true;
            try
            {
                IProgressResult<Progress, BundleManifest> manifestResult = _downloader.DownloadManifest(BundleSetting.ManifestFilename);

                yield return manifestResult.WaitForDone();

                if (manifestResult.Exception != null)
                {
                    Debug.LogFormat("Downloads BundleManifest failure.Error:{0}", manifestResult.Exception);
                    PopupManager.PopUpDialog("[[9C0002]Error[-]]", "Downloads data info failure. Error:{0}" +
                        manifestResult.Exception, PopupManager.DialogType.OkDialog,
                        (() => SceneManager.LoadScene("Menu")));
                    yield break;
                }

                BundleManifest manifest = manifestResult.Result;
                /*TODO if manifest BundleInfo in the cloud != manifest BundleInfo in the local
                  Clear Cache and and download again
                  else if Storable directory if empty download
                  else preload and load the scene
                 */

                IProgressResult<float, List<BundleInfo>> bundlesResult = _downloader.GetDownloadList(manifest);
                yield return bundlesResult.WaitForDone();

                List<BundleInfo> bundles = bundlesResult.Result;

                if (bundles == null || bundles.Count <= 0)
                {
                    Debug.LogFormat("Please clear cache and remove StreamingAssets,try again.");
                    PopupManager.PopUpDialog("[[9C0002]Error[-]]", "You need to clear cache to download the data, press OK to continue",
                        PopupManager.DialogType.YesNoDialog, () =>
                        {
                            var dlManager = GameObject.FindObjectOfType<DownLoadManager>();
                            dlManager.RetryDownload();
                        }, () => SceneManager.LoadScene("Menu"));
                    yield break;
                }

                IProgressResult<Progress, bool> downloadResult = _downloader.DownloadBundles(bundles);
                downloadResult.Callbackable().OnProgressCallback(p =>
                {
                    Debug.LogFormat("Downloading {0:F2}KB/{1:F2}KB {2:F3}KB/S", p.GetCompletedSize(UNIT.KB), p.GetTotalSize(UNIT.KB), p.GetSpeed(UNIT.KB));
                    var percent = p.GetCompletedSize(UNIT.KB) / p.GetTotalSize(UNIT.KB);
                    slider.value = percent;
                    //var label =  slider.transform.Find("displaytext").gameObject.GetComponent<UILabel>();
                    //label.text = p.GetCompletedSize(UNIT.KB).ToString()+"/"+ p.GetTotalSize(UNIT.KB).ToString();

                    //if (Math.Abs(percent - 1) <= 0)
                    //{
                    //    PlayerPrefs.SetString("Downloaded" + ThemeController.instance.Theme, "DONE");
                    //}
                });

                yield return downloadResult.WaitForDone();

                if (downloadResult.Exception != null)
                {
                    Debug.LogFormat("Downloads AssetBundle failure.Error:{0}", downloadResult.Exception);
                    PopupManager.PopUpDialog("[[9C0002]Error[-]]", "Downloads Data failure. Error",
                        PopupManager.DialogType.YesNoDialog, () =>
                        {
                            var dlManager = GameObject.FindObjectOfType<DownLoadManager>();
                            dlManager.RetryDownload();
                        }, () => SceneManager.LoadScene("Menu"));
                    yield break;
                }

                if (downloadResult.IsDone)
                {
                    PlayerPrefs.SetString("Downloaded" + ThemeController.instance.Theme, "DONE");
                    Debug.LogWarning("DONE download");
                    var dlManager = GameObject.FindObjectOfType<DownLoadManager>();
                    dlManager.PreLoad();
                }

                if (downloadResult.IsCancelled)
                {
                    
                    BundleUtil.ClearStorableDirectory();
                    PlayerPrefs.DeleteKey("Downloaded" + ThemeController.instance.Theme);
                    Debug.LogWarning("Cancelled download");
                }

                if (resources != null)
                {
                    //update BundleManager's manifest
                    BundleManager manager = (resources as BundleResources).BundleManager as BundleManager;
                    manager.BundleManifest = manifest;
                }

#if UNITY_EDITOR
                UnityEditor.EditorUtility.OpenWithDefaultApp(BundleUtil.GetStorableDirectory());
#endif

            }
            finally
            {
                downloading = false;
            }
        }
        #endregion
        //TODO Check if current app version != bundle manifest version

        //public IEnumerator DownloadBundleManifest(Action<bool> needUpdate)
        //{
        //    IProgressResult<Progress, BundleManifest> manifestResult = _downloader.DownloadManifest(BundleSetting.ManifestFilename);

        //    yield return manifestResult.WaitForDone();

        //    if (manifestResult.Exception != null)
        //    {
        //        Debug.LogFormat("Downloads BundleManifest failure.Error:{0}", manifestResult.Exception);
        //        PopupManager.PopUpDialog("[[9C0002]Error[-]]", "Downloads BundleManifest failure. Error:{0}" + manifestResult.Exception);
        //        yield break;
        //    }
        //    Debug.LogWarning(manifestResult.Result.ToString());
        //    bool isNeedUpdate=  manifestResult.Result.GetAll().Any(bundleInfo => !BundleUtil.ExistsInStorableDirectory(bundleInfo));
        //    needUpdate(isNeedUpdate);
        //    yield return null;
        //}
    }
}

