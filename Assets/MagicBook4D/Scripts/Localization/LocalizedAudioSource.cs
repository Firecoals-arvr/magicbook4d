using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizedAudioSource : MonoBehaviour
{
    public LeanLocalization Target;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateNewPhrase(string languageName, string phraseName)
    {
        var target = Target;

        if (target == null && LeanLocalization.AllLocalizations.Count > 0)
        {
            target = LeanLocalization.AllLocalizations[0];
        }

        // Create a new LeanLocalization?
        if (target == null)
        {
            target = new GameObject("LeanLocalization").AddComponent<LeanLocalization>();
        }

        bool updateLanguage = false;

        var translation = target.AddTranslation(languageName, phraseName);
    }
}
