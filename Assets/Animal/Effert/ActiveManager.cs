using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;
//using GooglePlayGames;

public class ActiveManager
{
    private const string PROJECT_NAME = "A";
    private static string _Url = "https://firecoalslisenceserver.herokuapp.com";
    //urlhttp://104.199.229.206
    //private static string _Port = "3122";
    private static string ModuleRegister = "/register";
    private static string Module_CheckActive = "/checkactive";
    private const string PREFKEY = "Project_" + PROJECT_NAME;
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
    public static bool isShowActivationButton = false;

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
    public static bool IsActiveOfflineOk()
    {
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
        if (www.isError)
        {
            OnConnectionError();
        }
        else
        {
            string result = www.downloadHandler.text;
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
        if (www.isError)
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
        if (www.isError)
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
        }
    }

}
