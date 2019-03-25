using Firecoals.Augmentation;
using UnityEngine;

public class AssetLoader : MonoBehaviour
{
    public string[] bundleNames;

    // Start is called before the first frame update
    public string bundleRoot;

    private void Awake()
    {
        StartCoroutine(AssetHandler.PreLoad(bundleRoot, bundleNames));
    }
}