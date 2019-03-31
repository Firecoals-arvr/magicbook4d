using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownLoadManager : MonoBehaviour
{
    private ThemeController _theme;
    private void Start()
    {
        _theme = FindObjectOfType<ThemeController>();
        //Debug.LogWarning();
    }
}
