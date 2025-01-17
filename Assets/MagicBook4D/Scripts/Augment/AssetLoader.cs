﻿using Firecoals.AssetBundles;
using Firecoals.MagicBook;
using Loxodon.Framework.Asynchronous;
using Loxodon.Framework.Bundles;
using Loxodon.Framework.Contexts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Firecoals.Augmentation
{
    /// <summary>
    /// Prepare load assets to scene
    /// </summary>
    public class AssetLoader : MonoBehaviour
    {
        /// <summary>
        /// Bundle name means named assetbundle which built from Unity (Loxodon Bundle Framework or AssetBundle Browser)
        /// </summary>
        public string[] bundleNames;

        // Start is called before the first frame update
        /// <summary>
        /// Bundle root means the root folder where contains all built assetbundles and manifest file 
        /// </summary>
        public string bundleRoot;
        /// <summary>
        /// For call Pre-Load assetbundle
        /// </summary>
        public AssetBundlesLoader assetBundlesLoader = new AssetBundlesLoader();
        public IResources Resources { private set; get; }
        //private string iv = "5Hh2390dQlVh0AqC";
        //private string key = "E4YZgiGQ0aqe5LEJ";
        public static AssetLoader instance;

        private void OnDestroy()
        {
            assetBundlesLoader.OnDestroy();
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += BundleNameSwitch;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= BundleNameSwitch;
        }
        private void BundleNameSwitch(Scene scene, LoadSceneMode mode)
        {
            switch (ThemeController.instance.Theme)
            {
                case "Animal":
                    bundleRoot = "Animal/bundles";
                    if (ActiveManager.IsActiveOfflineOk(ActiveManager.NameToProjectID(ThemeController.instance.Theme)))
                    {
                        bundleNames = new[] {
                            "animals/model/bear",
                            "animals/model/buffalo",
                            "animals/model/cat",
                            "animals/model/chameleon",
                            "animals/model/cow",
                            "animals/model/crocodile",
                            "animals/model/dog",
                            "animals/model/dolphin",
                            "animals/model/eagle",
                            "animals/model/elephant",
                            "animals/model/frog",
                            "animals/model/giraffe",
                            "animals/model/gorilla",
                            "animals/model/horse",
                            "animals/model/kangaroo",
                            "animals/model/lion",
                            "animals/model/oschinh",
                            "animals/model/owl",
                            "animals/model/parrot",
                            "animals/model/peacock",
                            "animals/model/penguin",
                            "animals/model/pig",
                            "animals/model/rabbit",
                            "animals/model/rhino",
                            "animals/model/rooster",
                            "animals/model/sheep",
                            "animals/model/squirrel",
                            "animals/model/tiger",
                            "animals/model/turtle",
                            "animals/model/wolf",
                            "animals/info/en",
                            "animals/info/jp",
                            "animals/info/vn",
                            "animals/name/cn",
                            "animals/name/en",
                            "animals/name/vn",
                            "animals/name/jp",
                            "animals/noise"
                        };
                    }
                    else
                    {
                        bundleNames = new[] {
                            "animals/model/lion",
                            "animals/model/elephant",
                            "animals/model/gorilla",
                            "animals/info/en",
                            "animals/info/jp",
                            "animals/info/vn",
                            "animals/name/cn",
                            "animals/name/en",
                            "animals/name/vn",
                            "animals/name/jp",
                            "animals/noise"
                        };
                    }
                    break;
                case "Space":
                    bundleRoot = "Space/bundles";
                    if (ActiveManager.IsActiveOfflineOk(ActiveManager.NameToProjectID(ThemeController.instance.Theme)))
                    {
                        bundleNames = new[] {"space/models/solarsystem",
                            "space/models/sun",
                            "space/models/iss",
                            "space/models/mars",
                            "space/models/venus",
                            "space/models/mercury",
                            "space/models/earth",
                            "space/models/jupiter",
                            "space/models/saturn",
                            "space/models/uranus",
                            "space/models/neptune",
                            "space/models/moon",
                            "space/models/blackhole",
                            "space/models/boosterandshuttle",
                            "space/models/alienware",
                            "space/models/satellite",
                            "space/models/eclipse",
                            "space/models/game1",
                            "space/models/game2",
                            "space/models/bigbang",
                            "space/models/comet",
                            "space/sound/name/en",
                            "space/sound/name/vn",
                            "space/sound/info/vn",
                            "space/sound/info/en",
                            "space/music"
                        };
                    }
                    else
                    {
                        bundleNames = new[] {
                            "space/models/solarsystem",
                            "space/models/sun",
                            "space/models/mercury",
                            "space/sound/name/en",
                            "space/sound/name/vn",
                            "space/sound/info/vn",
                            "space/sound/info/en",
                            "space/music"
                        };

                    }

                    break;
                case "Color":
                    bundleRoot = "Color/bundles";
                    if (ActiveManager.IsActiveOfflineOk(ActiveManager.NameToProjectID(ThemeController.instance.Theme)))
                    {
                        bundleNames = new[] { "color/model/camtrai",
                            "color/model/chantrau",
                            "color/model/dabong",
                            "color/model/khurung",
                            "color/model/maybay",
                            "color/model/samac",
                            "color/model/tambien",
                            "color/model/thadieu",
                            "color/model/thanhpho",
                            "color/model/trangtrai",
                            "color/sounds/sounds"
                        };
                    }
                    else
                    {
                        bundleNames = new[] { "color/model/maybay", "color/sounds/sounds" };
                    }
                    break;

            }
        }
#region MONOBEHAVIOUR_METHOD
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                if (this != instance)
                {
                    Destroy(gameObject);
                }
            }
            

        }


#endregion

        public void InitResource()
        {
            BundleSetting setting = new BundleSetting(bundleRoot);
            Resources = CreateResources();
            ApplicationContext context = Context.GetApplicationContext();
            context.GetContainer().Register<IResources>(Resources);
        }

        /// <summary>
        /// Preload header of the asset bundles
        /// </summary>
        /// <param name="slider"></param>
        /// <returns></returns>
        public IEnumerator PreLoad(UISlider slider)
        {
            BundleSetting bundleSettings = new BundleSetting(bundleRoot);
            /*Preload asset bundle*/
            assetBundlesLoader.slider = slider;
            yield return assetBundlesLoader.Preload(bundleNames, 1);
        }

        /// <summary>
        /// The resource of assetbunlde
        /// </summary>
        /// <returns></returns>
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

        public void LoadGameObjectAsync(string[] bundlePaths, Transform parrent)
        {
            IProgressResult<float, GameObject[]> result = Resources.LoadAssetsAsync<GameObject>(bundlePaths);

            GameObject tempGameObject = null;
            result.Callbackable().OnProgressCallback(p =>
            {
                Debug.LogFormat(bundlePaths[0] + "is loading with result :{0}%", p * 100);
            });

            result.Callbackable().OnCallback((r) =>
            {
                try
                {
                    if (r.Exception != null)
                        throw r.Exception;
                    //tempGameObject = r.Result;
                    Debug.Log("<color=yellow>Instantiated " + r.Result[0].name + "</color>");
                    GameObject.Instantiate(r.Result[0], parrent);


                }
                catch (Exception e)
                {
                    Debug.LogErrorFormat("Load failure.Error:{0}", e);
                }
            });


            //return tempGameObject;
        }

        public GameObject LoadGameObjectAsync(string bundlePath, Transform parent)
        {
            IProgressResult<float, GameObject> result = Resources.LoadAssetAsync<GameObject>(bundlePath);

            GameObject tempGameObject = null;
            result.Callbackable().OnProgressCallback(p =>
            {
                Debug.LogFormat(bundlePath + "is loading with result :{0}%", p * 100);
            });
            //yield return result.WaitForDone();
            result.Callbackable().OnCallback((r) =>
            {
                try
                {
                    if (r.Exception != null)
                        throw r.Exception;
                    //tempGameObject = r.Result;
                    Debug.Log("<color=yellow>Instantiated " + r.Result.name + "</color>");

                    tempGameObject = Instantiate(r.Result, parent) as GameObject;
                    tempGameObject.SetActive(false);
                }
                catch (Exception e)
                {
                    Debug.LogErrorFormat("Load failure.Error:{0}", e);
                }
            });

            return tempGameObject;
        }


        public GameObject LoadGameObjectSync(string bundleName, string bundlePath)
        {
            IBundle bundle = assetBundlesLoader.bundles[bundleName];
            var go = bundle.LoadAsset<GameObject>(bundlePath);
            return go;
        }
    }

}
