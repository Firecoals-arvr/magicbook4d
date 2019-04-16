using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
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
