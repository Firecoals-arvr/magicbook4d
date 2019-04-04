using System;
using UnityEngine;

public class LanguageController : MonoBehaviour
{
    public event Action<string, string> OnPickLanguage = delegate { };
    public event Action<string> OnShowListLanguage = delegate { };
    //public void PickLanguage(string languageId, string projectId)
    //{
    //    //Set language for project ID
    //}

    //public void ShowListLanguage(string projectId)
    //{

    //}

    protected virtual void OnOnPickLanguage(string languageId, string projectId)
    {
        OnPickLanguage(languageId, projectId);
    }

    protected virtual void OnOnShowListLanguage(string projectId)
    {
        OnShowListLanguage(projectId);
    }
}
