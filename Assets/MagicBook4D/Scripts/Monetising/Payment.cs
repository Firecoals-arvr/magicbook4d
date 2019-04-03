using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Payment : MonoBehaviour
{

    public UIButton activateCodeButton;
    void Start()
    {
        InAppPurchaseInGame.InitIAP();
        Inititial();
        StartCheckingLicense();
        //PlayerPrefs.DeleteKey("Project_A");
        //PlayerPrefs.DeleteKey("Project_B");
        //PlayerPrefs.DeleteKey("Project_C");
    }

    private void Inititial()
    {
        InAppPurchaseInGame.IAPOnPurchaseSuccess += OnPurchaseSuccess;
        InAppPurchaseInGame.IAPOnPurchaseFailed += OnPurchaseFailed;

        ActiveManager.OnRegisterLicenseSuccess += OnRegisterLicenseSuccess;
        ActiveManager.OnRegisterLicenseFailure += OnRegisterLicenseFailure;

        ActiveManager.OnPlayerServerActived += OnPlayerServerActived;
        ActiveManager.OnPlayerServerNotActived += OnPlayerServerNotActived;

        ActiveManager.OnConnectionError += OnConnectionError;
#if UNITY_IOS
        ActiveManager.OnShowActivationButton += OnShowActivationButton;
        ActiveManager.OnHideActivationButton += OnHideActivationButton;
#endif
    }

    private void OnHideActivationButton()
    {
        NGUITools.SetActive(activateCodeButton.gameObject, false);
    }
    private void OnShowActivationButton()
    {
        NGUITools.SetActive(activateCodeButton.gameObject, true);
    }
    private void StartCheckingLicense()
    {
        for (int i = 0; i < 3; ++i)
        {
            if (ActiveManager.IsActiveOfflineOk(i+1) == true)
            {
                if (InAppPurchaseInGame.IsInitialized() == false)
                {
                    InAppPurchaseInGame.InitIAP();
                }
                if (ActivateChanged.instance != null)
                    ActivateChanged.instance.ActivatedChange();// ActivatedChange();
            }
            else
            {
                if (ActivateChanged.instance != null)
                    ActivateChanged.instance.NotActivatedChange();
            }
        }


    }
    #region EVENT_HANDLE

    private void OnPlayerServerActived()
    {
        //DialogManager.instance.HideLoadingBar();
        //DialogManager.PopUpDialog(Localization.Get("SuccessDialogTitle"), Localization.Get("RestoreSuccess"), DialogManager.DialogType.OkDialog, null, null);
        ActiveManager.SaveActiveState();
        if (ActivateChanged.instance != null)
            ActivateChanged.instance.ActivatedChange();
    }

    private void OnPlayerServerNotActived()
    {
        if (ActivateChanged.instance != null)
            ActivateChanged.instance.NotActivatedChange();
        //DialogManager.instance.HideLoadingBar();
        //DialogManager.PopUpDialog(Localization.Get("FailDialogTitle"), Localization.Get("RestoreFail"), DialogManager.DialogType.OkDialog, null, null);
    }
    private void OnRegisterLicenseFailure(string msg)
    {
        //GetDialog("Loading").SetActive(false);
        Debug.LogError("Fail");
        //DialogManager.instance.HideLoadingBar();
        if (msg == "1")
        {
            //DialogManager.PopUpDialog(Localization.Get("FailDialogTitle"), Localization.Get("IncorrectCode"), DialogManager.DialogType.OkDialog);
        }
        else if (msg == "2")
        {
            //DialogManager.instance.HideLoadingBar();
            //DialogManager.PopUpDialog(Localization.Get("FailDialogTitle"), Localization.Get("UsedCode"), DialogManager.DialogType.OkDialog);
            //Ma da duoc su dung

        }
        else
        {
            // Loi khong xac dinh
            //DialogManager.PopUpDialog(Localization.Get("FailDialogTitle"), Localization.Get("Error"), DialogManager.DialogType.OkDialog);
        }
    }

    private void OnRegisterLicenseSuccess()
    {
        Debug.LogError("Successfully");
        //DialogManager.instance.HideLoadingBar();
        //DialogManager.PopUpDialog(Localization.Get("SuccessDialogTitle"), Localization.Get("activesucceed"), DialogManager.DialogType.OkDialog, null, null);
        ActiveManager.SaveActiveState();
        if (ActivateChanged.instance != null)
            ActivateChanged.instance.ActivatedChange();
    }

    private void OnPurchaseSuccess()
    {
        Debug.Log("Handle purchase success here.");
        //DialogManager.instance.HideLoadingBar();
        ActiveManager.SaveActiveState();
        if (ActivateChanged.instance != null)
            ActivateChanged.instance.ActivatedChange();
        //DialogManager.PopUpDialog(Localization.Get("SuccessDialogTitle"), Localization.Get("activesucceed"), DialogManager.DialogType.OkDialog, null, null);
    }

    private void OnPurchaseFailed()
    {
        Debug.Log("Handle purchase failed here.");
        //DialogManager.instance.HideLoadingBar();
        if (ActivateChanged.instance != null)
            ActivateChanged.instance.NotActivatedChange();
        //DialogManager.PopUpDialog(Localization.Get("FailDialogTitle"), Localization.Get("BuyFail"), DialogManager.DialogType.OkDialog, null, null);
    }

    private void OnConnectionError()
    {
        //DialogManager.instance.HideLoadingBar();
        //DialogManager.PopUpDialog(Localization.Get("FailDialogTitle"), Localization.Get("NetworkError"), DialogManager.DialogType.OkDialog, null, null);
    }
    #endregion

    #region IN_APP_PURCHASE
    public void Buy()
    {
        //switch (ThemeController.themeSelected)
        //{
        //    case Theme.AnimalBook:
        //        InAppPurchaseInGame.SetUPIAP(1);
        //        InAppPurchaseInGame.BuyLicense();
        //        break;
        //    case Theme.SpaceBook:
        //        InAppPurchaseInGame.SetUPIAP(2);
        //        InAppPurchaseInGame.BuyLicense();
        //        //Debug.LogError("purchased space");
        //        break;
        //    case Theme.ColorBook:
        //        InAppPurchaseInGame.SetUPIAP(3);
        //        InAppPurchaseInGame.BuyLicense();
        //        //Debug.LogError("purchased color");
        //        break;
        //}
    }
    #endregion


    #region CODE_ACTIVATION 
    #region RESTORE_PHONE_NUMBER
    private void RestoreCodeUsingPhonenumber()
    {
        if (!ActivationManager.instance.IsValidPhoneNumberRestore())
        {
            //DialogManager.PopUpDialog(Localization.Get("FailDialogTitle"), Localization.Get("InvalidPhoneNumber"), DialogManager.DialogType.OkDialog, null, null);
        }
        else
        {
            //DialogManager.PopUpDialog(string.Empty, string.Empty, DialogManager.DialogType.LoadingBar);
            //switch (ThemeController.themeSelected)
            //{
            //    case Theme.AnimalBook:
            //        ActiveManager.Setup(1);
            //        break;
            //    case Theme.SpaceBook:
            //        ActiveManager.Setup(2);
            //        break;
            //    case Theme.ColorBook:
            //        ActiveManager.Setup(3);
            //        break;
            //    default: break;
            //}
            StartCoroutine(ActiveManager.CheckActiveOnServer(ActivationManager.instance.GetPhoneNumberRestore()));
        }

    }

    public void RestoreCode()
    {
        if (ActiveManager.IsNetworkConnectionOk())
        {
            RestoreCodeUsingPhonenumber();
        }
        else
        {
            //DialogManager.PopUpDialog(Localization.Get("FailDialogTitle"), Localization.Get("NetworkError"), DialogManager.DialogType.OkDialog, null, null);
        }
    }
    #endregion

    #region NEW_REGISTER 
    public void NewUser()
    {
        
        if (ActiveManager.IsNetworkConnectionOk())
        {
            
            if (ActivationManager.instance.IsValidCodeAndPhoneNumber(ChooseProjectForNewUser()))
            {
                Debug.LogError("Clicked");
                //DialogManager.PopUpDialog(string.Empty, string.Empty, DialogManager.DialogType.LoadingBar);
                StartCoroutine(ActiveManager.RegisterUser(ActivationManager.instance.GetPhoneNumber(), ActivationManager.instance.GetCode()));
            }
            else
            {
                //DialogManager.PopUpDialog(Localization.Get("FailDialogTitle"), Localization.Get("Invalid"), DialogManager.DialogType.OkDialog, null, null);
            }
        }
        else
        {
            //DialogManager.PopUpDialog(Localization.Get("FailDialogTitle"), Localization.Get("NetworkError"), DialogManager.DialogType.OkDialog, null, null);
        }
    }
    private int ChooseProjectForNewUser()
    {
        //switch (ThemeController.themeSelected)
        //{
        //    case Theme.AnimalBook:
        //        //Debug.LogError("animal selected");
        //        return 1;
        //    case Theme.SpaceBook:
        //        //Debug.LogError("space selected");
        //        return 2;
        //    case Theme.ColorBook:
        //        //Debug.LogError("color selected");
        //        return 3;
        //}
        return 1;
    }
    #endregion
    #endregion

    ////public GameObject hiddenObjectWhenActived
    //public GameObject downloadButtonAnimal;
    //public GameObject activeButtonAnimal;
    //public GameObject downloadButtonSpace;
    //public GameObject activeButtonSpace;
    //public GameObject downloadButtonColor;
    //public GameObject activeButtonColor;

    //public GameObject thumbnailAnimals;
    //public GameObject thumbnailSpaces;
    //public GameObject thumbnailColors;

    //public GameObject avtiveForm;
    //public void ActivatedChange()
    //{
    //    if (PlayerPrefs.GetString("Project_A").Equals("ACTIVED"))
    //    {
    //        NGUITools.SetActive(downloadButtonAnimal, true);
    //        NGUITools.SetActive(activeButtonAnimal, false);

    //        foreach (UIWidget i in thumbnailAnimals.GetComponentsInChildren<UIWidget>())
    //        {
    //            i.color = Color.white;
    //        }

    //        //Hide form when activated
    //        NGUITools.SetActiveSelf(avtiveForm, false);
    //    }
    //    if (PlayerPrefs.GetString("Project_B").Equals("ACTIVED"))
    //    {
    //        NGUITools.SetActive(downloadButtonSpace, true);
    //        NGUITools.SetActive(activeButtonSpace, false);


    //        foreach (UISprite i in thumbnailSpaces.GetComponentsInChildren<UISprite>())
    //        {
    //            i.color = Color.white;

    //        }
    //        //Hide form when activated
    //        NGUITools.SetActiveSelf(avtiveForm, false);
    //    }
    //    if (PlayerPrefs.GetString("Project_C").Equals("ACTIVED"))
    //    {
    //        NGUITools.SetActive(downloadButtonColor, true);
    //        NGUITools.SetActive(activeButtonColor, false);
    //        foreach (UISprite i in thumbnailColors.GetComponentsInChildren<UISprite>())
    //        {
    //            i.color = Color.white;
    //        }
    //        //Hide form when activated
    //        NGUITools.SetActiveSelf(avtiveForm, false);
    //    }
    //}

    //public void NotActivatedChange()
    //{
    //    if (!PlayerPrefs.GetString("Project_A").Equals("ACTIVED"))
    //    {
    //        NGUITools.SetActive(downloadButtonAnimal, false);
    //        NGUITools.SetActive(activeButtonAnimal, true);
    //        foreach (UISprite i in thumbnailColors.GetComponentsInChildren<UISprite>())
    //        {
    //            if (i.gameObject.tag != "FreeTrial")
    //            {
    //                i.color = Color.grey;
    //            }
    //        }

    //        if (ThemeController.themeSelected == Theme.AnimalBook)
    //        {
    //            //Show form when activated
    //            NGUITools.SetActiveSelf(avtiveForm, true);
    //        }

    //    }
    //    if (!PlayerPrefs.GetString("Project_B").Equals("ACTIVED"))
    //    {
    //        NGUITools.SetActive(downloadButtonSpace, false);
    //        NGUITools.SetActive(activeButtonSpace, true);
    //        foreach (UISprite i in thumbnailColors.GetComponentsInChildren<UISprite>())
    //        {
    //            if (i.gameObject.tag != "FreeTrial")
    //            {
    //                i.color = Color.grey;
    //            }
    //        }
    //        if (ThemeController.themeSelected == Theme.SpaceBook)
    //        {
    //            //Show form when activated
    //            NGUITools.SetActiveSelf(avtiveForm, true);
    //        }
    //    }
    //    if (!PlayerPrefs.GetString("Project_C").Equals("ACTIVED"))
    //    {
    //        NGUITools.SetActive(downloadButtonColor, false);
    //        NGUITools.SetActive(activeButtonColor, true);
    //        foreach (UISprite i in thumbnailColors.GetComponentsInChildren<UISprite>())
    //        {
    //            if (i.gameObject.tag != "FreeTrial")
    //            {
    //                i.color = Color.grey;
    //            }
    //        }
    //        if (ThemeController.themeSelected == Theme.ColorBook)
    //        {
    //            //Show form when activated
    //            NGUITools.SetActiveSelf(avtiveForm, true);
    //        }
    //    }
    //}
}
