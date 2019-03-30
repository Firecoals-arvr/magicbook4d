using Loxodon.Framework.Bundles;
using UnityEngine;
using System;
using System.Text;

namespace Firecoals.AssetBundles
{
    public class MyBundleResources : IBundleResource
    {
        protected IResources resources;
        private string iv = "5Hh2390dQlVh0AqC";
        private string key = "E4YZgiGQ0aqe5LEJ";
        public virtual IResources GetResources()
        {
            if (this.resources != null)
                return this.resources;
            /* Create a BundleManifestLoader. */
            IBundleManifestLoader manifestLoader = new BundleManifestLoader();
            /* Loads BundleManifest. */
            BundleManifest manifest = manifestLoader.Load(BundleUtil.GetStorableDirectory() + BundleSetting.ManifestFilename);
            /* Create a PathInfoParser. */
            IPathInfoParser pathInfoParser = new AutoMappingPathInfoParser(manifest);
            /* Use a custom BundleLoaderBuilder */
            ILoaderBuilder builder = new CustomBundleLoaderBuilder(new Uri(BundleUtil.GetStorableDirectory()), false);
            /* Create a BundleManager */
            IBundleManager manager = new BundleManager(manifest, builder);
            /* Create a BundleResources */
            this.resources = new BundleResources(pathInfoParser, manager);
            return this.resources;
        }

        IResources CreateResources()
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
                BundleManifest manifest = manifestLoader.Load(BundleUtil.GetReadOnlyDirectory() + BundleSetting.ManifestFilename);
                /* Create a PathInfoParser. */
                //IPathInfoParser pathInfoParser = new SimplePathInfoParser("@");
                IPathInfoParser pathInfoParser = new AutoMappingPathInfoParser(manifest);

                /* Create a BundleLoaderBuilder */
                //ILoaderBuilder builder = new WWWBundleLoaderBuilder(new Uri(BundleUtil.GetReadOnlyDirectory()), false);

                /* AES128_CBC_PKCS7 */
                RijndaelCryptograph rijndaelCryptograph = new RijndaelCryptograph(128, Encoding.ASCII.GetBytes(this.key), Encoding.ASCII.GetBytes(this.iv));

                /* Use a custom BundleLoaderBuilder */
                ILoaderBuilder builder = new CustomBundleLoaderBuilder(new Uri(BundleUtil.GetReadOnlyDirectory()), false, rijndaelCryptograph);

                /* Create a BundleManager */
                IBundleManager manager = new BundleManager(manifest, builder);

                /* Create a BundleResources */
                resources = new BundleResources(pathInfoParser, manager);
            }
            return resources;
        }
    }
}

