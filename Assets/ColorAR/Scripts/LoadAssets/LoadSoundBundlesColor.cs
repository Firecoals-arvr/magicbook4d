using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firecoals.Augmentation;
using Firecoals.AssetBundles.Sound;
using Loxodon.Framework.Bundles;
using System;
using Loxodon.Framework.Asynchronous;

namespace Firecoals.Color
{
    public class LoadSoundBundlesColor : MonoBehaviour
    {
        /// <summary>
        /// các biến để load sound
        /// </summary>
        private ISoundManifestLoader _soundManifest;
        private IBundle _bundleAudioClip;
        /// <summary>
        /// biến asset loader để preload sound từ bundles
        /// </summary>
        private AssetLoader _assetLoader;
        /// <summary>
        /// mảng các image target để lấy tag sound và tag info
        /// </summary>
        GameObject[] imageTarget;
        /// <summary>
        /// 2 mảng sound info để lấy được các giá trị trong file json
        /// </summary>
        private SoundInfo[] sounds;
        
        private void Start()
        {
            // tìm tất cả các tranh có tag image target
            imageTarget = GameObject.FindGameObjectsWithTag("ImageTarget");

        }
        // hàm để chạy tên của model khi tìm thấy tranh
        public void PlaySound(string tagSound)
        {
            // cho preload bundles
            _assetLoader = GameObject.FindObjectOfType<AssetLoader>();
            // soundmanifest để load file json
            _soundManifest = new SoundManifestLoader();
            var soundManifest = _soundManifest.LoadSync(Application.streamingAssetsPath + "/ColorAudio.json");
            //đưa file json về thành 1 mảng
            sounds = soundManifest.soundInfos;
            _bundleAudioClip = _assetLoader.assetBundlesLoader.bundles["color/sounds/sounds"];
            //PlaySoundAsync();
            AudioClip audioClip = _bundleAudioClip.LoadAsset<AudioClip>(GetSoundBundlePath("all", tagSound));
            FirecoalsSoundManager.PlaySound(audioClip);
            //Debug.LogWarning("<color=pink>" + _bundleAudioClip.LoadAsset<AudioClip>("ColorAR/ColorResource/Sounds/Music/MusicTrangTrai.ogg") + "</color>");
        }

        private void PlaySoundAsync()
        {
            //_bundleAudioClip = _assetLoader.assetBundlesLoader.bundles["color/sounds/sounds"];
            IProgressResult<float, AudioClip> progressResult = _bundleAudioClip.LoadAssetAsync<AudioClip>("Assets/ColorAR/ColorResource/Sounds/Music/MusicCamTrai.ogg");
            progressResult.Callbackable().OnProgressCallback((p) =>
            {
                Debug.Log("<color=lime>loading audioclip " + p * 100 + "%</color>");
            });
            progressResult.Callbackable().OnCallback((r) =>
            {
                if (r.Exception != null)
                    throw r.Exception;
                Debug.Log("<color=purple>loaded sound named: " + r.Result.name + "</color>");
                FirecoalsSoundManager.PlaySound(r.Result);
            });
        }
        // hàm để lấy dc path của tên bundles
        private string GetSoundBundlePath(string currentLanguage, string tag)
        {
            // chạy từng thằng trong sound bundles
            foreach (var sound in sounds)
            {
                // nếu ngôn ngữ truyền vào + tên của model đúng thì trả về 1 đường path
                // còn ko thì trả về null
                if (sound.Language == currentLanguage && sound.Tag == tag)
                {
                    return sound.PathBundle;
                }
            }
            return string.Empty;
        }
       
        
    }
}
