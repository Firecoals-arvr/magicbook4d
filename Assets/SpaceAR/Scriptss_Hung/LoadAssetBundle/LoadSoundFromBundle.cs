using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firecoals.AssetBundles;
using Firecoals.AssetBundles.Sound;
using Loxodon.Framework.Bundles;

namespace Firecoals.Space
{
    /// <summary>
    /// load âm thanh từ bundles
    /// </summary>
    public class LoadSoundFromBundle : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            AssetBundlesLoader assetLoader = new AssetBundlesLoader();

            IBundle audioBundle = assetLoader.bundles["space/sound/name/en"];
            ISoundManifestLoader soundLoader = new SoundManifestLoader();
            var soundManifest = soundLoader.LoadSync(Application.streamingAssetsPath + "/AnimalAudioClip.json");
            var bundlePath = soundManifest.soundInfos[1].PathBundle;
            AudioClip audioClip = audioBundle.LoadAsset<AudioClip>(soundManifest.soundInfos[1].PathBundle);
            FirecoalsSoundManager.PlayMusic(audioClip);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
