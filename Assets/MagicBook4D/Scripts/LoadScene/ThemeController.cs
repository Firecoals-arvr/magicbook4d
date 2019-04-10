using UnityEngine;

public class ThemeController : MonoBehaviour
{
    [Tooltip("The grid contain UIButton theme")]
    public UIGrid gridGameObject;
    public string Theme { get; set; }
    public static ThemeController instance = null;


    private ThemeController(string themeName)
    {
        Theme = themeName;
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
        PlayerPrefs.DeleteAll();
        Debug.LogWarning("Deleted all player preference key");
    }

    private void SetTheme(string themeName)
    {
        Theme = themeName;
        Debug.Log(Theme);
        LoadingScreen.Run();
    }
    private void AddThemeEvent()
    {
        var buttons = gridGameObject.gameObject.GetComponentsInChildren<UIButton>();
        foreach (var button in buttons)
        {
            EventDelegate eventDelegate = new EventDelegate(this, "SetTheme");
            eventDelegate.parameters[0].value = button.name;
            EventDelegate.Set(button.onClick, eventDelegate);
        }
    }

}
