using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DowloadsImages : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private string Link;
    public void Downloads()
    {
        Application.OpenURL(Link);
    }
}
