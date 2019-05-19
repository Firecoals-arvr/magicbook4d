using Firecoals.MagicBook;
using Loxodon.Framework.Asynchronous;
using Loxodon.Framework.Bundles;
using System;
using System.Collections;
using System.Collections.Generic;
using Firecoals.SceneTransition;
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
        _platformName = "IOS";
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
                //Download manifest
                IProgressResult<Progress, BundleManifest> manifestResult = _downloader.DownloadManifest(BundleSetting.ManifestFilename);

                yield return manifestResult.WaitForDone();
                if (manifestResult.Exception != null)
                {
                    Debug.LogFormat("Downloads BundleManifest failure.Error:{0}", manifestResult.Exception);
                    BundleUtil.ClearStorableDirectory();
                    PopupManager.PopUpDialog("[[9C0002]Error[-]]", "Downloads data info failure. Error:{0}" +
                        manifestResult.Exception, "Menu", default, default, PopupManager.DialogType.OkDialog,
                        (() => SceneLoader.LoadScene("Menu")));
                    yield break;
                }

                BundleManifest manifest = manifestResult.Result;

                IProgressResult<float, List<BundleInfo>> bundlesResult = null;
                if (ActiveManager.IsActiveOfflineOk(ActiveManager.NameToProjectID(ThemeController.instance.Theme)))
                {
                    bundlesResult = _downloader.GetDownloadList(manifest);
                }
                else
                {
                    bundlesResult = _downloader.GetDownloadList(manifest, GetFreeBundleNames());
                }
                yield return bundlesResult.WaitForDone();

                List<BundleInfo> bundles = bundlesResult.Result;

                if (bundles == null || bundles.Count <= 0)
                {
                    Debug.LogFormat("Please clear cache and remove StreamingAssets,try again.");
                    PopupManager.PopUpDialog("[[9C0002]Error[-]]", "You need to clear cache to download the data, press OK to continue", default, "OK", "Hủy bỏ",
                        PopupManager.DialogType.YesNoDialog, () =>
                        {
                            var dlManager = GameObject.FindObjectOfType<DownLoadManager>();
                            dlManager.ClearCacheAndRetry();
                        }, () => SceneLoader.LoadScene("Menu"));
                    yield break;
                }

                IProgressResult<Progress, bool> downloadResult = _downloader.DownloadBundles(bundles);

                downloadResult.Callbackable().OnProgressCallback(p =>
                {

                    Debug.LogFormat("Downloading {0:F2}KB/{1:F2}KB {2:F3}KB/S", p.GetCompletedSize(UNIT.KB), p.GetTotalSize(UNIT.KB), p.GetSpeed(UNIT.KB));
                    var percent = p.GetCompletedSize(UNIT.KB) / p.GetTotalSize(UNIT.KB);

                    slider.value = percent;
                    slider.transform.GetChild(2).gameObject.GetComponent<UILabel>().text = "Các bé dưới 5 tuổi khi sử dụng ứng dụng cần có sự giám sát của phụ huynh";
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
                    BundleUtil.ClearStorableDirectory();
                    PopupManager.PopUpDialog("[[9C0002]Error[-]]", "Downloads Data failure. Error" + downloadResult.Exception, default, "Thử lại", "Menu",
                        PopupManager.DialogType.YesNoDialog, () =>
                        {
                            var dlManager = GameObject.FindObjectOfType<DownLoadManager>();
                            dlManager.ClearCacheAndRetry();
                        }, () => SceneLoader.LoadScene("Menu"));
                    yield break;
                }

                if (downloadResult.IsDone)
                {
                    //PlayerPrefs.SetString("Downloaded" + ThemeController.instance.Theme, "DONE");
                    //Debug.LogWarning("DONE download");
                    var dlManager = GameObject.FindObjectOfType<DownLoadManager>();
                    dlManager.PreLoad();
                }

                if (downloadResult.IsCancelled)
                {

                    //BundleUtil.ClearStorableDirectory();
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
        /// <summary>
        /// Get bundle names to be downloaded
        /// In Firecoals policy
        /// animals free: lion, elephant, gorilla
        /// space free: solar system, sun, mercury
        /// color free plane
        /// </summary>
        /// <returns></returns>
        public static string[] GetFreeBundleNames()
        {
            string[] bundleNames = null;
            switch (ThemeController.instance.Theme)
            {
                case "Animal":
<<<<<<< HEAD
                    bundleNames = new[] { "animals/model/lion" ,
                        "animals/model/elephant" ,
=======
                    bundleNames = new[] { "animals/model/lion",
                        "animals/model/elephant",
>>>>>>> 518a457d51d359045dde1809288953a492d17fba
                        "animals/model/gorilla",
                        "animals/info/en",
                        "animals/info/jp",
                        "animals/info/vn",
                        "animals/name/cn",
                        "animals/name/en",
                        "animals/name/vn",
                        "animals/name/jp",
                        "animals/noise" };
                    break;
                case "Space":
                    bundleNames = new[] { "space/models/solarsystem",
<<<<<<< HEAD
                            "space/models/sun",
                            "space/models/mercury",
                            "space/sound/name/en",
                            "space/sound/name/vn",
                            "space/sound/info/vn",
                            "space/sound/info/en",
                            "space/music" };
=======
                        "space/models/sun",
                        "space/models/mercury",
                        "space/sound/name/en",
                        "space/sound/name/vn",
                        "space/sound/info/vn",
                        "space/sound/info/en",
                        "space/music" };
>>>>>>> 518a457d51d359045dde1809288953a492d17fba
                    break;
                case "Color":
                    bundleNames = new[] { "color/model/maybay", "color/sounds/sounds" };
                    break;
                default:
                    bundleNames = null;
                    break;
            }

            if (bundleNames != null)
            {
                return bundleNames;
            }
            else
            {
                Debug.LogError("Fail to get free bundle names to download");
                return null;
            }

        }
        #endregion

    }
}

