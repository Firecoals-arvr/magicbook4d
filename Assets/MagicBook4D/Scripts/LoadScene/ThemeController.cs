﻿namespace Firecoals.MagicBook
{
    using UnityEngine;
    /// <summary>
    /// Control the book theme for users to be used
    /// </summary>
    public class ThemeController : MonoBehaviour
    {
        private GameObject menuGrid;

        public string Theme { get; private set; }
        public static ThemeController instance;

        private ThemeController(string themeName)
        {
            instance.Theme = themeName;
        }

        public static ThemeController Animal => new ThemeController("Animal");
        public static ThemeController Color => new ThemeController("Color");
        public static ThemeController Space => new ThemeController("Space");

        private void Start()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(this);
            AddThemeEvent();
            //PlayerPrefs.DeleteAll();
            //Debug.LogWarning("Deleted all player preference key");
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                StartCoroutine(ActiveManager.AssetBundleVersion());
            }
        }

        /// <summary>
        /// Set theme when click menu to load the scene "Loading"
        /// </summary>
        /// <param name="themeName"></param>
        private void SetTheme(string themeName)
        {
            instance.Theme = themeName;
//#if UNITY_EDITOR && UNITY_IOS
            LoadingScreen.Run();
//#elif UNITY_ANDROID
//            if (themeName == "Color")
//            {
//                Debug.Log("ClickColor");
//                Application.OpenURL("https://play.google.com/store/apps/details?id=com.firecoals.magiccolor&hl=vi");
//            }
//            else
//                LoadingScreen.Run();
            Debug.Log("<color=blue>Current Theme Name = " + themeName + "</color>");
//#endif
        }
        /// <summary>
        /// Add event to all button under grid
        /// </summary>
        private void AddThemeEvent()
        {
            var buttons = menuGrid.GetComponentsInChildren<UIButton>();
            foreach (var button in buttons)
            {
                EventDelegate eventDelegate = new EventDelegate(this, "SetTheme");
                eventDelegate.parameters[0].value = button.name;
                EventDelegate.Set(button.onClick, eventDelegate);

            }
        }
        private void OnEnable()
        {
            menuGrid = GameObject.FindGameObjectWithTag("Menu_Grid");
            if (menuGrid)
            {
                AddThemeEvent();
            }
        }

    }

}