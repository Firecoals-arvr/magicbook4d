using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firecoals.Augmentation;
using Firecoals.AssetBundles.Sound;
using Loxodon.Framework.Bundles;


namespace Firecoals.Space
{
    /// <summary>
    /// class này để load sound từ bundles
    /// </summary>
    public class LoadSoundbundles : MonoBehaviour
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
        private SoundInfo[] soundNames;
        private SoundInfo[] soundInfos;
        /// <summary>
        ///  biến check ngôn ngữ
        /// </summary>
        private SelectLanguage select;
        string language;

        /// <summary>
        /// chứa các giá trị file json, để lấy ra nhạc của 1 số object có nhạc riêng
        /// </summary>
        private SoundInfo[] _objectMusic = default;

        private ObjectInformation _objInfor;

        private void Start()
        {
            // tìm script ngôn ngữ ở trên scene
            select = GameObject.FindObjectOfType<SelectLanguage>();
            // tìm tất cả các tranh có tag image target
            imageTarget = GameObject.FindGameObjectsWithTag("ImageTarget");

            //tìm object có kiểu là ObjectInformation
            _objInfor = GameObject.FindObjectOfType<ObjectInformation>();

        }
        // hàm để chạy tên của model khi tìm thấy tranh
        public void PlayNameSound(string tagSound)
        {
            // cho preload bundles
            _assetLoader = GameObject.FindObjectOfType<AssetLoader>();
            // soundmanifest để load file json
            _soundManifest = new SoundManifestLoader();
            var soundManifest = _soundManifest.LoadSync(Application.streamingAssetsPath + "/SpaceAudio.json");
            //đưa file json về thành 1 mảng
            soundNames = soundManifest.soundInfos;
            // nếu ngôn ngữ đang dc chọn là tiếng anh
            if (select.en == true)
            {
                language = "english";
                //load tất cả các bundles có tên là space/sound/name/en
                _bundleAudioClip = _assetLoader.assetBundlesLoader.bundles["space/sound/name/en"];
                //load audioclip theo path được tìm bởi tag name là tên của model + ngôn ngữ
                AudioClip audioClip = _bundleAudioClip.LoadAsset<AudioClip>(GetSoundBundlePath(language, tagSound));
                // play sound
                FirecoalsSoundManager.PlaySound(audioClip);
            }
            // giống vs tiếng anh
            if (select.vn == true)
            {
                language = "vietnamese";
                _bundleAudioClip = _assetLoader.assetBundlesLoader.bundles["space/sound/name/vn"];
                AudioClip audioClip = _bundleAudioClip.LoadAsset<AudioClip>(GetSoundBundlePath(language, tagSound));
                FirecoalsSoundManager.PlaySound(audioClip);
            }
        }
        // hàm để lấy dc path của tên bundles
        private string GetSoundBundlePath(string currentLanguage, string tag)
        {
            // chạy từng thằng trong sound bundles
            foreach (var soundName in soundNames)
            {
                // nếu ngôn ngữ truyền vào + tên của model đúng thì trả về 1 đường path
                // còn ko thì trả về null
                if (soundName.Language == currentLanguage && soundName.Tag == tag)
                {
                    return soundName.PathBundle;
                }
            }
            return string.Empty;
        }
        // giống vs sound name
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
        // hàm này để chạy lại tên của modle khi ấn vào nút loa
        public void ReplayNameSound()
        {
            // tìm từng ảnh trên scene
            foreach (GameObject go in imageTarget)
            {
                // nếu thằng nào đang có con tức là đang đc tracking found thì chạy lại hàm play name sound
                if (go.transform.childCount > 0 && go.gameObject.activeInHierarchy)
                {
                    PlayNameSound(go.transform.GetComponentInParent<IntroScripts>().tagSound);
                }
            }
        }
        // giống vs hàm trên
        public void ReplayInfoSound()
        {
            foreach (GameObject go in imageTarget)
            {
                if (go.transform.childCount > 0 && go.gameObject.activeInHierarchy)
                {
                    if (_objInfor.checkOpen == true)
                    {
                        PlayInfoSound(go.transform.GetComponentInParent<IntroScripts>().tagInfo);
                    }
                }
            }
        }

        /// <summary>
        /// load file nhạc nền
        /// </summary>
        /// <param name="tag"></param>
        public void PlayMusicOfObjects(string tag)
        {
            _assetLoader = GameObject.FindObjectOfType<AssetLoader>();
            _soundManifest = new SoundManifestLoader();
            var soundManifest = _soundManifest.LoadSync(Application.streamingAssetsPath + "/SpaceAudio.json");
            soundInfos = soundManifest.soundInfos;

            _bundleAudioClip = _assetLoader.assetBundlesLoader.bundles["space/music"];
            AudioClip audioClip = _bundleAudioClip.LoadAsset<AudioClip>(GetMusicPath(tag));
            FirecoalsSoundManager.PlaySound(audioClip);
        }

        /// <summary>
        /// lấy đường dẫn của file nhạc nền
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private string GetMusicPath(string tag)
        {
            foreach (var music in _objectMusic)
            {
                if (music.Language == "all" && music.Tag == tag)
                {
                    return music.PathBundle;
                }
            }
            return string.Empty;
        }
    }
}
