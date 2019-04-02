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
        IBundle bundleAudioClip = _assetLoader.assetBundlesLoader.bundles["animals/noise"];
        ISoundManifestLoader soundManifestLoader = new SoundManifestLoader();
        var soundManifest = soundManifestLoader.LoadSync(Application.streamingAssetsPath + "/AnimalAudioClip.json");
        //var myBundlePath = soundManifest.soundInfos[5].PathBundle;
        AudioClip audioClip = bundleAudioClip.LoadAsset<AudioClip>(soundManifest.soundInfos[5].PathBundle);
        FirecoalsSoundManager.PlaySound(audioClip);
    }

    public void ShowOKPopUp()
    {
        if (!PopupManager.showingDialog)
        {
            PopupManager.PopUpDialog("OK Dialog", "Bầu trời trong xanh thăm thẳm, không một gợn mây", PopupManager.DialogType.OkDialog);
        }
    }

    public void ShowYesNoPopUp()
    {
        if (!PopupManager.showingDialog)
        {
            PopupManager.PopUpDialog("Yes/No Dialog","Bầu trời trong xanh thăm thẳm, không một gợn mây", PopupManager.DialogType.YesNoDialog,
                () => { Debug.Log("You've clicked NO button"); }, () => { Debug.Log("You've clicked YES button"); });
        }
        
    }
}

