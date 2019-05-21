using Firecoals.AssetBundles;
using Firecoals.Augmentation;
using Firecoals.MagicBook;
using Firecoals.SceneTransition;
using Loxodon.Framework.Bundles;
using System.IO;
using UnityEngine;
using Version = System.Version;

/// <summary>
/// Manage download asset bundles
/// </summary>
/// <remarks></remarks>
public class DownLoadManager : MonoBehaviour
{
    public UISlider loadingBar;

    private DownLoadAssetBundles _dlAssets;
    private BundleManifest _localManifest;
    private void Start()
    {
        var themeName = ThemeController.instance.Theme;
        //NGUITools.SetActive(loadingBar.gameObject, false);
        _dlAssets = new DownLoadAssetBundles(themeName, themeName + "/bundles");

        LoadLocalManifest();
        InitDownload();
    }

    private void InitDownload()
    {
        Debug.Log("<color=green>required download is " + RequiredDownload() + "</color>");
        // if don't have connection and not required download
        if (Application.internetReachability == NetworkReachability.NotReachable && !RequiredDownload())
        {
            //if activated
            if (ActiveManager.IsActiveOfflineOk(ActiveManager.NameToProjectID(ThemeController.instance.Theme)))
            {
                PreLoad();
            }
            else
            {
                PopupManager.PopUpDialog("Xin chào", "Bạn đang ở chế độ dùng thử, kích hoạt để trải nghiệm hết các tính năng",
                    "Ok", "Kích hoạt", "Dùng thử", PopupManager.DialogType.YesNoDialog,
                    () => SceneLoader.LoadScene("Activate"), PreLoad
                    );
            }
        }
        //if don't have a connection and required download
        else if (Application.internetReachability == NetworkReachability.NotReachable && RequiredDownload())
        {
            PopupManager.PopUpDialog("[[9C0002]Error[-]]", "Không có kết nối mạng, vui lòng thử lại", "Ok", "Thử lại", "Menu", PopupManager.DialogType.YesNoDialog,
                () =>
                {
                    if (Application.internetReachability != NetworkReachability.NotReachable)
                    {
                        Download();
                    }

                    SceneLoader.LoadScene("Menu");
                }, () => SceneLoader.LoadScene("Menu"));
        }
        //if have a connection and don't require download and there is a update data
        else if (Application.internetReachability != NetworkReachability.NotReachable && !RequiredDownload() && NeedUpdate())
        {
            PopupManager.PopUpDialog("", "Có bản cập nhật dữ liệu mới, bạn có muốn tải về không?", default, "Đồng ý", "Hủy bỏ", PopupManager.DialogType.YesNoDialog,
                () =>
                {
                    ClearCacheAndRetry();
                }, () =>
                {
                    PreLoad();
                });
        }
        else if (Application.internetReachability != NetworkReachability.NotReachable && RequiredDownload())
        {
            if (ActiveManager.IsActiveOfflineOk(ActiveManager.NameToProjectID(ThemeController.instance.Theme)))
            {
                Download();
            }
            else
            {
                PopupManager.PopUpDialog("Xin chào!", "Bạn đang ở chế độ dùng thử, kích hoạt để sử dụng hết tính năng của sản phẩm",
                    "OK", "Kích hoạt", "Dùng thử", PopupManager.DialogType.YesNoDialog, () =>
                            SceneLoader.LoadScene("Activate"), Download);
            }

        }
        else if (Application.internetReachability != NetworkReachability.NotReachable && !RequiredDownload())
        {
            if (ActiveManager.IsActiveOfflineOk(ActiveManager.NameToProjectID(ThemeController.instance.Theme)))
            {
                PreLoad();
            }
            else
            {
                PopupManager.PopUpDialog("Xin chào!", "Bạn đang ở chế độ dùng thử, kích hoạt để sử dụng hết tính năng của sản phẩm",
                    "OK", "Kích hoạt", "Dùng thử", PopupManager.DialogType.YesNoDialog, () =>
                        SceneLoader.LoadScene("Activate"), PreLoad);
            }
        }
    }

    public void PreLoad()
    {
        var assetLoader = GameObject.FindObjectOfType<AssetLoader>();
        assetLoader.InitResource();
        StartCoroutine(assetLoader.PreLoad(loadingBar));

    }
    //TODO If has Internet connection DONE
    //If has a update version PopUp Do you want to update new version data
    //FIRST popup: The app need to download data, do you want to download it? if No load Menu scene, if Yes, download the data
    //TODO If no Internet connection
    /*Check if Cache Ok => Load Scene
     *Check if Cache Not Ok => PopUp Need an internet connection to download data, Please try a gain
     */

    /*
     TODO Case The download is not completed
      + Clear Cache
      + Download again
      TODO In the future IF DOWNLOAD IS NOT COMPLETED
      + Download missing file
     
     */
    /// <summary>
    /// Start download asset bundle
    /// </summary>
    public void Download()
    {
        //PopupManager.PopUpDialog("Xin chào!", "Bạn cần tải dữ liệu để tiếp tục, bấm Đồng ý", default, "Đồng ý", "Hủy bỏ", PopupManager.DialogType.YesNoDialog,
        //(() =>
        //{
        _dlAssets.slider = loadingBar;
        NGUITools.SetActive(loadingBar.gameObject, true);
        StartCoroutine(_dlAssets.Download());
        //}), () => SceneLoader.LoadScene("Menu"));
    }
    /// <summary>
    /// If the download has been failed retry download
    /// </summary>
    public void RetryDownload()
    {
        _dlAssets.slider = loadingBar;
        _dlAssets.slider.value = 0;
        NGUITools.SetActive(loadingBar.gameObject, true);
        StartCoroutine(_dlAssets.Download());
    }

    public void ClearCacheAndRetry()
    {
        //Clear persistent data path
        BundleUtil.ClearStorableDirectory();
        PlayerPrefs.DeleteKey("Downloaded" + ThemeController.instance.Theme);
        //Downloading
        _dlAssets.slider = loadingBar;
        _dlAssets.slider.value = 0;
        NGUITools.SetActive(loadingBar.gameObject, true);
        StartCoroutine(_dlAssets.Download());
    }

    /// <summary>
    /// Check if manifest version different application version
    /// </summary>
    public bool NeedUpdate()
    {
        //IBundleManifestLoader manifestLoader = new BundleManifestLoader();
        //_localManifest = manifestLoader.Load(BundleUtil.GetStorableDirectory() + BundleSetting.ManifestFilename);
        if (!ActiveManager.cloudBundleVersion.IsNullOrEmpty())
        {
            var localVersion = new Version(_localManifest.Version);
            var cloudVersion = new Version(ActiveManager.cloudBundleVersion);
            var result = cloudVersion.CompareTo(localVersion);
            return result > 0;
        }
        return false;
    }

    /// <summary>
    /// if the product is activated. Must have all data file.
    /// else only need the free file
    /// </summary>
    /// <returns></returns>
    public bool RequiredDownload()
    {
        //return !BundleUtil.ExistsInStorableDirectory(BundleUtil.GetStorableDirectory() + BundleSetting.ManifestFilename);
        if (_localManifest != null)
        {
            var freeBundleInfos = _localManifest.GetBundleInfos(DownLoadAssetBundles.GetFreeBundleNames());
            var fullBundleInfos = _localManifest.GetAll();
            if (ActiveManager.IsActiveOfflineOk(ActiveManager.NameToProjectID(ThemeController.instance.Theme)))
            {
                if (!BundleUtil.ExistsInStorableDirectory(fullBundleInfos))
                {
                    return true;
                }
                //return !BundleUtil.ExistsInStorableDirectory(BundleUtil.GetStorableDirectory() + BundleSetting.ManifestFilename);
            }
            else
            {
                if (!BundleUtil.ExistsInStorableDirectory(freeBundleInfos))
                {
                    return true;
                }
            }
            return false;
        }
        else
        {
            return true;
        }
    }
    /// <summary>
    /// load local manifest
    /// </summary>
    public void LoadLocalManifest()
    {
        IBundleManifestLoader manifestLoader = new BundleManifestLoader();
        if (File.Exists(BundleUtil.GetStorableDirectory() + BundleSetting.ManifestFilename))
            _localManifest = manifestLoader.Load(BundleUtil.GetStorableDirectory() + BundleSetting.ManifestFilename);
    }

}
