﻿using Loxodon.Framework.Asynchronous;
using Loxodon.Framework.Bundles;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.AssetBundles
{
    public interface IDownloader
    {
        Uri BaseUri { get; set; }

        int MaxTaskCount { get; set; }

        /// <summary>
        /// Get a list of files that need to be downloaded. All file is defined in manifest
        /// </summary>
        /// <param name="manifest"></param>
        /// <returns></returns>
        IProgressResult<float, List<BundleInfo>> GetDownloadList(BundleManifest manifest);
        /// <summary>
        /// Get a custom list by bundle name that to be downloaded
        /// </summary>
        /// <param name="manifest"></param>
        /// <param name="bundleNames"></param>
        /// <returns></returns>
        IProgressResult<float, List<BundleInfo>> GetDownloadList(BundleManifest manifest, string[] bundleNames);
        /// <summary>
        /// Download the BundleManifest.Store address: BundleUtil.GetStorableDirectory() + bundles.dat,bak file: BundleUtil.GetStorableDirectory() + bundles.dat.bak
        /// </summary>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        IProgressResult<Progress, BundleManifest> DownloadManifest(string relativePath);

        /// <summary>
        /// Download the Assetbundle.Store address:BundleUtil.GetStorableDirectory(),default:Application.persistentDataPath + "/bundles/"
        /// </summary>
        /// <param name="bundles"></param>
        /// <returns></returns>
        IProgressResult<Progress, bool> DownloadBundles(List<BundleInfo> bundles);
    }
}


