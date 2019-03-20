using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class LanguageController : MonoBehaviour
{

    public static string LanguageCurrent;
    public Button LayerLanguageDefault;
    // Use this for initialization
    void Start()
    {
        if (!PlayerPrefs.HasKey("LanguageCurrent"))
        {
            PlayerPrefs.SetString("LanguageCurrent", "VN");
            LanguageCurrent = PlayerPrefs.GetString("LanguageCurrent");
        }
        else
        {
            LanguageCurrent = PlayerPrefs.GetString("LanguageCurrent");
        }
        anima = GetComponent<Animation>();
        for (int i = 0; i < spriteLanguage.Length; i++)
        {
            if (spriteLanguage[i].name == LanguageCurrent)
            {
                LayerLanguageDefault.GetComponent<Image>().sprite = spriteLanguage[i].GetComponent<Image>().sprite;
            }
        }
    }
    private void changelanguage()
    {
        for (int i = 0; i < spriteLanguage.Length; i++)
        {
            if (spriteLanguage[i].name == LanguageCurrent)
            {
                LayerLanguageDefault.GetComponent<Image>().sprite = spriteLanguage[i].GetComponent<Image>().sprite;
            }
        }
    }
    Animation anima;
    public Button[] spriteLanguage;
    // Update is called once per frame
    void Update()
    {

    }

    public void openselectlanguage()
    {
        if (anima.IsPlaying("Default"))
        {
            anima.Play("Open");
        }
    }
    public void selectlanguage()
    {
        anima.Play("Default");
        PlayerPrefs.SetString("LanguageCurrent", EventSystem.current.currentSelectedGameObject.name);
        LanguageCurrent = PlayerPrefs.GetString("LanguageCurrent");
        changelanguage();
    }
}
