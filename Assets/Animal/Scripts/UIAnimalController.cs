using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimalController : MonoBehaviour
{
    public GameObject SoundName,Language;
    public void MoveNextCodePage()
    { 
            var x = TweenPosition.Begin(SoundName, .5f, new Vector3(1480f, 0f, 0f));
            x.method = UITweener.Method.EaseInOut;
            x.Play(true);
            var y = TweenPosition.Begin(Language, .5f, new Vector3(0f, 0f, 0f));
            y.method = UITweener.Method.EaseInOut;
            y.Play(true);
    }

    public void MoveBackPhonePage()
    {
        var x = TweenPosition.Begin(SoundName, .5f, new Vector3(0f, 0f, 0f));
        x.method = UITweener.Method.EaseInOut;
        x.Play(true);
        var y = TweenPosition.Begin(Language, .5f, new Vector3(-1480f, 0f));
        y.method = UITweener.Method.EaseInOut;
        y.Play(true);
    }
}
