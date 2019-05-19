using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Firecoals.SceneTransition
{
    public class SceneLoader : MonoBehaviour
    {
        private static SceneLoader SceneLoaderInstance;

        public GameObject LoadingScreen;
        // Get the progress of the load
        [HideInInspector]
        public float Progress = 0;
        public GameObject leftCloud, rightCloud, sun;
        public float time;
        private bool closed = false, openned = false;
        private AsyncOperation SceneLoadingOperation;

        private bool Loading = false;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static SceneLoader Instance
        {
            get
            {
                if (SceneLoaderInstance == null)
                {
                    SceneLoader sceneLoader = (SceneLoader)GameObject.FindObjectOfType(typeof(SceneLoader));
                    if (sceneLoader != null)
                    {
                        SceneLoaderInstance = sceneLoader;
                    }
                    else
                    {
                        GameObject sceneLoaderPrefab = Resources.Load<GameObject>("SceneLoader");
                        SceneLoaderInstance = (GameObject.Instantiate(sceneLoaderPrefab)).GetComponent<SceneLoader>();
                    }
                }
                return SceneLoaderInstance;
            }
        }

        /// <summary>
        /// Loads a scene.
        /// </summary>
        /// <param name="name">Name of the scene to load</param>
        public static void LoadScene(string name)
        {
            Instance.Load(name);
        }

        /// <summary>
        /// Loads a scene.
        /// </summary>
        /// <param buildIndex="buildIndex">Build index of the scene to load</param>
        public static void LoadScene(int buildIndex)
        {
            Instance.Load(buildIndex);
        }

        /// <summary>
        /// Awake
        /// </summary>
        public void Awake()
        {
            Object.DontDestroyOnLoad(gameObject);

            // Get rid of any old SceneLoaders
            if (SceneLoaderInstance != null && SceneLoaderInstance != this)
            {
                Destroy(SceneLoaderInstance.gameObject);
                SceneLoaderInstance = this;
            }
        }
        


        /// <summary>
        /// Loads a scene
        /// </summary>
        /// <param name="name">Name of the scene to load</param>
        private void Load(string name)
        {
            if (!Loading)
            {
                var scene = new Scene
                {
                    SceneName = name
                };
                StartCoroutine(InnerLoad(scene));
            }
        }

        /// <summary>
        /// Loads a scene
        /// </summary>
        /// <param buildIndex="buildIndex">Build index of the scene to load</param>
        private void Load(int buildIndex)
        {
            if (!Loading)
            {
                var scene = new Scene
                {
                    BuildIndex = buildIndex
                };
                StartCoroutine(InnerLoad(scene));
            }
        }

        /// <summary>
        /// Coroutine for loading the scene
        /// </summary>
        /// <returns>The load.</returns>
        /// <param name="name">Name of the scene to load</param>
        private IEnumerator InnerLoad(Scene scene)
        {
            Loading = true;
            Progress = 0;
            // Fade out
            //BeginFadeOut();
            //TODO Begin Open
            leftCloud.transform.localPosition = new Vector3(-1208f, 0f, 0f);
            rightCloud.transform.localPosition = new Vector3(1205f, 0f, 0f);
            CloseCloud();
            while (!closed)
            {
                yield return 0;
            }
            if (LoadingScreen != null && !closed)
            {
                LoadingScreen.SetActive(true);
            }

            //Start to load the level we want in the background
            if (!string.IsNullOrEmpty(scene.SceneName))
            {
                SceneLoadingOperation = SceneManager.LoadSceneAsync(scene.SceneName);
            }
            else
            {
                SceneLoadingOperation = SceneManager.LoadSceneAsync(scene.BuildIndex);
            }
            SceneLoadingOperation.allowSceneActivation = false;

            //Wait for the level to finish loading
            while (SceneLoadingOperation.progress < 0.9f)
            {
                Progress = SceneLoadingOperation.progress;
                yield return 0;
            }
            Progress = 1f;

            //SetFadersEnabled(true); // Enable Faders in new scene before switching to it
            //TODO Set active left/right clouds
            SceneLoadingOperation.allowSceneActivation = true;

            while (!SceneLoadingOperation.isDone)
            {
                yield return 0;
            }
            OpenCloud();
            if (LoadingScreen != null && openned)
            {
                LoadingScreen.SetActive(false);
            }

            // Fade in
            

            Loading = false; // At this point is should be safe to start a new load even though it's still fading in
        }


        private void CloseCloud()
        {
            closed = false;
            TweenPosition.Begin(leftCloud, time, new Vector3(-300f, 0f, 0f)).SetOnFinished(() =>
            {
                closed = true;
                openned = false;
                NGUITools.SetActive(sun,true);
                TweenScale.Begin(sun, 0.1f, Vector3.one);
            }); 
            TweenPosition.Begin(rightCloud, time, new Vector3(300f, 0f, 0f));
        }

        private void OpenCloud()
        {
            openned = false;
            TweenScale.Begin(sun, 0.25f, Vector3.zero);
            //NGUITools.SetActive(sun, false);
            TweenPosition.Begin(leftCloud, time, new Vector3(-1208f, 0f, 0f)).SetOnFinished(() =>
            {
                openned = true;
                closed = false;
            });
            TweenPosition.Begin(rightCloud, time, new Vector3(1205f, 0f, 0f));
        }
    }

    internal class Scene
    {
        public string SceneName;
        public int BuildIndex;
    }
}
