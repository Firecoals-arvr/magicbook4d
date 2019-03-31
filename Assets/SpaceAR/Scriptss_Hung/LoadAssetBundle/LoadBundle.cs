using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firecoals.Augmentation;
using Firecoals.AssetBundles.Sound;
using Firecoals.AssetBundles;
using Loxodon.Framework.Bundles;

namespace Firecoals.Space
{
    public class LoadBundle : MonoBehaviour
    {
        public string bundleRoot;
        public string[] bundleNames;
        private void Awake()
        {
            //StartCoroutine(AssetHandler.PreLoad(bundleRoot, bundleNames));
        }

        //IEnumerator Start()
        //{
        //    AssetBundlesLoader assetLoader = new AssetBundlesLoader();
        //    yield return assetLoader.Preload(bundleNames, 1);

        //    IBundle audioBundle = assetLoader.bundles["space/sound/name/en"];
        //    ISoundManifestLoader soundLoader = new SoundManifestLoader();
        //    var soundManifest = soundLoader.LoadSync(Application.streamingAssetsPath + "/MagicAudioSpace.json");
        //    var bundlePath = soundManifest.soundInfos[1].PathBundle;
        //    AudioClip audioClip = audioBundle.LoadAsset<AudioClip>(soundManifest.soundInfos[1].PathBundle);
        //    FirecoalsSoundManager.PlayMusic(audioClip);
        //}
    }
}