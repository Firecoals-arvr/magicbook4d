
using Firecoals.AssetBundles.Sound;
using Firecoals.Augmentation;
using Loxodon.Framework.Bundles;
using UnityEngine;

public class TestEvent : MonoBehaviour
{
    private AssetLoader _assetLoader;
    private void Start()
    {
        _assetLoader = GameObject.FindObjectOfType<AssetLoader>();
    }
    public void ClickShowTooltip()
    {
        UITooltip.Show("Bầu trời trong xanh thăm thẳm, không một gợn mây.");
    }

    public void PlaySoundFromAssetBundle()
    {
        IBundle bundleAudioClip = _assetLoader.assetBundlesLoader.bundles["space/sound/name/en"];
        ISoundManifestLoader soundManifestLoader = new SoundManifestLoader();
        var soundManifest = soundManifestLoader.LoadSync(Application.streamingAssetsPath + "/MagicAudioSpace.json");
        //var myBundlePath = soundManifest.soundInfos[5].PathBundle;
        //TODO query json by tag and language = bundle path
        AudioClip audioClip = bundleAudioClip.LoadAsset<AudioClip>(soundManifest.soundInfos[38].PathBundle);
        FirecoalsSoundManager.PlaySound(audioClip);
    }
}

