using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageAnimal : LanguageController
{
    public void SetLanguageVn()
    {
        OnOnPickLanguage("VI", "A");
        PlayerPrefs.SetString("AnimalLanguage", "VI");
    }
    public void SetLanguageEn()
    {
        OnOnPickLanguage("EN", "A");
        PlayerPrefs.SetString("AnimalLanguage", "EN");
    }
    public void SetLanguageJp()
    {
        OnOnPickLanguage("JP", "A");
        PlayerPrefs.SetString("AnimalLanguage", "JP");
    }
    //public void SetLanguageCn()
    //{
    //    OnOnPickLanguage("CN", "A");
    //    PlayerPrefs.SetString("AnimalLanguage", "CN");
    //}
}
