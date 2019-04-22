using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Firecoals.Augmentation
{
    /// <remarks>Instantiate GameObject Asynchronous</remarks>>
    public class InstantiationAsync : MonoBehaviour
    {
        //public GameObject prefab;
        //private void Start()
        //{
        //    Debug.Log("Before instantiate");
        //    for (var i = 0; i < 10; i++)
        //    {
        //        Task.Run(async () =>
        //        {
        //            // Example of long running code.
        //            await Task.Delay(1000);
        //            Dispatcher.Instance.Invoke(() =>
        //            {
        //                GameObject.Instantiate(prefab, transform);
        //                Debug.Log("Instantiated");
        //            });
        //        });
        //    }
        //    // Method will return almost instantly, ready to do more work.
        //}

        /// <summary>
        /// Instantiate a GameObject as a child of parent in delay time
        /// </summary>
        /// <param name="original">GameObject to be spawn</param>
        /// <param name="parent"></param>
        /// <param name="delayTimeInMillisecond">delay time</param>
        public static void InstantiateAsync(GameObject original, Transform parent, int delayTimeInMillisecond)
        {
            Task.Run(async () =>
            {
                // Example of long running code.
                await Task.Delay(delayTimeInMillisecond);
                Dispatcher.Instance.Invoke(() =>
                {
                    GameObject.Instantiate(original, parent);
                });
            });

        }

        /// <summary>
        /// Instantiate a GameObject
        /// </summary>
        /// <param name="original">GameObject to be spawn</param>
        /// <param name="delayTimeInMillisecond">delay time</param>
        public static void InstantiateAsync(GameObject original, int delayTimeInMillisecond)
        {

            Task.Run(async () =>
            {
                // Example of long running code.
                await Task.Delay(delayTimeInMillisecond);
                Dispatcher.Instance.Invoke(() =>
                {
                    GameObject.Instantiate(original);
                });
            });

        }
        /// <summary>
        /// Clones a GameObject
        /// </summary>
        /// <param name="original"></param>
        /// <param name="parent"></param>
        /// <param name="instantiateInWorldSpace"></param>
        /// <param name="delayTimeInMillisecond"></param>
        public static void InstantiateAsync(GameObject original, Transform parent, bool instantiateInWorldSpace, int delayTimeInMillisecond)
        {

            Task.Run(async () =>
            {
                // Example of long running code.
                await Task.Delay(delayTimeInMillisecond);
                Dispatcher.Instance.Invoke(() =>
                {
                    GameObject.Instantiate(original, parent, instantiateInWorldSpace);
                });
            });

        }
        /// <summary>
        /// Clones a GameObject
        /// </summary>
        /// <param name="original"></param>
        /// <param name="position"></param>
        /// <param name="quaternion"></param>
        /// <param name="delayTimeInMillisecond"></param>
        public static void InstantiateAsync(GameObject original, Vector3 position, Quaternion quaternion, int delayTimeInMillisecond)
        {

            Task.Run(async () =>
            {
                // Example of long running code.
                await Task.Delay(delayTimeInMillisecond);
                Dispatcher.Instance.Invoke(() =>
                {
                    GameObject.Instantiate(original, position, quaternion);
                });
            });

        }
        /// <summary>
        /// Clones a GameObject
        /// </summary>
        /// <param name="original"></param>
        /// <param name="position"></param>
        /// <param name="quaternion"></param>
        /// <param name="parent"></param>
        /// <param name="delayTimeInMillisecond"></param>
        public static void InstantiateAsync(GameObject original, Vector3 position, Quaternion quaternion, Transform parent, int delayTimeInMillisecond)
        {

            Task.Run(async () =>
            {
                // Example of long running code.
                await Task.Delay(delayTimeInMillisecond);
                Dispatcher.Instance.Invoke(() =>
                {
                    GameObject.Instantiate(original, position, quaternion, parent);
                });
            });

        }

        public void DestroyAfterSpawnInSecond(GameObject clone, float second)
        {
            CM_Job.Make(DelayDestroy(clone, second)).Start();
        }

        private IEnumerator DelayDestroy(GameObject clone, float second)
        {
            yield return new WaitForSeconds(second);
            Destroy(clone);
        }
        public static void Asynchronous(Action callback, int delayTimeInMillisecond)
        {
            Task.Run(async () =>
            {
                // Example of long running code.
                await Task.Delay(delayTimeInMillisecond);
                Dispatcher.Instance.Invoke(() => { callback(); });
            });

        }
    }

}