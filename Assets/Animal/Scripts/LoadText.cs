﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadTextAnimal()
    {
        Localization.loadFunction("Animal");
        Localization.language = "EN";
    }
}
