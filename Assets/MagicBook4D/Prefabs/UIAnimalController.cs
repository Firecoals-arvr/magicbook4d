using System.Collections;
using System.Collections.Generic;
using Firecoals.Animal;
using UnityEngine;
using  UnityEngine.SceneManagement;

namespace Firecoals.Animal
{
    public class UIAnimalController : MonoBehaviour
    {
        //public GameObject SoundName, Language;
        public GameObject SettingLanguage;
        public bool CheckSetActive;

        private void Start()
        {
            CheckSetActive = true;
        }
        public void SettingLanguageAnimal()
        {
             NGUITools.SetActive(SettingLanguage, true);
        }
        public void ExitAnimal()
        {
            SceneManager.LoadScene("Menu");
        }

        public void Environment()
        {
          
            if (CheckSetActive)
            {

                NGUITools.SetActive(GameObject.FindGameObjectWithTag("Environment"), true);
            }
            else
            {
                NGUITools.SetActive(GameObject.FindGameObjectWithTag("Environment"), false);
            }
            CheckSetActive = !CheckSetActive;
        }
    }
}
