using Loxodon.Framework.Bundles;
using Loxodon.Framework.Contexts;

namespace Firecoals.Augmentation
{
    using UnityEngine;

    public class UnregisterIResource : MonoBehaviour
    {
        private void Start()
        {
            ApplicationContext context = Context.GetApplicationContext();
            context.GetContainer().Unregister<IResources>();
            Debug.Log("<color=green>Unregistered</color>");
        }
    }

}