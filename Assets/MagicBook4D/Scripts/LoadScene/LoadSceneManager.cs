using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    public static LoadSceneManager instance;
    public string currentScene { get; set; }
    public string previousScene { get; set; }
    //private void Start()
    //{
    //    if (instance != this)
    //    {
    //        instance = this;
    //    }
    //    else
    //    {
    //        DontDestroyOnLoad(this.gameObject);
    //    }
    //}

    public void LoadSettingScene()
    {
        SceneManager.LoadScene("Settings");
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadInfoScene()
    {
        SceneManager.LoadScene("Info");
    }

    public void LoadActivateScene()
    {
        SceneManager.LoadScene("Activate");
    }

    public void LoadStoreScene()
    {
        SceneManager.LoadScene("Store");
    }
       
}
