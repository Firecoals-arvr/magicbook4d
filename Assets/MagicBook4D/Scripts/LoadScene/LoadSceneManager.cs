using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneManager : MonoBehaviour
{
    public void LoadPreLoadScene()
    {
        LoadingScreen.SceneToLoad = "PreloadAssetBundleExample";
        LoadingScreen.Run();
    }

    public void LoadTestScene()
    {
        LoadingScreen.SceneToLoad = "TestScene";
        LoadingScreen.Run(); 
    }

    public void LoadMenuScene()
    {
        LoadingScreen.SceneToLoad = "Menu";
        LoadingScreen.Run();
    }
   
    public void LoadResourcesDownloadScene()
    {
        LoadingScreen.SceneToLoad = "ResourcesDownloadExample";
        LoadingScreen.Run();
    }
}
