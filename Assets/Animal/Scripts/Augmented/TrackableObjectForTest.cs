using Firecoals.Augmentation;
using Loxodon.Framework.Bundles;
using Loxodon.Framework.Contexts;
using UnityEngine;

public class TrackableObjectForTest : DefaultTrackableEventHandler
{
    private IResources _resources;
    protected override void Start()
    {
        base.Start();
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
        GameObject go = _resources.LoadAsset<GameObject>("Animal/GetPreFab/Bear.prefab") as GameObject;
        if(go)
            GameObject.Instantiate(go, mTrackableBehaviour.transform);
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
    }
}
