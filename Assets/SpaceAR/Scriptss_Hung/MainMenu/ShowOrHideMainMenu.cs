﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace FireCoals.Space
{
    public class ShowOrHideMainMenu : MonoBehaviour
    {

        public GameObject mainMenu;
        public GameObject buttonShowMenu;

        public void HideMenu()
        {
            mainMenu.GetComponent<Transform>().DOLocalMove(new Vector3(-1286f, 26f, 146.1736f), 0.8f);
            buttonShowMenu.GetComponent<Transform>().DOLocalMove(new Vector3(-799f, 23f, 0f), 0.8f);
        }

        public void ShowMenu()
        {
            mainMenu.GetComponent<Transform>().DOLocalMove(new Vector3(-864f, 29f, 146.1736f), 0.8f);
            buttonShowMenu.GetComponent<Transform>().DOLocalMove(new Vector3(-1237f, 23f, 0f), 0.8f);
        }
    }
}