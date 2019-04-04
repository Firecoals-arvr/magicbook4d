using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;
public class ActiveManager
{
    private const int ID_ANIMAL = 1;
    private const int ID_SPACE = 2;
    private const int ID_COLOR = 3;

    private const string NAME_ANIMAL = "A";
    private const string NAME_SPACE = "B";
    private const string NAME_COLOR = "C";

    private static string _Url = "https://firecoalslisenceserver.herokuapp.com";
    //urlhttp://104.199.229.206
    //private static string _Port = "3122";
    private static string ModuleRegister = "/register";
    private static string Module_CheckActive = "/checkactive";
    private static string PROJECT_NAME = "";
    private static string PREFKEY = "Project_";
    private const string ACTIVED = "ACTIVED";
    private const string REGISTER_OK = "0";
    private const string PlayerID = "playerid";
    private const string ProjectName = "projectname";
    private const string License = "license";
    private static string Module_Activation = "/activation";

    public static event Action OnRegisterLicenseSuccess = delegate { };
    public static event Action<string> OnRegisterLicenseFailure = delegate { };
    public static event Action OnConnectionError = delegate { };
    public static event Action OnPlayerServerActived = delegate { };
    public static event Action OnPlayerServerNotActived = delegate { };
    public static event Action OnShowActivationButton = delegate { };
    public static event Action OnHideActivationButton = delegate { };
    public static bool isShowActivationButton = false;


    ///<summary>
    ///Setup something. call this before use.
    ///Project_Id: Animal = 1, Sapce = 2, Color = 3
    ///</summary>

    public static void Setup(int Project_Id)
    {
        switch (Project_Id)
        {
            case ID_ANIMAL:
                PROJECT_NAME = NAME_ANIMAL;
                break;
            case ID_SPACE:
                PROJECT_NAME = NAME_SPACE;
                break;
            case ID_COLOR:
                PROJECT_NAME = NAME_COLOR;
                break;
        }

        PREFKEY = "Project_" + PROJECT_NAME;

    }


    //Check network connection
    public static bool IsNetworkConnectionOk()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            return false;
        }
        return true;
    }

    //Check current user actived offline or not
    public static bool IsActiveOfflineOk(int projectID)
    {
        Setup(projectID);
        if (PlayerPrefs.GetString(PREFKEY).Equals(ACTIVED))
        {
            return true;
        }
        return false;
    }

    // Save Active offline
    public static void SaveActiveState()
    {
        PlayerPrefs.SetString(PREFKEY, ACTIVED);
    }

    //Check current user active on server or not
    public static IEnumerator CheckActiveOnServer(string playerid)
    {
        WWWForm form = new WWWForm();
        form.AddField(ProjectName, PROJECT_NAME);
        form.AddField(PlayerID, playerid);
        UnityWebRequest www = UnityWebRequest.Post(_Url + Module_CheckActive, form);
        yield return www.Send();
        if (www.isNetworkError)
        {
            OnConnectionError();
        }
        else
        {
            string result = www.downloadHandler.text;
            Debug.LogWarning("result: " + result);
            if (result.Equals(ACTIVED))
            {
                OnPlayerServerActived();
            }
            else
            {
                OnPlayerServerNotActived();
            }
        }
    }

    //register new user
    public static IEnumerator RegisterUser(string playerid, string license)
    {
        WWWForm form = new WWWForm();
        form.AddField(ProjectName, PROJECT_NAME);
        form.AddField(PlayerID, playerid);
        form.AddField(License, license);
        UnityWebRequest www = UnityWebRequest.Post(_Url + ModuleRegister, form);
        yield return www.Send();
        if (www.isNetworkError)
        {
            OnConnectionError();
        }
        else
        {
            string result = www.downloadHandler.text;
            if (result.Equals(REGISTER_OK))
            {
                OnRegisterLicenseSuccess();
            }
            else
            {
                OnRegisterLicenseFailure(result);
            }

        }
    }

    //Check to ON/OFF activation button
    public static IEnumerator CheckActivation()
    {
        WWWForm form = new WWWForm();
        form.AddField("projectname", PROJECT_NAME);
        UnityWebRequest www = UnityWebRequest.Post(_Url + Module_Activation, form);
        yield return www.Send();
        if (www.isNetworkError)
        {
            OnConnectionError();
        }
        else
        {
            //off/on
            string result = www.downloadHandler.text;
            //			Debug.Log (result);
            if (result.Equals("on"))
            {
                OnShowActivationButton();
            }
            else if (result.Equals("off"))
            {
                OnHideActivationButton();
            }
        }
    }

}
