using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firecoals.Augmentation;
using Firecoals.AssetBundles.Sound;
using Loxodon.Framework.Bundles;

namespace Firecoals.Animal
{
    public class LoadSoundbundles : MonoBehaviour
    {
        private ISoundManifestLoader _soundManifest;
        private IBundle _bundleAudioClip;
        private AssetLoader _assetLoader;
        //private IntroScripts _intro;
        GameObject[] imageTarget;
        //public GameObject target;
        //bool isTrackable;
        private SoundInfo[] soundNames;
        private SoundInfo[] soundInfos;
        private SoundInfo[] soundNoises;
        private string language;
        private void Start()
        {
            imageTarget = GameObject.FindGameObjectsWithTag("ImageTarget");
        }
        public void PlaySound(string sound)
        {
            Debug.Log("loading json");
            _assetLoader = GameObject.FindObjectOfType<AssetLoader>();
            _soundManifest = new SoundManifestLoader();
            var soundManifest = _soundManifest.LoadSync(Application.streamingAssetsPath + "/AnimalAudio.json");
            soundNoises = soundManifest.soundInfos;
            language = "all";
            _bundleAudioClip = _assetLoader.assetBundlesLoader.bundles["animals/noise"];
            AudioClip audioClip = _bundleAudioClip.LoadAsset<AudioClip>(GetSoundBundlePath(language, sound));
            FirecoalsSoundManager.PlaySound(audioClip);
        }
        private string GetSoundBundlePath(string currentLanguage, string tag)
        {
            foreach (var soundNoise in soundNoises)
            {
                if (soundNoise.Language == currentLanguage && soundNoise.Tag == tag)
                {
                    return soundNoise.PathBundle;
                }
            }
            return string.Empty;
        }
        public void PlayName(string tagName)
        {
            _assetLoader = GameObject.FindObjectOfType<AssetLoader>();
            _soundManifest = new SoundManifestLoader();
            var soundManifest = _soundManifest.LoadSync(Application.streamingAssetsPath + "/AnimalAudio.json");
            soundNames = soundManifest.soundInfos;
            //if (select.en == true)
            //{

            //}
            language = "vietnamese";
            _bundleAudioClip = _assetLoader.assetBundlesLoader.bundles["animals/name/vn"];
            AudioClip audioClip = _bundleAudioClip.LoadAsset<AudioClip>(GetNameBundlePath(language, tagName));
            FirecoalsSoundManager.PlaySound(audioClip);
            //if (select.vn == true)
            //{
            //    language = "vietnamese";
            //    _bundleAudioClip = _assetLoader.assetBundlesLoader.bundles["animals/sound/info/vn"];
            //    AudioClip audioClip = _bundleAudioClip.LoadAsset<AudioClip>(GetSoundBundlePath(language, tagInfo));
            //    FirecoalsSoundManager.PlaySound(audioClip);

            //}
        }
        private string GetNameBundlePath(string currentLanguage, string tag)
        {
            foreach (var soundName in soundNames)
            {
                if (soundName.Language == currentLanguage && soundName.Tag == tag)
                {
                    return soundName.PathBundle;
                }
            }
            return string.Empty;
        }

        public void PlayInfo(string tagInfo)
        {
            _assetLoader = GameObject.FindObjectOfType<AssetLoader>();
            _soundManifest = new SoundManifestLoader();
            var soundManifest = _soundManifest.LoadSync(Application.streamingAssetsPath + "/AnimalAudio.json");
            soundInfos = soundManifest.soundInfos;
            //if (select.en == true)
            //{

            //}
            language = "vietnamese";
            _bundleAudioClip = _assetLoader.assetBundlesLoader.bundles["animals/info/vn"];
            AudioClip audioClip = _bundleAudioClip.LoadAsset<AudioClip>(GetInfoBundlePath(language, tagInfo));
            FirecoalsSoundManager.PlaySound(audioClip);
            //if (select.vn == true)
            //{
            //    language = "vietnamese";
            //    _bundleAudioClip = _assetLoader.assetBundlesLoader.bundles["animals/sound/info/vn"];
            //    AudioClip audioClip = _bundleAudioClip.LoadAsset<AudioClip>(GetSoundBundlePath(language, tagInfo));
            //    FirecoalsSoundManager.PlaySound(audioClip);

            //}
        }
        private string GetInfoBundlePath(string currentLanguage, string tag)
        {
            foreach (var soundInfo in soundInfos)
            {
                if (soundInfo.Language == currentLanguage && soundInfo.Tag == tag)
                {
                    return soundInfo.PathBundle;
                }
            }
            return string.Empty;
        }
        
        public void ReplayName()
        {
            foreach (GameObject go in imageTarget)
            {
                if (go.transform.childCount > 0)
                {
                    PlayName(go.transform.GetComponentInParent<LoadModleAnimal>().tagName);
                }
            }

        }
        public void ReplayInfo()
        {
            foreach (GameObject go in imageTarget)
            {
                if (go.transform.childCount > 0)
                {
                    PlayInfo(go.transform.GetComponentInParent<LoadModleAnimal>().tagInfo);
                }
            }

        }

    }
}
