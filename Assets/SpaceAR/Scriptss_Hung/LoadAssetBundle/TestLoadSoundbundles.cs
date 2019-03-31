using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firecoals.Augmentation;
using Firecoals.AssetBundles.Sound;
using Loxodon.Framework.Bundles;
using System;
using System.IO;

namespace Firecoals.Space
{
    public class TestLoadSoundbundles : MonoBehaviour
    {
        
        private ISoundManifestLoader _soundManifest;
        private IBundle _bundleAudioClip;
        private AssetLoader _assetLoader;
        private IntroScripts _intro;

        string jsonText;

        private void Start()
        {
            _intro = GameObject.FindObjectOfType<IntroScripts>();
        }
        public void PlayNameSound(int num)
        {
            Debug.Log("loading json");
            _assetLoader = GameObject.FindObjectOfType<AssetLoader>();
            _bundleAudioClip = _assetLoader.assetBundlesLoader.bundles["space/sound/name/en"];
            _soundManifest = new SoundManifestLoader();
            var soundManifest = _soundManifest.LoadSync(Application.streamingAssetsPath + "/MagicAudioSpace.json");
            AudioClip audioClip = _bundleAudioClip.LoadAsset<AudioClip>(soundManifest.soundInfos[num].PathBundle);
            FirecoalsSoundManager.PlaySound(audioClip);
            
        }
        public void PlayInfoSound()
        {
            
        }
        public void ReplayNameSound()
        {
            PlayNameSound(_intro.soundNumber);
        }
    }
}
