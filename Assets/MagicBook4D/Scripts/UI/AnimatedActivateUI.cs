using Firecoals.Purchasing;
using UnityEngine;

public class AnimatedActivateUI : MonoBehaviour
{
    public GameObject phonePage, codePage;
    public void MoveNextCodePage()
    {
        if (ActivationManager.instance.IsValidPhone())
        {
            var x = TweenPosition.Begin(phonePage, .5f, new Vector3(-1480f, 0f, 0f));
            x.method = UITweener.Method.EaseInOut;
            x.Play(true);
            var y = TweenPosition.Begin(codePage, .5f, new Vector3(0f, 0f, 0f));
            y.method = UITweener.Method.EaseInOut;
            y.Play(true);
        }
        else
        {
            PopupManager.PopUpDialog("Lỗi", "Vui lòng nhập số điện thoại đúng");
        }
    }

    public void MoveBackPhonePage()
    {
        var x = TweenPosition.Begin(phonePage, .5f, new Vector3(0f, 0f, 0f));
        x.method = UITweener.Method.EaseInOut;
        x.Play(true);
        var y = TweenPosition.Begin(codePage, .5f, new Vector3(1480f, 0f, 0f));
        y.method = UITweener.Method.EaseInOut;
        y.Play(true);
    }
}
