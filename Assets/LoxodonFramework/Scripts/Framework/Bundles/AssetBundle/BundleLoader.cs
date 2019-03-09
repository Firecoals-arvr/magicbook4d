﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Loxodon.Framework.Asynchronous;
using Loxodon.Framework.Execution;
//using Loxodon.Log;

namespace Loxodon.Framework.Bundles
{
    public abstract class BundleLoader : IBundle
    {
        //private static readonly ILog log = LogManager.GetLogger(typeof(BundleLoader));

        private const string ASSETS = "Assets/";
        private const float TIME_PROGRESS_WEIGHT = 0.2f;

        private Dictionary<string, AssetBundleRequest> requestCache;
        private BundleInfo bundleInfo;
        private List<BundleLoader> dependencies;
        private List<BundleLoader> allDependencies;

        private System.Uri uri;
        private int refCount = 0;
        private object _lock = new object();
        private int priority = int.MinValue;
        private float startTime = 0f;
        private AssetBundle assetBundle;
        private IProgressResult<float, IBundle> result;
        private ProgressResult<float, AssetBundle> loadResult;
        private ITaskExecutor executor;
        private BundleManager manager;

        public BundleLoader(System.Uri uri, BundleInfo bundleInfo, BundleLoader[] dependencies, BundleManager manager)
        {
            if (bundleInfo == null)
                throw new System.ArgumentNullException("bundleInfo");

            this.uri = uri;
            this.bundleInfo = bundleInfo;
            this.manager = manager;
            this.manager.AddBundleLoader(this);
            this.requestCache = new Dictionary<string, AssetBundleRequest>();
            this.executor = this.manager.Executor;

            this.dependencies = new List<BundleLoader>();
            if (dependencies == null || dependencies.Length == 0)
                return;

            for (int i = 0; i < dependencies.Length; i++)
            {
                var dependency = dependencies[i];
                if (!this.dependencies.Contains(dependency))
                {
                    dependency.Retain();
                    this.dependencies.Add(dependency);
                }
            }
        }

        public virtual string Name { get { return this.bundleInfo.Name; } }

        public virtual BundleInfo BundleInfo { get { return this.bundleInfo; } }

        public virtual IBundle Bundle { get { return this.IsDone && this.AssetBundle != null ? new InternalBundleWrapper(this, this) : null; } }

        public virtual int Priority
        {
            get { return this.priority; }
            set
            {
                if (this.priority > value)
                    return;

                this.priority = value;
                List<BundleLoader> loaders = GetDependencies(false);
                for (int i = 0; i < loaders.Count; i++)
                    loaders[i].Priority = this.priority;
            }
        }

        protected virtual AssetBundle AssetBundle { get { return this.assetBundle ?? (this.assetBundle = (this.loadResult != null && this.loadResult.IsDone ? this.loadResult.Result : null)); } }

        protected virtual bool IsDone { get { return this.result != null ? this.result.IsDone : false; } }

        protected virtual System.Uri Uri { get { return this.uri; } }

        protected virtual BundleManager BundleManager { get { return this.manager; } }

        protected virtual string GetAbsoluteUri()
        {
            string path = this.Uri.AbsoluteUri;
            if (this.Uri.Scheme.Equals("jar") && !path.StartsWith("jar:file://"))
                path = path.Replace("jar:file:", "jar:file://");
            return path;
        }

        protected virtual string GetAbsolutePath()
        {
            string path = System.Uri.UnescapeDataString(this.Uri.AbsolutePath);
            if (this.Uri.Scheme.Equals("jar"))
                path = path.Replace("file://", "jar:file://");
            return path;
        }

        protected virtual bool IsRemoteUri()
        {
            if ("http".Equals(uri.Scheme) || "https".Equals(uri.Scheme) || "ftp".Equals(uri.Scheme))
                return true;
            return false;
        }

        protected virtual List<BundleLoader> GetDependencies(bool recursive)
        {
            if (!recursive)
                return this.dependencies;

            if (this.allDependencies != null)
                return this.allDependencies;

            this.allDependencies = new List<BundleLoader>();
            for (int i = 0; i < this.dependencies.Count; i++)
            {
                var loader = this.dependencies[i];
                if (!this.allDependencies.Contains(loader))
                    this.allDependencies.Add(loader);

                var list = loader.GetDependencies(true);
                for (int j = 0; j < list.Count; j++)
                {
                    var sub = list[j];
                    if (sub != this && !this.allDependencies.Contains(sub))
                        this.allDependencies.Add(sub);
                }
            }
            return this.allDependencies;
        }

        protected IEnumerator Wrap(IEnumerator task, IPromise promise)
        {
            this.Retain();
            InterceptableEnumerator enumerator = new InterceptableEnumerator(task);
            enumerator.RegisterCatchBlock(e =>
            {
                promise.SetException(e);
            });
            enumerator.RegisterFinallyBlock(() =>
            {
                this.Release();
            });
            return enumerator;
        }

        protected IEnumerator Wrap(IEnumerator task)
        {
            this.Retain();
            InterceptableEnumerator enumerator = new InterceptableEnumerator(task);
            enumerator.RegisterFinallyBlock(() =>
            {
                this.Release();
            });
            return enumerator;
        }

        public void Retain()
        {
            lock (_lock)
            {
                if (this.disposed)
                    throw new System.ObjectDisposedException(this.Name);

                this.refCount++;
            }
        }

        public void Release()
        {
            lock (_lock)
            {
                if (this.disposed)
                    return;

                this.refCount--;
                if (this.refCount <= 0)
                    this.Dispose();
            }
        }

        public virtual IProgressResult<float, IBundle> Load()
        {
            if (this.result == null || this.result.Exception != null)
                this.result = this.Execute<float, IBundle>(promise => Wrap(DoLoadBundleAndDependencies(promise)));

            ProgressResult<float, IBundle> resultCopy = new ProgressResult<float, IBundle>();
            this.result.Callbackable().OnProgressCallback(p => resultCopy.UpdateProgress(p));
            this.result.Callbackable().OnCallback((r) =>
            {
                if (r.Exception != null)
                    resultCopy.SetException(r.Exception);
                else
                    resultCopy.SetResult(new InternalBundleWrapper(this, r.Result));
            });
            return resultCopy;
        }

        protected virtual IEnumerator DoLoadBundleAndDependencies(IProgressPromise<float, IBundle> promise)
        {
            this.startTime = Time.realtimeSinceStartup;
            List<IProgressResult<float, AssetBundle>> results = new List<IProgressResult<float, AssetBundle>>();
            IProgressResult<float, AssetBundle> currResult = this.LoadAssetBundle();

            if (!currResult.IsDone)
                results.Add(currResult);

            var all = this.GetDependencies(true);
            for (int i = 0; i < all.Count; i++)
            {
                var result = all[i].LoadAssetBundle();
                if (!result.IsDone)
                    results.Add(result);
            }

            bool finished = false;
            float progress = 0f;
            float timeProgress = 0f;
            int count = results.Count;
            while (!finished && count > 0)
            {
                yield return null;

                progress = 0f;
                finished = true;
                for (int i = 0; i < count; i++)
                {
                    var result = results[i];
                    if (!result.IsDone)
                        finished = false;

                    progress += result.Progress;
                }

                timeProgress = TIME_PROGRESS_WEIGHT * Mathf.Atan(Time.realtimeSinceStartup - this.startTime) * 2 / Mathf.PI;
                promise.UpdateProgress(timeProgress + (1.0f - TIME_PROGRESS_WEIGHT) * progress / count);
            }

            promise.UpdateProgress(1f);

            yield return null;

            if (currResult.Exception != null)
                promise.SetException(currResult.Exception);
            else
                promise.SetResult(this);
        }

        protected virtual IProgressResult<float, AssetBundle> LoadAssetBundle()
        {
            if (this.loadResult == null || this.loadResult.Exception != null)
            {
                this.loadResult = new ProgressResult<float, AssetBundle>();
                LoadingTask<float, AssetBundle> task = new LoadingTask<float, AssetBundle>(loadResult, Wrap(DoLoadAssetBundle(loadResult), loadResult), this);
                this.executor.Execute(task);
            }

            return this.loadResult;
        }

        protected abstract IEnumerator DoLoadAssetBundle(IProgressPromise<float, AssetBundle> promise);

        #region IBundle Support
        protected virtual void Check()
        {
            if (this.disposed)
                throw new System.ObjectDisposedException(this.Name);

            if (!this.IsDone || this.AssetBundle == null)
                throw new System.Exception(string.Format("The AssetBundle '{0}' isn't ready.", this.Name));
        }

        protected virtual string GetFullName(string name)
        {
            if (name.StartsWith(ASSETS, System.StringComparison.OrdinalIgnoreCase) || name.IndexOf("/") < 0)
                return name;
            return string.Format("{0}{1}", ASSETS, name);
        }

        protected IProgressResult<TProgress, TResult> Execute<TProgress, TResult>(System.Func<IProgressPromise<TProgress, TResult>, IEnumerator> func)
        {
            ProgressResult<TProgress, TResult> result = new ProgressResult<TProgress, TResult>();
            Executors.RunOnCoroutine(func(result), result);
            return result;
        }

        //public virtual bool Contains(string name)
        //{
        //    this.Check();
        //    var fullName = GetFullName(name);
        //    return this.AssetBundle.Contains(fullName);
        //}

        public virtual Object LoadAsset(string name, System.Type type)
        {
            this.Check();

            if (string.IsNullOrEmpty(name))
                throw new System.ArgumentNullException("name", "The name is null or empty!");

            if (type == null)
                throw new System.ArgumentNullException("type");

            var fullName = GetFullName(name);
            return this.AssetBundle.LoadAsset<Object>(fullName);
        }

        public virtual T LoadAsset<T>(string name) where T : Object
        {
            this.Check();

            if (string.IsNullOrEmpty(name))
                throw new System.ArgumentNullException("name", "The name is null or empty!");

            var fullName = GetFullName(name);
            return this.AssetBundle.LoadAsset<T>(fullName);
        }

        protected string Key(string name, System.Type type)
        {
            return string.Format("{0}-{1}", name, type);
        }

        protected string Key(string name, System.Type type, string flag)
        {
            return string.Format("{0}-{1}-{2}", name, type, flag);
        }

        public virtual IProgressResult<float, Object> LoadAssetAsync(string name, System.Type type)
        {
            try
            {
                this.Check();

                if (string.IsNullOrEmpty(name))
                    throw new System.ArgumentNullException("name", "The name is null or empty!");

                if (type == null)
                    throw new System.ArgumentNullException("type");

                return this.Execute<float, Object>(promise => Wrap(DoLoadAssetAsync(promise, name, type)));
            }
            catch (System.Exception e)
            {
                return new ImmutableProgressResult<float, Object>(e, 0f);
            }
        }

        protected virtual IEnumerator DoLoadAssetAsync(IProgressPromise<float, Object> promise, string name, System.Type type)
        {
            string key = Key(name, type);
            AssetBundleRequest request;
            if (!this.requestCache.TryGetValue(key, out request))
            {
                var fullName = GetFullName(name);
                request = this.AssetBundle.LoadAssetAsync(fullName, type);
                this.requestCache.Add(key, request);
            }

            while (!request.isDone)
            {
                promise.UpdateProgress(request.progress);
                yield return null;
            }

            this.requestCache.Remove(key);

            Object asset = request.asset;
            if (asset == null)
            {
                promise.SetException(new System.Exception(string.Format("Not found the asset {0}", name)));
                yield break;
            }
            promise.UpdateProgress(1f);
            promise.SetResult(asset);
        }

        public virtual IProgressResult<float, T> LoadAssetAsync<T>(string name) where T : Object
        {
            try
            {
                this.Check();

                if (string.IsNullOrEmpty(name))
                    throw new System.ArgumentNullException("name", "The name is null or empty!");

                return this.Execute<float, T>(promise => Wrap(DoLoadAssetAsync<T>(promise, name)));
            }
            catch (System.Exception e)
            {
                return new ImmutableProgressResult<float, T>(e, 0f);
            }
        }

        protected virtual IEnumerator DoLoadAssetAsync<T>(IProgressPromise<float, T> promise, string name) where T : Object
        {
            string key = Key(name, typeof(T));
            AssetBundleRequest request;
            if (!this.requestCache.TryGetValue(key, out request))
            {
                var fullName = GetFullName(name);
                request = this.AssetBundle.LoadAssetAsync<T>(fullName);
                this.requestCache.Add(key, request);
            }

            while (!request.isDone)
            {
                promise.UpdateProgress(request.progress);
                yield return null;
            }

            this.requestCache.Remove(key);

            Object asset = request.asset;
            if (asset == null)
            {
                promise.SetException(new System.Exception(string.Format("Not found the asset '{0}'.", name)));
                yield break;
            }

            promise.UpdateProgress(1f);
            promise.SetResult(asset);
        }

        public virtual IProgressResult<float, Object[]> LoadAssetsAsync(System.Type type, params string[] names)
        {
            try
            {
                this.Check();

                if (names == null || names.Length <= 0)
                    new System.ArgumentNullException("names", "The names is null or empty!");

                if (type == null)
                    throw new System.ArgumentNullException("type");

                return this.Execute<float, Object[]>(promise => Wrap(DoLoadAssetsAsync(promise, type, names)));
            }
            catch (System.Exception e)
            {
                return new ImmutableProgressResult<float, Object[]>(e, 0f);
            }
        }

        protected virtual IEnumerator DoLoadAssetsAsync(IProgressPromise<float, Object[]> promise, System.Type type, params string[] names)
        {
            List<Object> results = new List<Object>();
            int count = names.Length;
            float progress = 0f;
            foreach (string name in names)
            {
                var fullName = GetFullName(name);
                AssetBundleRequest request = this.AssetBundle.LoadAssetAsync(fullName, type);
                while (!request.isDone)
                {
                    promise.UpdateProgress(progress + request.progress / count);
                    yield return null;
                }
                progress += 1f / count;
                Object asset = request.asset;
                if (asset != null)
                    results.Add(asset);
            }

            promise.UpdateProgress(1f);
            promise.SetResult(results.ToArray());
        }

        public virtual IProgressResult<float, T[]> LoadAssetsAsync<T>(params string[] names) where T : Object
        {
            try
            {
                this.Check();

                if (names == null || names.Length <= 0)
                    new System.ArgumentNullException("names", "The names is null or empty!");

                return this.Execute<float, T[]>(promise => Wrap(DoLoadAssetsAsync<T>(promise, names)));
            }
            catch (System.Exception e)
            {
                return new ImmutableProgressResult<float, T[]>(e, 0f);
            }
        }

        protected virtual IEnumerator DoLoadAssetsAsync<T>(IProgressPromise<float, T[]> promise, params string[] names) where T : Object
        {
            List<T> results = new List<T>();
            int count = names.Length;
            float progress = 0f;
            foreach (string name in names)
            {
                var fullName = GetFullName(name);
                AssetBundleRequest request = this.AssetBundle.LoadAssetAsync<T>(fullName);
                while (!request.isDone)
                {
                    promise.UpdateProgress(progress + request.progress / count);
                    yield return null;
                }
                progress += 1f / count;
                T asset = (T)request.asset;
                if (asset != null)
                    results.Add(asset);
            }

            promise.UpdateProgress(1f);
            promise.SetResult(results.ToArray());
        }

        public virtual Object[] LoadAllAssets(System.Type type)
        {
            this.Check();

            if (type == null)
                throw new System.ArgumentNullException("type");

            return this.AssetBundle.LoadAllAssets(type);
        }

        public virtual T[] LoadAllAssets<T>() where T : Object
        {
            this.Check();
            return this.AssetBundle.LoadAllAssets<T>();
        }

        public virtual IProgressResult<float, Object[]> LoadAllAssetsAsync(System.Type type)
        {
            try
            {
                this.Check();

                if (type == null)
                    throw new System.ArgumentNullException("type");

                return this.Execute<float, Object[]>(promise => Wrap(DoLoadAllAssetsAsync(promise, type)));
            }
            catch (System.Exception e)
            {
                return new ImmutableProgressResult<float, Object[]>(e, 0f);
            }
        }

        protected virtual IEnumerator DoLoadAllAssetsAsync(IProgressPromise<float, Object[]> promise, System.Type type)
        {
            string key = Key("_ALL", type);
            AssetBundleRequest request;
            if (!this.requestCache.TryGetValue(key, out request))
            {
                request = this.AssetBundle.LoadAllAssetsAsync(type);
                this.requestCache.Add(key, request);
            }

            while (!request.isDone)
            {
                promise.UpdateProgress(request.progress);
                yield return null;
            }

            this.requestCache.Remove(key);
            promise.UpdateProgress(1f);
            promise.SetResult(request.allAssets);
        }

        public virtual IProgressResult<float, T[]> LoadAllAssetsAsync<T>() where T : Object
        {
            try
            {
                this.Check();
                return this.Execute<float, T[]>(promise => Wrap(DoLoadAllAssetsAsync<T>(promise)));
            }
            catch (System.Exception e)
            {
                return new ImmutableProgressResult<float, T[]>(e, 0f);
            }
        }

        protected virtual IEnumerator DoLoadAllAssetsAsync<T>(IProgressPromise<float, T[]> promise) where T : Object
        {
            string key = Key("_ALL", typeof(T));
            AssetBundleRequest request;
            if (!this.requestCache.TryGetValue(key, out request))
            {
                request = this.AssetBundle.LoadAllAssetsAsync<T>();
                this.requestCache.Add(key, request);
            }

            while (!request.isDone)
            {
                promise.UpdateProgress(request.progress);
                yield return null;
            }

            this.requestCache.Remove(key);

            var all = request.allAssets;
            T[] assets = new T[all.Length];
            for (int i = 0; i < all.Length; i++)
            {
                assets[i] = ((T)all[i]);
            }

            promise.UpdateProgress(1f);
            promise.SetResult(assets);
        }

        public virtual UnityEngine.Object[] LoadAssetWithSubAssets(string name, System.Type type)
        {
            this.Check();

            if (string.IsNullOrEmpty(name))
                throw new System.ArgumentNullException("name", "The name is null or empty!");

            if (type == null)
                throw new System.ArgumentNullException("type");

            var fullName = GetFullName(name);
            return this.AssetBundle.LoadAssetWithSubAssets(fullName, type);
        }

        public virtual T[] LoadAssetWithSubAssets<T>(string name) where T : UnityEngine.Object
        {
            this.Check();

            if (string.IsNullOrEmpty(name))
                throw new System.ArgumentNullException("name", "The name is null or empty!");

            var fullName = GetFullName(name);
            return this.AssetBundle.LoadAssetWithSubAssets<T>(fullName);
        }

        public virtual IProgressResult<float, T[]> LoadAssetWithSubAssetsAsync<T>(string name) where T : UnityEngine.Object
        {
            try
            {
                this.Check();

                if (string.IsNullOrEmpty(name))
                    throw new System.ArgumentNullException("name", "The name is null or empty!");

                return this.Execute<float, T[]>(promise => Wrap(DoLoadAssetWithSubAssetsAsync(promise, name)));
            }
            catch (System.Exception e)
            {
                return new ImmutableProgressResult<float, T[]>(e, 0f);
            }
        }

        protected virtual IEnumerator DoLoadAssetWithSubAssetsAsync<T>(IProgressPromise<float, T[]> promise, string name) where T : Object
        {
            string key = Key(name, typeof(T), "SubAssets");
            AssetBundleRequest request;
            if (!this.requestCache.TryGetValue(key, out request))
            {
                var fullName = GetFullName(name);
                request = this.AssetBundle.LoadAssetWithSubAssetsAsync(fullName, typeof(T));
                this.requestCache.Add(key, request);
            }

            while (!request.isDone)
            {
                promise.UpdateProgress(request.progress);
                yield return null;
            }

            this.requestCache.Remove(key);

            var all = request.allAssets;
            T[] assets = new T[all.Length];
            for (int i = 0; i < all.Length; i++)
            {
                assets[i] = ((T)all[i]);
            }

            promise.UpdateProgress(1f);
            promise.SetResult(assets);
        }

        public virtual IProgressResult<float, UnityEngine.Object[]> LoadAssetWithSubAssetsAsync(string name, System.Type type)
        {
            try
            {
                this.Check();

                if (string.IsNullOrEmpty(name))
                    throw new System.ArgumentNullException("name", "The name is null or empty!");

                if (type == null)
                    throw new System.ArgumentNullException("type");

                return this.Execute<float, Object[]>(promise => Wrap(DoLoadAssetWithSubAssetsAsync(promise, name, type)));
            }
            catch (System.Exception e)
            {
                return new ImmutableProgressResult<float, Object[]>(e, 0f);
            }
        }

        protected virtual IEnumerator DoLoadAssetWithSubAssetsAsync(IProgressPromise<float, Object[]> promise, string name, System.Type type)
        {
            string key = Key(name, type, "SubAssets");
            AssetBundleRequest request;
            if (!this.requestCache.TryGetValue(key, out request))
            {
                var fullName = GetFullName(name);
                request = this.AssetBundle.LoadAssetWithSubAssetsAsync(fullName, type);
                this.requestCache.Add(key, request);
            }

            while (!request.isDone)
            {
                promise.UpdateProgress(request.progress);
                yield return null;
            }

            this.requestCache.Remove(key);

            promise.UpdateProgress(1f);
            promise.SetResult(request.allAssets);
        }
        #endregion

        #region IDisposable Support
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                try
                {
                    this.manager.RemoveBundleLoader(this);
                    /* Must be released in the main thread  */
                    Executors.RunOnMainThread(() =>
                    {
                        if (this.AssetBundle != null)
                            this.AssetBundle.Unload(false);

                        for (int i = 0; i < this.dependencies.Count; i++)
                        {
                            this.dependencies[i].Release();
                        }
                        this.dependencies.Clear();
                        this.allDependencies.Clear();
                    });
                }
                catch (System.Exception)
                {
                }
                finally
                {
                    disposed = true;
                }
            }
        }

        ~BundleLoader()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }
        #endregion
    }

    #region LoadingTask Support
    class LoadingTask<TProgress, TResult> : ITask
    {
        private long startTime = 0L;
        private IEnumerator routine;
        private BundleLoader loader;
        private IProgressPromise<TProgress, TResult> result;

        public LoadingTask(IProgressPromise<TProgress, TResult> result, IEnumerator routine, BundleLoader loader)
        {
            this.result = result;
            this.routine = routine;
            this.loader = loader;
            this.startTime = System.DateTime.Now.Ticks / 10000;
        }

        public bool IsDone { get { return this.result.IsDone; } }

        public int Priority { get { return this.loader.Priority; } }

        public long StartTime { get { return this.startTime; } }

        public virtual IEnumerator GetRoutin()
        {
            return this.routine;
        }

        #region IDisposable Support
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                this.loader = null;
                this.result = null;
                this.routine = null;
                this.disposed = true;
            }
        }

        //~LoadingTask()
        //{
        //    Dispose(false);
        //}

        public void Dispose()
        {
            Dispose(true);
            //System.GC.SuppressFinalize(this);
        }
        #endregion
    }
    #endregion
}
