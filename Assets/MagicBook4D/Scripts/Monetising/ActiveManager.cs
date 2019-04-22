using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ActiveManager
{
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

    public static event Action<string> OnRegisterLicenseSuccess = delegate { };
    public static event Action<string> OnRegisterLicenseFailure = delegate { };
    public static event Action OnConnectionError = delegate { };
    public static event Action<string> OnPlayerServerActived = delegate { };
    public static event Action OnPlayerServerNotActived = delegate { };
    public static event Action OnShowActivationButton = delegate { };
    public static event Action OnHideActivationButton = delegate { };
    public static bool isShowActivationButton = false;

    public static Dictionary<string, string> activeStatus;
    public static string cloudBundleVersion;
    ///<summary>
    ///Setup something. call this before use.
    ///Project_Id: Animal = 1, Space = 2, Color = 3
    ///</summary>

    public static void Setup(int Project_Id)
    {
        switch (Project_Id)
        {
            case 1:
                PROJECT_NAME = "A";
                break;
            case 2:
                PROJECT_NAME = "B";
                break;
            case 3:
                PROJECT_NAME = "C";
                break;
        }

        PREFKEY = "Project_" + PROJECT_NAME;

    }

    public static IEnumerator CheckRestore(string playerId, string projectId)
    {
        var jobsToQueue = new List<CM_Job>()
        {
            CM_Job.Make(CheckActiveOnServer(playerId, projectId))
        };
        CM_JobQueue.Global.Enqueue(jobsToQueue).Start();
        yield return null;
    }

    //Check current user actived offline or not
    public static bool IsActiveOfflineOk(string projectId)
    {
        return PlayerPrefs.GetString("Project_" + projectId).Equals(ACTIVED);
    }

    // Save Active offline
    //public static void SaveActiveState()
    //{
    //    PlayerPrefs.SetString(PREFKEY, ACTIVED);
    //}

    public static void SaveActivatedStatus(string projectId)
    {
        PlayerPrefs.SetString("Project_" + projectId, ACTIVED);
    }
    //Check current user active on server or not
    public static IEnumerator CheckActiveOnServer(string playerid, string projectID)
    {
        activeStatus = new Dictionary<string, string>();
        WWWForm form = new WWWForm();
        form.AddField(ProjectName, projectID);
        form.AddField(PlayerID, playerid);
        UnityWebRequest www = UnityWebRequest.Post(_Url + Module_CheckActive, form);
        yield return www.SendWebRequest();
        if (www.isNetworkError)
        {
            OnConnectionError();
        }
        else
        {
            string result = www.downloadHandler.text;
            while (!www.isDone)
            {
                yield return null;
            }
            Debug.LogWarning("result: " + result);
            Debug.LogWarning("Phone number " + playerid + " and Project " + projectID + " is " + result);
            
            if (result.Equals(ACTIVED))
            {
                SaveActivatedStatus(projectID);
                OnPlayerServerActived(projectID);
            }
            else
            {
                OnPlayerServerNotActived();
            }
            yield return new WaitForSecondsRealtime(3f);
        }

        yield return null;
    }

    public static string ProjectIdToName(string projectId)
    {
        switch (projectId)
        {
            case "A":
                return "Animal";
            case "B":
                return "Space";
            case "C":
                return "Color";
            default:
                return string.Empty;

        }
    }
    //register new user
    public static IEnumerator RegisterUser(string playerid, string projectId, string license)
    {
        WWWForm form = new WWWForm();
        form.AddField(ProjectName, projectId);
        form.AddField(PlayerID, playerid);
        form.AddField(License, license);
        UnityWebRequest www = UnityWebRequest.Post(_Url + ModuleRegister, form);
        //https://firecoalslisenceserver.herokuapp.com/register
        yield return www.SendWebRequest();
        if (www.isNetworkError)
        {
            OnConnectionError();
        }
        else
        {
            string result = www.downloadHandler.text;
            if (result.Equals(REGISTER_OK))
            {
                OnRegisterLicenseSuccess(projectId);
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
        yield return www.SendWebRequest();
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

    public static IEnumerator AssetBundleVersion()
    {
        UnityWebRequest www = UnityWebRequest.Post("https://firecoalslisenceserver.herokuapp.com/getbundlename", "");
        yield return www.SendWebRequest();
        string result = www.downloadHandler.text;
        cloudBundleVersion = result;
    }
}


