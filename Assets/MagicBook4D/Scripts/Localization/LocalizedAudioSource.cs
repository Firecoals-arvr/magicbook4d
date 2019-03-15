using Firecoals.AssetBundles;
using Lean.Localization;
using Loxodon.Framework.Bundles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;

public class LocalizedAudioSource : MonoBehaviour
{
	public LeanLocalization Target;
	public AudioSource audioSource;
	public Button vn, en; 

	private DownLoadAssetBundles downLoad;
	private static string currentLanguage = "English";

	// Start is called before the first frame update
	void Start()
	{
		BundleSetting bundleSetting = new BundleSetting("Audio/bundles");
		downLoad = new DownLoadAssetBundles(DownLoadAssetBundles.GetDataUrl("Audio"));
		Download();
	}

	public void Download()
	{
		StartCoroutine(downLoad.Download());
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
		string url = CheckForLoadAsset(languageName);
		Object bear = downLoad.LoadAssetObject(url);
		translation.Object = bear;

		if (LeanLocalization.CurrentLanguage == languageName)
		{
			updateLanguage = true;
		}

		if (updateLanguage == true)
		{
			LeanLocalization.UpdateTranslations();
		}
	}

	public string CheckForLoadAsset(string languageName)
	{
		switch (languageName)
		{
			case "English" :
				return "Animal/Sound/Animal_Info/InfoEN/enInfoBear.ogg";
			case "Vietnamese":
				return "Animal/Sound/Animal_Info/InfoVN/vnInfoBear.ogg";
			default :
				return "Animal/Sound/Animal_Info/InfoEN/enInfoBear.ogg";
		}
	}

	public void OnClickButton()
	{
		CreateNewPhrase(currentLanguage, "BearInfo");
		audioSource.Play();
	}

	public void SetCurrentLanguageToVN()
	{
		currentLanguage = "Vietnamese";
	}

	public void SetCurrentLanguageToEN()
	{
		currentLanguage = "English";
	}

	private void OnTrackingFoundEvent(int index)
	{

	}

	private void OnTrackingLostEvent()
	{

	}
}
