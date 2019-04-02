using Firecoals.AssetBundles;
using Firecoals.Augmentation;
using Loxodon.Framework.Bundles;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DownLoadManager : MonoBehaviour
{
    public UISlider loadingBar;

    private DownLoadAssetBundles _dlAssets;
    private void Start()
    {
        var themeName = ThemeController.instance.Theme;
        NGUITools.SetActive(loadingBar.gameObject, false);
        _dlAssets = new DownLoadAssetBundles(themeName, themeName + "/bundles");
        InitDownload();
    }

    private void InitDownload()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable && !RequiredDownload())
        {
            PreLoad();
            //TODO Load Play Scene Additive, Disable Loading scene UI
        }
        else if (Application.internetReachability == NetworkReachability.NotReachable && RequiredDownload())
        {
            PopupManager.PopUpDialog("[[9C0002]Error[-]]", "Không có kết nối mạng, vui lòng thử lại", PopupManager.DialogType.YesNoDialog,
                () =>
                {
                    if (Application.internetReachability != NetworkReachability.NotReachable)
                    {
                        Download();
                    }

                    SceneManager.LoadScene("Menu");
                }, () => SceneManager.LoadScene("Menu"));
        }
        else if (Application.internetReachability != NetworkReachability.NotReachable && !RequiredDownload() && NeedUpdate())
        {
            PopupManager.PopUpDialog("", "Có bản cập nhật dữ liệu mới, bạn có muốn tải về không?", PopupManager.DialogType.YesNoDialog,
                () =>
                {
                    RetryDownload();
                }, () =>
            {
                PreLoad();
                //TODO Load Play Scene Additive, Disable Loading scene UI
            });
        }
        else if(Application.internetReachability != NetworkReachability.NotReachable && RequiredDownload())
        {
            Download();
        }
        else if(Application.internetReachability != NetworkReachability.NotReachable && !RequiredDownload())
        {
            PreLoad();
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

        PopupManager.PopUpDialog("Xin chào!", "Bạn cần tải dữ liệu để tiếp tục, bấm Đồng ý", PopupManager.DialogType.YesNoDialog,
        (() =>
        {
            _dlAssets.slider = loadingBar;
            NGUITools.SetActive(loadingBar.gameObject, true);
            StartCoroutine(_dlAssets.Download());
        }), () => SceneManager.LoadScene("Menu"));
    }
    /// <summary>
    /// If the download has been failed retry download
    /// </summary>
    public void RetryDownload()
    {
        //Clear persistent data path
        BundleUtil.ClearStorableDirectory();
        PlayerPrefs.DeleteKey("Downloaded" + ThemeController.instance.Theme);
        //Downloading
        _dlAssets.slider = loadingBar;
        _dlAssets.slider.value = 0;
        StartCoroutine(_dlAssets.Download());
    }

    /// <summary>
    /// Check if manifest version different application version
    /// </summary>
    public bool NeedUpdate()
    {
        IBundleManifestLoader manifestLoader = new BundleManifestLoader();
        BundleManifest localManifest = manifestLoader.Load(BundleUtil.GetStorableDirectory() + BundleSetting.ManifestFilename);
        return localManifest.Version != Application.version;
    }


    /// <summary>
    /// There is NO data in local
    /// TODO for test: Delete key "Downloaded+ThemeName" when delete data
    /// </summary>
    /// <returns></returns>
    public bool RequiredDownload()
    {
        //return !PlayerPrefs.GetString("Downloaded" + ThemeController.instance.Theme).Equals("DONE");
        return !PlayerPrefs.GetString("Downloaded" + ThemeController.instance.Theme).Equals("DONE");
    }

}
