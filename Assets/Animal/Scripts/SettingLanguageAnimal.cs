using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingLanguageAnimal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponentInChildren<UISprite>().spriteName = PlayerPrefs.GetString("AnimalLanguage");
    }
}
