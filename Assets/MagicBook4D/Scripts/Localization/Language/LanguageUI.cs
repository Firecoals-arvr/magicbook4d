using UnityEngine;

public class LanguageUI : LanguageController
{
    public UIButton animalButton, spaceButton, colorButton;

    public UIButton[] tableOfLanguages;
    private LanguageController languageController;
    private static string currentProjectID = "";
    private void Start()
    {
        languageController = GetComponent<LanguageController>();
        //Set event for animal language
        EventDelegate animalBtnDelegate = new EventDelegate(this, "OnOnShowListLanguage");
        animalBtnDelegate.parameters[0].value = "A";
        EventDelegate.Set(animalButton.onClick, animalBtnDelegate);
        //Set event for space language
        EventDelegate spaceBtnDelegate = new EventDelegate(this, "OnOnShowListLanguage");
        spaceBtnDelegate.parameters[0].value = "B";
        EventDelegate.Set(spaceButton.onClick, spaceBtnDelegate);
        //Set event for color language
        EventDelegate colorBtnDelegate = new EventDelegate(this, "OnOnShowListLanguage");
        colorBtnDelegate.parameters[0].value = "C";
        EventDelegate.Set(colorButton.onClick, colorBtnDelegate);
        InitialLanguage();
    }
    //TODO Add Sprite Name to Atlas
    //VI,EN,JP,CN,KR
    private void InitialLanguage()
    {
        OnOnPickLanguage(PlayerPrefs.HasKey("AnimalLanguage") ? PlayerPrefs.GetString("AnimalLanguage") : "VI", "A");
        OnOnPickLanguage(PlayerPrefs.HasKey("SpaceLanguage") ? PlayerPrefs.GetString("SpaceLanguage") : "VI", "B");
    }
    protected override void OnOnPickLanguage(string languageId, string projectId)
    {

        switch (projectId)
        {
            case "A":
                //TODO Set Language for Animal
                Localization.language = languageId;
                //TODO Set icon for Animal Button
                Debug.LogWarning(UIButton.current.name);
                //animalButton.GetComponentInChildren<UISprite>().spriteName = UIButton.current.name;
                if (UIButton.current != null)
                    PlayerPrefs.SetString("AnimalLanguage", UIButton.current.name);
                break;
            case "B":
                //TODO Set Language for Space
                Localization.language = languageId;
                //TODO Set icon for Space Button
                //spaceButton.GetComponentInChildren<UISprite>().spriteName = UIButton.current.name;
                if (UIButton.current != null)
                    PlayerPrefs.SetString("SpaceLanguage", UIButton.current.name);
                break;
                //TODO no needed set Language for Color
                //case "C":
                //    Localization.language = languageId;
                //    break;
        }
        base.OnOnPickLanguage(languageId, projectId);
    }

    protected override void OnOnShowListLanguage(string projectId)
    {
        switch (projectId)
        {
            case "A":
                //Hide KR Show VI, EN, CN, JP
                currentProjectID = projectId;
                foreach (UIButton language in tableOfLanguages)
                {
                    NGUITools.SetActive(language.gameObject, language.name != "KR");
                }

                break;
            case "B":
                //Hide CN, JP, KR
                currentProjectID = projectId;
                foreach (UIButton language in tableOfLanguages)
                {
                    if (language.name == "VI" || language.name == "EN")
                    {
                        NGUITools.SetActive(language.gameObject, true);
                    }
                    else
                    {
                        NGUITools.SetActive(language.gameObject, false);
                    }
                }

                break;
            case "C":
                //No learning language needed
                PopupManager.PopUpDialog("Thông báo", "Nội dung Thế giới quanh em hiện chỉ hỗ trợ Tiếng Việt");
                break;
        }
        foreach (var language in tableOfLanguages)
        {
            EventDelegate btnDelegate = new EventDelegate(this, "OnOnPickLanguage");
            btnDelegate.parameters[0].value = language.name;
            btnDelegate.parameters[1].value = currentProjectID;
            EventDelegate.Set(language.onClick, btnDelegate);
        }
        base.OnOnShowListLanguage(projectId);
    }
}
