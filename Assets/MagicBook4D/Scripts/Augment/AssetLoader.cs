using System;
using System.Collections;
using Firecoals.AssetBundles;
using Loxodon.Framework.Bundles;
using Loxodon.Framework.Contexts;
using UnityEngine;

namespace Firecoals.Augmentation
{
    public class AssetLoader : MonoBehaviour
    {
        public string[] bundleNames;

        // Start is called before the first frame update
        public string bundleRoot;
        public AssetBundlesLoader assetBundlesLoader = new AssetBundlesLoader();
        public IResources Resources { private set; get; }
        //private string iv = "5Hh2390dQlVh0AqC";
        //private string key = "E4YZgiGQ0aqe5LEJ";
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            BundleSetting setting = new BundleSetting(bundleRoot);
            this.Resources = CreateResources();

            ApplicationContext context = Context.GetApplicationContext();
            context.GetContainer().Register<IResources>(this.Resources);
        }

        private void Start()
        {
            StartCoroutine(PreLoad());
        }

        private IEnumerator PreLoad()
        {
            BundleSetting bundleSettings = new BundleSetting(bundleRoot);
            /*Preload asset bundle*/

            yield return assetBundlesLoader.Preload(bundleNames, 1);
        }
        private IResources CreateResources()
        {
            IResources resources = null;
#if UNITY_EDITOR
            if (SimulationSetting.IsSimulationMode)
            {
                Debug.Log("Use SimulationResources. Run In Editor");

                /* Create a PathInfoParser. */
                //IPathInfoParser pathInfoParser = new SimplePathInfoParser("@");
                IPathInfoParser pathInfoParser = new SimulationAutoMappingPathInfoParser();

                /* Create a BundleManager */
                IBundleManager manager = new SimulationBundleManager();

                /* Create a BundleResources */
                resources = new SimulationResources(pathInfoParser, manager);
            }
            else
#endif
            {
                /* Create a BundleManifestLoader. */
                IBundleManifestLoader manifestLoader = new BundleManifestLoader();

                /* Loads BundleManifest. */
                BundleManifest manifest = manifestLoader.Load(BundleUtil.GetStorableDirectory() + BundleSetting.ManifestFilename);

                //manifest.ActiveVariants = new string[] { "", "sd" };
                //manifest.ActiveVariants = new string[] { "", "hd" };

                /* Create a PathInfoParser. */
                //IPathInfoParser pathInfoParser = new SimplePathInfoParser("@");
                IPathInfoParser pathInfoParser = new AutoMappingPathInfoParser(manifest);

                /* Create a BundleLoaderBuilder */
                //ILoaderBuilder builder = new WWWBundleLoaderBuilder(new Uri(BundleUtil.GetReadOnlyDirectory()), false);

                /* AES128_CBC_PKCS7 */
                //RijndaelCryptograph rijndaelCryptograph = new RijndaelCryptograph(128, Encoding.ASCII.GetBytes(this.key), Encoding.ASCII.GetBytes(this.iv));

                /* Use a custom BundleLoaderBuilder */
                ILoaderBuilder builder = new CustomBundleLoaderBuilder(new Uri(BundleUtil.GetStorableDirectory()), false);

                /* Create a BundleManager */
                IBundleManager manager = new BundleManager(manifest, builder);

                /* Create a BundleResources */
                resources = new BundleResources(pathInfoParser, manager);
            }
            return resources;
        }
    }

}
