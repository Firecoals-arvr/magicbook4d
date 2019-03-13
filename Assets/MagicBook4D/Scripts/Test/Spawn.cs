using UnityEngine;
using Firecoals.AssetBundles;
using System.IO;
using System;
using Loxodon.Framework.Bundles;

public class Spawn : MonoBehaviour
{
    private DownLoadAssetBundles downLoad;


    void Start()
    {
        BundleSetting bundleSetting = new BundleSetting("Animal/bundles");
        downLoad = new DownLoadAssetBundles(DownLoadAssetBundles.GetDataUrl("Animal"));
    }

    public void Download()
    {
        StartCoroutine(downLoad.Download());
    }

    public void Create()
    {
        //var stopwatch = new Stopwatch();
        //stopwatch.Start();

        //downLoad.LoadAsset("Animal/GetPreFab/Elephant.prefab");
        //downLoad.LoadAsset("Animal/GetPreFab/Tiger.prefab");
        //downLoad.LoadAsset("Animal/GetPreFab/Cat.prefab");

        //stopwatch.Stop();

        //UnityEngine.Debug.Log("MeasureByEnvironmentTickCount: " + stopwatch.ElapsedMilliseconds);

        var startTime = DateTime.Now;
        downLoad.LoadAsset("Animal/GetPreFab/Elephant.prefab");
        downLoad.LoadAsset("Animal/GetPreFab/Tiger.prefab");
        downLoad.LoadAsset("Animal/GetPreFab/Cat.prefab");
        UnityEngine.Debug.Log("MeasureByDateTime: " + (DateTime.Now - startTime).Milliseconds);
    }
    /// <summary>
    /// Clear persistent data path of a book
    /// </summary>
    /// <param name="bookName"></param>
    public void CleanCache(string bookName)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(Application.persistentDataPath + "/" + bookName + "/bundles");
        directoryInfo.Delete();
    }
}
