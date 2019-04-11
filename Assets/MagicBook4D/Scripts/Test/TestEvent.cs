using Firecoals.AssetBundles.Sound;
using Firecoals.Augmentation;
using Loxodon.Framework.Bundles;
using UnityEngine;

public class TestEvent : MonoBehaviour
{
    public GameObject panel;
    public void ClickShowTooltip()
    {
        UITooltip.Show("Bầu trời trong xanh thăm thẳm, không một gợn mây.");
    }

    public void OpenPanel()
    {
            var x = TweenPosition.Begin(panel, .5f, new Vector3(-150f, 0f, 0f));
            x.method = UITweener.Method.EaseInOut;
            x.Play(true);

    }

    public void ClosePanel()
    {
        var x = TweenPosition.Begin(panel, .5f, new Vector3(14f, 0f, 0f));
        x.method = UITweener.Method.EaseInOut;
        x.Play(true);
    }
}

