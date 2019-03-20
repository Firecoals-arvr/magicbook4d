using UnityEngine;
using Firecoals.Augmentation;
public class AssetLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public string bundleRoot;
    public string[] bundleNames;
    void Awake()
    {
        StartCoroutine(AssetHandler.PreLoad(bundleRoot, bundleNames));
    }


}
