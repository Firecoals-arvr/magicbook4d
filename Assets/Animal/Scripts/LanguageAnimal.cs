using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageAnimal : LanguageController
{
    public UILabel LabelLanguage;
    public GameObject setting;
    private  void Start()
    {
    }
    public void SetLanguageVn()
    {
        OnOnPickLanguage("VI", "A");
        Localization.language = "VI";
        PlayerPrefs.SetString("AnimalLanguage", "VI");
        StartCoroutine(Wait());
    }
    public void SetLanguageEn()
    {
        OnOnPickLanguage("EN", "A");
        Localization.language = "EN";
        PlayerPrefs.SetString("AnimalLanguage", "EN");
        StartCoroutine(Wait());
    }
    public void SetLanguageJp()
    {
        OnOnPickLanguage("JP", "A");
        Localization.language = "JP";
        PlayerPrefs.SetString("AnimalLanguage", "JP");
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        setting.SetActive(false);
    }
    //public void SetLanguageCn()
    //{
    //    //OnOnPickLanguage("CN", "A");
    //    //PlayerPrefs.SetString("AnimalLanguage", "CN");
    //    LabelLanguage.text = "Ngôn ngữ hệ thống:CN";
    //}
}
