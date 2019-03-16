using Loxodon.Framework.Asynchronous;
using Loxodon.Framework.Execution;
using Loxodon.Log;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Firecoals.AssetBundles.Sound
{
    public class SoundManifestLoader : ISoundManifestLoader
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(SoundManifestLoader));

#if UNITY_ANDROID && !UNITY_EDITOR
        public string GetCompressedFileName(string url)
        {
            url = Regex.Replace(url, @"^jar:file:///", "");
            return url.Substring(0, url.LastIndexOf("!"));
        }

        public string GetCompressedEntryName(string url)
        {
            return url.Substring(url.LastIndexOf("!") + 1);
        }
#endif
        public virtual SoundManifest LoadSync(string path)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            if (Regex.IsMatch(path, @"(jar:file:///)|(\.jar)|(\.apk)|(\.obb)|(\.zip)", RegexOptions.IgnoreCase))
            {
                using (ZipFile zip = new ZipFile(this.GetCompressedFileName(path)))
                {
                    string entryName = this.GetCompressedEntryName(path);
                    if (!zip.ContainsEntry(entryName))
                    {
                        if(log.IsErrorEnabled)
                            log.ErrorFormat("Not found the SoundManifest '{0}'.", path);
                        return null;
                    }

                    ZipEntry entry = zip[entryName];
                    byte[] buffer = new byte[entry.UncompressedSize];
                    using (Stream input = entry.OpenReader())
                    {
                        input.Read(buffer, 0, buffer.Length);
                    }
                    return SoundManifest.Parse(Encoding.UTF8.GetString(buffer));
                }
            }
            return SoundManifest.Parse(File.ReadAllText(path, Encoding.UTF8));
#elif UNITY_WEBGL && !UNITY_EDITOR
            throw new NotSupportedException("Because WebGL is single-threaded, this method is not supported,please use LoadAsync instead.");
#else
            return SoundManifest.Parse(File.ReadAllText(path, Encoding.UTF8));
#endif
        }
        public virtual IAsyncResult<SoundManifest> LoadAsync(string path)
        {
            AsyncResult<SoundManifest> result = new AsyncResult<SoundManifest>();
            Executors.RunOnCoroutine(DoLoadAsync(result, path), result);
            return result;
        }
        protected virtual IEnumerator DoLoadAsync(IPromise<SoundManifest> promise, string path)
        {
            string absoluteUri = "";
            try
            {
                Uri uri = new Uri(path);
                absoluteUri = uri.AbsoluteUri;
                if (uri.Scheme.Equals("jar") && !absoluteUri.StartsWith("jar:file://"))
                    absoluteUri = absoluteUri.Replace("jar:file:", "jar:file://");
            }
            catch (Exception)
            {
                absoluteUri = path;
            }
#if UNITY_2017_1_OR_NEWER
            using (UnityWebRequest www = new UnityWebRequest(absoluteUri))
            {
                www.downloadHandler = new DownloadHandlerBuffer();
#if UNITY_2018_1_OR_NEWER
                www.SendWebRequest();
#else
                www.Send();
#endif
                while (!www.isDone)
                {
                    yield return null;
                }

                if (!string.IsNullOrEmpty(www.error))
                {
                    promise.SetException(new Exception(string.Format("Failed to load the Sound Manifest at the address '{0}'.Error:{1}", absoluteUri, www.error)));
                    yield break;
                }

                try
                {
                    string json = www.downloadHandler.text;
                    SoundManifest manifest = SoundManifest.Parse(json);
                    promise.SetResult(manifest);
                }
                catch (Exception e)
                {
                    promise.SetException(e);
                }
            }
#else
            using (WWW www = new WWW(absoluteUri))
            {
                yield return www;

                if (!string.IsNullOrEmpty(www.error))
                {
                    promise.SetException(new Exception(string.Format("Failed to load the Sound Manifest at the address '{0}'.Error:{1}", absoluteUri, www.error)));
                    yield break;
                }

                try
                {
                    string json = www.text;
                    SoundManifest manifest = SoundManifest.Parse(json);
                    promise.SetResult(manifest);
                }
                catch (Exception e)
                {
                    promise.SetException(e);
                }
            }
#endif
        }
    }
}

