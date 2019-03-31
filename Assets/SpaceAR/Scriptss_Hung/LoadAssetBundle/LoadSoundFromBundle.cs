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
            var soundManifest = _soundManifest.LoadSync(Application.streamingAssetsPath + "/MagicAudioSpace.json");
            AudioClip audioClip = _bundleAudioClip.LoadAsset<AudioClip>(soundManifest.soundInfos[1].PathBundle);
            FirecoalsSoundManager.PlaySound(audioClip);
        }

        //public void LoadJson(int num)
        //{
        //    //Debug.Log("loading json");
        //    //_assetLoader = GameObject.FindObjectOfType<AssetLoader>();
        //    //_bundleAudioClip = _assetLoader.assetBundlesLoader.bundles["space/sound/name/en"];
        //    //_soundManifest = new SoundManifestLoader();
        //    //var soundManifest = _soundManifest.LoadSync(Application.streamingAssetsPath + "/MagicAudioSpace.json");
        //    //AudioClip audioClip = _bundleAudioClip.LoadAsset<AudioClip>(soundManifest.soundInfos[num].PathBundle);
        //    //FirecoalsSoundManager.PlaySound(audioClip);

        //    //IBundle audioBundle = this.bundles["space/sound/name/en"];
        //    //ISoundManifestLoader soundLoader = new SoundManifestLoader();
        //    //soundManifest = soundLoader.LoadSync(Application.streamingAssetsPath + "/MagicAudioSpace.json");
        //    //var bundlePath = soundManifest.soundInfos[1].PathBundle;
        //    //Debug.Log("jsonfile loaded");
        //}
        //public void PLayX()
        //{
        //    AudioClip audioClip = this.LoadAssetObject(soundManifest.soundInfos[1].PathBundle) as AudioClip;
        //    FirecoalsSoundManager.PlayMusic(audioClip);
        //}

        public void PlayObjectSound()
        {
            string audioJsonPath = Application.streamingAssetsPath + "/MagicAudioSpace.json";
            if (File.Exists(audioJsonPath))
            {
                string data = File.ReadAllText(audioJsonPath);
                var listSound = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SoundInfo>>(data);
                SoundInfo sound = listSound.Where(s => s.Language == "english" && s.Category == "space").Single();
                //List<SoundInfo> soundList = JsonUtility.
                //SoundInfo soundinfo = JsonUtility.FromJson<SoundInfo>(data);
            }
            else
            {
                Debug.LogError("ko co file json.");
            }
        }
    }
}