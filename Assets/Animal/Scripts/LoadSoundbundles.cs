using Firecoals.AssetBundles.Sound;
using Firecoals.Augmentation;
using Loxodon.Framework.Bundles;
using UnityEngine;

namespace Firecoals.Animal
{
    public class LoadSoundbundles : MonoBehaviour
    {
        private ISoundManifestLoader _soundManifest;
        private IBundle _bundleAudioClip;
        private AssetLoader _assetLoader;
        private GameObject[] imageTarget;
        private SoundInfo[] soundNames;
        private SoundInfo[] soundInfos;
        private SoundInfo[] soundNoises;
        private string language;
        private void Start()
        {
            imageTarget = GameObject.FindGameObjectsWithTag("ImageTarget");
            language = PlayerPrefs.GetString("AnimalLanguage");
        }
        public void PlaySound(string sound)
        {
            //Debug.Log("loading json");
            _assetLoader = GameObject.FindObjectOfType<AssetLoader>();
            _soundManifest = new SoundManifestLoader();
            var soundManifest = _soundManifest.LoadSync(Application.streamingAssetsPath + "/AnimalAudioClip.json");
            soundNoises = soundManifest.soundInfos;
            _bundleAudioClip = _assetLoader.assetBundlesLoader.bundles["animals/noise"];
            //Debug.LogWarning("GetNameBundlePath" + GetSoundBundlePath("all", sound).ToString());
            AudioClip audioClip = _bundleAudioClip.LoadAsset<AudioClip>(GetSoundBundlePath("all", sound));
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
            language = PlayerPrefs.GetString("AnimalLanguage");
            _assetLoader = GameObject.FindObjectOfType<AssetLoader>();
            _soundManifest = new SoundManifestLoader();
            var soundManifest = _soundManifest.LoadSync(Application.streamingAssetsPath + "/AnimalAudioClip.json");
            soundNames = soundManifest.soundInfos;
            switch (language)
            {
                case "VI":
                    SetSoundNameBuilder("animals/name/vn", tagName, "vietnamese");
                    break;
                case "EN":
                    SetSoundNameBuilder("animals/name/en", tagName, "english");
                    break;
                case "CN":
                    SetSoundNameBuilder("animals/name/cn", tagName, "chinese");
                    break;
                case "JP":
                    SetSoundNameBuilder("animals/name/jp", tagName, "japanese");
                    break;
            }
        }

        public void SetSoundNameBuilder(string bundlesSound, string tag, string _language)
        {
            _bundleAudioClip = _assetLoader.assetBundlesLoader.bundles[bundlesSound];
            AudioClip audioClip = _bundleAudioClip.LoadAsset<AudioClip>(GetNameBundlePath(_language, tag));
            FirecoalsSoundManager.PlaySound(audioClip);
        }
        public void SetSoundInfoBuilder(string bundlesSound, string tag, string _language)
        {
            _bundleAudioClip = _assetLoader.assetBundlesLoader.bundles[bundlesSound];
            //Debug.Log("_language" + _language + "tag" + tag);
            AudioClip audioClip = _bundleAudioClip.LoadAsset<AudioClip>(GetInfoBundlePath(_language, tag));

            FirecoalsSoundManager.PlaySound(audioClip);
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
            language = PlayerPrefs.GetString("AnimalLanguage");
            _assetLoader = GameObject.FindObjectOfType<AssetLoader>();
            _soundManifest = new SoundManifestLoader();
            var soundManifest = _soundManifest.LoadSync(Application.streamingAssetsPath + "/AnimalAudioClip.json");
            soundInfos = soundManifest.soundInfos;
            //Debug.LogWarning("language"+language+ "tagInfo"+ tagInfo);         
            switch (language)
            {
                case "VI":
                    SetSoundInfoBuilder("animals/info/vn", tagInfo, "vietnamese");
                    break;
                case "EN":
                    SetSoundInfoBuilder("animals/info/en", tagInfo, "english");
                    break;
                case "JP":
                    SetSoundInfoBuilder("animals/info/jp", tagInfo, "japanese");
                    break;
            }

        }
        private string GetInfoBundlePath(string currentLanguage, string tag)
        {
            foreach (var soundInfo in soundInfos)
            {
                if (soundInfo.Language == currentLanguage && soundInfo.Tag == tag)
                {
                    //Debug.LogWarning("Searching");
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
                if (go.transform.childCount > 0 && go.transform.GetChild(0).gameObject.activeSelf)
                {
                    //Debug.LogWarning("ssssssssssss"+go.transform.GetComponentInParent<LoadModleAnimal>().tagInfo);
                    PlayInfo(go.transform.GetComponentInParent<LoadModleAnimal>().tagInfo);
                }
            }

        }

    }
}
