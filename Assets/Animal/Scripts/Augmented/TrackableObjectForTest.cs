using System.Collections;
using System.Collections.Generic;
using Firecoals.AssetBundles;
using Firecoals.Augmentation;
using Loxodon.Framework.Bundles;
using Loxodon.Framework.Contexts;
using UnityEngine;

public class TrackableObjectForTest : DefaultTrackableEventHandler
{
    // khai báo biến Iresource
    private IResources _resources;
    private AssetHandler assetHandler;
    private AssetLoader assetLoader;
    protected override void Start()
    {
        base.Start();
        // làm giống bên dưới 
        ApplicationContext context = Context.GetApplicationContext();
        this._resources = context.GetService<IResources>();
        //assetHandler = new AssetHandler(mTrackableBehaviour.transform);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        //GameObject go = assetHandler.CreateUnique("animals/model/bear", "Assets/Animal/GetPreFab/Bear.prefab");
        // sinh game object 
        // note : bỏ assets/ nếu ko sẽ bị lỗi 
        GameObject go = _resources.LoadAsset<GameObject>("Assets/Animal/GetPreFab/Bear.prefab") as GameObject;
        if(go)
            GameObject.Instantiate(go, mTrackableBehaviour.transform);
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
    }
}
