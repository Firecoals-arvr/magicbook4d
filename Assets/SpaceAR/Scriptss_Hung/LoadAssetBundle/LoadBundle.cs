using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firecoals.Augmentation;
namespace Firecoals.Space
{
    public class LoadBundle : MonoBehaviour
    {
        public string bundleRoot;
        public string[] bundleNames;
        private void Awake()
        {
            StartCoroutine(AssetHandler.PreLoad(bundleRoot, bundleNames));
        }
    }
}