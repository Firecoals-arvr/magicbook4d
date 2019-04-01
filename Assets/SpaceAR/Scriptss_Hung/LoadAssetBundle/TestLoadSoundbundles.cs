using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firecoals.Augmentation;
using Firecoals.AssetBundles.Sound;
using Loxodon.Framework.Bundles;


namespace Firecoals.Space
{
    public class TestLoadSoundbundles : MonoBehaviour
    {
        
        private ISoundManifestLoader _soundManifest;
        private IBundle _bundleAudioClip;
        private AssetLoader _assetLoader;
        private IntroScripts _intro;
        //List<GameObject> imageTarget = new List<GameObject>();
        //public GameObject target;
        //bool isTrackable;
        private SoundInfo[] soundInfos;
        private SelectLanguage select;
        string language;


        private void Start()
        {
            select = GameObject.FindObjectOfType<SelectLanguage>();
            
        }

        
        public void PlayInfoSound()
        {
            
        }
        public void ReplayNameSound()
        {
            
            
        }
        
        public void PlayNameSound(string tag)
        {
            Debug.Log("loading json");
            _assetLoader = GameObject.FindObjectOfType<AssetLoader>();
            _bundleAudioClip = _assetLoader.assetBundlesLoader.bundles["space/sound/name/en"];
            _soundManifest = new SoundManifestLoader();
            var soundManifest = _soundManifest.LoadSync(Application.streamingAssetsPath + "/SpaceAudio.json");
            soundInfos = soundManifest.soundInfos;
            if (select.en == true)
            {
                language = "english";
                _bundleAudioClip = _assetLoader.assetBundlesLoader.bundles["space/sound/name/en"];
                AudioClip audioClip = _bundleAudioClip.LoadAsset<AudioClip>(GetSoundBundlePath(language, tag));
                FirecoalsSoundManager.PlaySound(audioClip);
            }
            if(select.vn == true)
            {
                language = "vietnamese";
                _bundleAudioClip = _assetLoader.assetBundlesLoader.bundles["space/sound/name/vn"];
                AudioClip audioClip = _bundleAudioClip.LoadAsset<AudioClip>(GetSoundBundlePath(language, tag));
                FirecoalsSoundManager.PlaySound(audioClip);
            }

        }

        public string  GetSoundBundlePath(string currentLanguage, string tag)
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
    }
}
