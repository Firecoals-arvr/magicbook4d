//-------------------------------------------------
//            Magic Color
// Copyright © 2016-2018 Firecoals.,JSC
//-------------------------------------------------

using UnityEngine;

public class ActivateChanged : MonoBehaviour {
    public static ActivateChanged instance;
    private static bool created = false;
	private void Awake () {
        if (instance == null)
        {
            instance = this;
        }
        //if (!created)
        //{
        //    DontDestroyOnLoad(this.gameObject);
        //}
        //else
        //{
        //    DestroyImmediate(this.gameObject);
        //}
        ActivatedChange();
        NotActivatedChange();

    }

    public GameObject downloadButtonAnimal;
    public GameObject activeButtonAnimal;
    public GameObject downloadButtonSpace;
    public GameObject activeButtonSpace;
    public GameObject downloadButtonColor;
    public GameObject activeButtonColor;

    public GameObject thumbnailAnimals;
    public GameObject thumbnailSpaces;
    public GameObject thumbnailColors;

    public GameObject avtiveForm;
    public void ActivatedChange()
    {
        if (PlayerPrefs.GetString("Project_A").Equals("ACTIVED"))
        {
            NGUITools.SetActive(downloadButtonAnimal, true);
            NGUITools.SetActive(activeButtonAnimal, false);

            foreach (UIWidget i in thumbnailAnimals.GetComponentsInChildren<UIWidget>())
            {
                i.color = Color.white;
            }

            //Hide form when activated
            NGUITools.SetActiveSelf(avtiveForm, false);
        }
        if (PlayerPrefs.GetString("Project_B").Equals("ACTIVED"))
        {
            NGUITools.SetActive(downloadButtonSpace, true);
            NGUITools.SetActive(activeButtonSpace, false);
            foreach (UISprite i in thumbnailSpaces.GetComponentsInChildren<UISprite>())
            {
                i.color = Color.white;
            }
            //Hide form when activated
            NGUITools.SetActiveSelf(avtiveForm, false);
        }
        if (PlayerPrefs.GetString("Project_C").Equals("ACTIVED"))
        {
            NGUITools.SetActive(downloadButtonColor, true);
            NGUITools.SetActive(activeButtonColor, false);
            foreach (UISprite i in thumbnailColors.GetComponentsInChildren<UISprite>())
            {
                i.color = Color.white;
            }
            //Hide form when activated
            NGUITools.SetActiveSelf(avtiveForm, false);
        }
    }

    public void NotActivatedChange()
    {
        if (!PlayerPrefs.GetString("Project_A").Equals("ACTIVED"))
        {
            NGUITools.SetActive(downloadButtonAnimal, false);
            NGUITools.SetActive(activeButtonAnimal, true);
            foreach (UISprite i in thumbnailAnimals.GetComponentsInChildren<UISprite>())
            {
                if (i.gameObject.tag != "FreeTrial")
                {
                    i.color = Color.grey;
                    //Debug.LogError(i.gameObject + "turn grey");
                }
            }

            //if (ThemeController.themeSelected == Theme.AnimalBook)
            //{
            //    //Show form when activated
            //    NGUITools.SetActiveSelf(avtiveForm, true);
            //}

        }
        if (!PlayerPrefs.GetString("Project_B").Equals("ACTIVED"))
        {
            NGUITools.SetActive(downloadButtonSpace, false);
            NGUITools.SetActive(activeButtonSpace, true);
            UISprite[] spriteArray =  thumbnailSpaces.GetComponentsInChildren<UISprite>();
            //i[0].color = Color.grey;
            //i[7].color = Color.grey;
            //i[13].color = Color.grey;
            //foreach (UISprite i in spriteArray)
            //{
            //    if(!spriteArray[0] || !spriteArray[7] || !spriteArray[13])
            //        i.color = Color.gray;
            //}
            for (int i=0; i<spriteArray.Length;++i)
            {
                if(i!=0 || i!=7 || i != 13)
                {
                    spriteArray[i].color = Color.grey;
                }
            }
            spriteArray[0].color = Color.white;
            spriteArray[7].color = Color.white;
            spriteArray[13].color = Color.white;
            //if (ThemeController.themeSelected == Theme.SpaceBook)
            //{
            //    //Show form when activated
            //    NGUITools.SetActiveSelf(avtiveForm, true);
            //}
        }
        if (!PlayerPrefs.GetString("Project_C").Equals("ACTIVED"))
        {
            NGUITools.SetActive(downloadButtonColor, false);
            NGUITools.SetActive(activeButtonColor, true);
            foreach (UISprite i in thumbnailColors.GetComponentsInChildren<UISprite>())
            {
                if (i.gameObject.tag != "FreeTrial")
                {
                    i.color = Color.grey;
                }
            }
            //if (ThemeController.themeSelected == Theme.ColorBook)
            //{
            //    //Show form when activated
            //    NGUITools.SetActiveSelf(avtiveForm, true);
            //}
        }
    }
}
