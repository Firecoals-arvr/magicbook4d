using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firecoals.Augmentation;
using Firecoals.AssetBundles.Sound;
using Loxodon.Framework.Bundles;


namespace Firecoals.Space
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
        private SelectLanguage select;
        string language;
        


        private void Start()
        {
            select = GameObject.FindObjectOfType<SelectLanguage>();
            imageTarget = GameObject.FindGameObjectsWithTag ("ImageTarget");
            Debug.Log("number = " + imageTarget.Length);
        }
        public void PlayNameSound(string tagSound)
        {
            Debug.Log("loading json");
            _assetLoader = GameObject.FindObjectOfType<AssetLoader>();
            _soundManifest = new SoundManifestLoader();
            var soundManifest = _soundManifest.LoadSync(Application.streamingAssetsPath + "/SpaceAudio.json");
            soundNames = soundManifest.soundInfos;

            

            if (select.en == true)
            {
                language = "english";
                _bundleAudioClip = _assetLoader.assetBundlesLoader.bundles["space/sound/name/en"];
                AudioClip audioClip = _bundleAudioClip.LoadAsset<AudioClip>(GetSoundBundlePath(language, tagSound));
                FirecoalsSoundManager.PlaySound(audioClip);
            }
            if(select.vn == true)
            {
                language = "vietnamese";
                _bundleAudioClip = _assetLoader.assetBundlesLoader.bundles["space/sound/name/vn"];
                AudioClip audioClip = _bundleAudioClip.LoadAsset<AudioClip>(GetSoundBundlePath(language, tagSound));
                FirecoalsSoundManager.PlaySound(audioClip);
            }

        }
        private string  GetSoundBundlePath(string currentLanguage, string tag)
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
        private void PlayInfoSound(string tagInfo)
        {
            _assetLoader = GameObject.FindObjectOfType<AssetLoader>();
            _soundManifest = new SoundManifestLoader();
            var soundManifest = _soundManifest.LoadSync(Application.streamingAssetsPath + "/SpaceAudio.json");
            soundInfos = soundManifest.soundInfos;
            if (select.en == true)
            {
                language = "english";
                _bundleAudioClip = _assetLoader.assetBundlesLoader.bundles["space/sound/info/en"];
                AudioClip audioClip = _bundleAudioClip.LoadAsset<AudioClip>(GetSoundBundlePath(language, tagInfo));
                FirecoalsSoundManager.PlaySound(audioClip);
            }
            if (select.vn == true)
            {
                language = "vietnamese";
                _bundleAudioClip = _assetLoader.assetBundlesLoader.bundles["space/sound/info/vn"];
                AudioClip audioClip = _bundleAudioClip.LoadAsset<AudioClip>(GetSoundBundlePath(language, tagInfo));
                FirecoalsSoundManager.PlaySound(audioClip);
                
            }
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
        public void ReplayNameSound()
        {
            foreach (GameObject go in imageTarget)
            {
                if (go.transform.childCount > 0)
                {
                    PlayNameSound(go.transform.GetComponentInParent<IntroScripts>().tagSound);
                }
            }

        }
        public void ReplayInfoSound()
        {
            foreach (GameObject go in imageTarget)
            {
                if (go.transform.childCount > 0)
                {
                    PlayInfoSound(go.transform.GetComponentInParent<IntroScripts>().tagInfo);
                }
            }

        }
    }
}
