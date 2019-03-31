using Firecoals.AssetBundles;
using UnityEngine;

public class DownLoadManager : MonoBehaviour
{
    public UISlider loadingBar;

    private DownLoadAssetBundles dlAssets;
    private void Start()
    {
        var themeName = ThemeController.instance.Theme;
        dlAssets = new DownLoadAssetBundles(themeName,themeName+"/bundles" );
        dlAssets.slider = loadingBar;
        Debug.LogWarning("You're in theme: " + ThemeController.instance.Theme);
        StartCoroutine(dlAssets.Download());
    }
}
