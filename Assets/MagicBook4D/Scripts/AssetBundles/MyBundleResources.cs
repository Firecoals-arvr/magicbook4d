using Loxodon.Framework.Bundles;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Firecoals.AssetBundles
{
    public abstract class MyBundleResources: MonoBehaviour
    {
        protected IResources resources;
        protected virtual IResources GetResources()
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
            ILoaderBuilder builder = new CustomBundleLoaderBuilder(new Uri(BundleUtil.GetReadOnlyDirectory()), false);

            /* Create a BundleManager */
            IBundleManager manager = new BundleManager(manifest, builder);

            /* Create a BundleResources */
            this.resources = new BundleResources(pathInfoParser, manager);
            return this.resources;
        }

    }
}

