using UnityEngine;
using Firecoals.AssetBundles;
using System.IO;
using System;
using Loxodon.Framework.Bundles;
using System.Collections;
using Loxodon.Framework.Contexts;
using Loxodon.Framework.Asynchronous;
using System.Collections.Generic;
using Firecoals.AssetBundles.Sound;

public class Spawn : MonoBehaviour
{
    private DownLoadAssetBundles downLoad;
    private AudioSource audioSource;
    IEnumerator Start()
    {
        downLoad = new DownLoadAssetBundles("Animal", "Animal/bundles");
        audioSource = GetComponent<AudioSource>();
        AssetBundlesLoader assetBundles = new AssetBundlesLoader();

        /* Preload AssetBundle */
        yield return assetBundles.Preload(new string[] { "animals/model/dog", "animals/model/cat", "animals/model/tiger", "animals/model/giraffe", "animals/model/rabbit", "animals/noise" }, 1);
        var startTime = DateTime.Now;
        ///* Use IBundle,loads plane */
        IBundle bundle = assetBundles.bundles["animals/model/tiger"];
        GameObject goTemplate = bundle.LoadAsset<GameObject>("Animal/GetPreFab/Tiger.prefab"); //OK
        GameObject.Instantiate(goTemplate);
        IBundle bundleAudioClip = assetBundles.bundles["animals/noise"];

        ISoundManifestLoader soundManifestLoader = new SoundManifestLoader();
        var soundManifest = soundManifestLoader.LoadSync(Application.streamingAssetsPath + "/AnimalAudioClip.json");
        var myBundlePath = soundManifest.soundInfos[5].PathBundle;
        AudioClip audioClip = bundleAudioClip.LoadAsset<AudioClip>(soundManifest.soundInfos[5].PathBundle);
        audioSource.clip = audioClip;
        audioSource.Play();
        

        ///* Green and Red */
        //GameObject[] goTemplates = assetBundles.FindResource().LoadAssets<GameObject>("Animal/GetPreFab/Frog.prefab", "Assets/Animal/GetPreFab/Rabbit.prefab");
        //foreach (GameObject template in goTemplates)
        //{
        //    GameObject.Instantiate(template);
        //}
        UnityEngine.Debug.Log("MeasureByDateTime: " + (DateTime.Now - startTime).Milliseconds);


        //Debug.LogError(soundManifest.soundInfos[0].PathBundle);

        UnityEngine.Debug.Log("MeasureByDateTime: " + (DateTime.Now - startTime).Milliseconds);

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
