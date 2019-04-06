using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    public class ShowHideButtonsOnScreen : MonoBehaviour
    {
        public GameObject hideMenu;
        public GameObject showMenu;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ShowMenu()
        {
            var x = TweenPosition.Begin(hideMenu, .5f, new Vector3(-474f, 0f, 0f));
            x.method = UITweener.Method.EaseInOut;
            x.Play(true);
            var y = TweenPosition.Begin(showMenu, .5f, new Vector3(0f, 0f, 0f));
            y.method = UITweener.Method.EaseInOut;
            y.Play(true);
        }

        public void HideMenu()
        {
            var x = TweenPosition.Begin(hideMenu, .5f, new Vector3(0f, 0f, 0f));
            x.method = UITweener.Method.EaseInOut;
            x.Play(true);
            var y = TweenPosition.Begin(showMenu, .5f, new Vector3(474f, 0f, 0f));
            y.method = UITweener.Method.EaseInOut;
            y.Play(true);
        }
    }
}