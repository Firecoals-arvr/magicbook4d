using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firecoals.AssetBundles;
using Firecoals.AssetBundles.Sound;
using Loxodon.Framework.Bundles;
using System.IO;
using System.Linq;
using Firecoals.Augmentation;

namespace Firecoals.Space
{
    /// <summary>
    /// load âm thanh từ bundles
    /// </summary>
    public class LoadSoundFromBundle : MonoBehaviour
    {
        SoundManifest soundManifest;

        private ISoundManifestLoader _soundManifest;
        private IBundle _bundleAudioClip;
        private AssetLoader _assetLoader;
        private IntroScripts _intro;

        void Start()
        {
            _intro = GameObject.FindObjectOfType<IntroScripts>();

            Debug.Log("loading json");
            _assetLoader = GameObject.FindObjectOfType<AssetLoader>();
            _bundleAudioClip = _assetLoader.assetBundlesLoader.bundles["space/sound/name/en"];
            _soundManifest = new SoundManifestLoader();
            var soundManifest = _soundManifest.LoadSync(Application.streamingAssetsPath + "/SpaceAudio.json");
            AudioClip audioClip = _bundleAudioClip.LoadAsset<AudioClip>(soundManifest.soundInfos[1].PathBundle);
            FirecoalsSoundManager.PlaySound(audioClip);
        }

        void PlaySound()
        {

        }
    }
}