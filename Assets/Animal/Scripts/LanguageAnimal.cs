using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageAnimal : LanguageController
{
    public UILabel LabelLanguage;
    private  void Start()
    {
    }
    public void SetLanguageVn()
    {
        OnOnPickLanguage("VI", "A");
        Localization.language = "VI";
        PlayerPrefs.SetString("AnimalLanguage", "VI");
    }
    public void SetLanguageEn()
    {
        OnOnPickLanguage("EN", "A");
        Localization.language = "EN";
        PlayerPrefs.SetString("AnimalLanguage", "EN");
    }
    public void SetLanguageJp()
    {
        OnOnPickLanguage("JP", "A");
        Localization.language = "JP";
        PlayerPrefs.SetString("AnimalLanguage", "JP");
    }
    //public void SetLanguageCn()
    //{
    //    //OnOnPickLanguage("CN", "A");
    //    //PlayerPrefs.SetString("AnimalLanguage", "CN");
    //    LabelLanguage.text = "Ngôn ngữ hệ thống:CN";
    //}
}
