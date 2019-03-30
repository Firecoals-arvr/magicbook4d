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
    public class LoadSoundFromBundle : AssetBundlesLoader
    {
        SoundManifest soundManifest;

        public void LoadJson()
        {
            IBundle audioBundle = this.bundles["space/sound/name/en"];
            ISoundManifestLoader soundLoader = new SoundManifestLoader();
            soundManifest = soundLoader.LoadSync(Application.streamingAssetsPath + "/MagicAudioSpace.json");
            var bundlePath = soundManifest.soundInfos[1].PathBundle;
            Debug.Log("jsonfile loaded");
        }
        public void PLayX()
        {
            AudioClip audioClip = this.LoadAssetObject(soundManifest.soundInfos[1].PathBundle) as AudioClip;
            FirecoalsSoundManager.PlayMusic(audioClip);
        }
    }
}
