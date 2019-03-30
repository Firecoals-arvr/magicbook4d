using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedActivateUI : MonoBehaviour
{
    public GameObject phonePage, codePage;
    public void MoveNextCodePage()
    {
        var x = TweenPosition.Begin(phonePage, .5f, new Vector3(-1480f, 0f, 0f));
        x.method = UITweener.Method.EaseInOut;
        x.Play(true);
        var y = TweenPosition.Begin(codePage, .5f, new Vector3(0f, 0f, 0f));
        y.method = UITweener.Method.EaseInOut;
        y.Play(true);
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
