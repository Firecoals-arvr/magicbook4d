using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Firecoals.Augmentation
{
    public class InstantiationAsync : MonoBehaviour
    {
        public GameObject prefab;
        private void Start()
        {
            Debug.Log("Before instantiate");
            for (var i = 0; i < 10; i++)
            {
                Task.Run(async () =>
                {
                    // Example of long running code.
                    await Task.Delay(1000);
                    Dispatcher.Instance.Invoke(() =>
                    {
                        GameObject.Instantiate(prefab, this.transform);
                        Debug.Log("Instantiated");
                    });
                });
            }
            // Method will return almost instantly, ready to do more work.
        }
        /// <summary>
        /// Instantiate a GameObject as a child of parent in delay time
        /// </summary>
        /// <param name="go">GameObject to be spawn</param>
        /// <param name="parent"></param>
        /// <param name="delayTimeInMillisecond">delay time</param>
        public void InstantiateAsync(GameObject go, Transform parent, int delayTimeInMillisecond)
        {

            Task.Run(async () =>
            {
                // Example of long running code.
                await Task.Delay(delayTimeInMillisecond);
                Dispatcher.Instance.Invoke(() =>
                {
                    GameObject.Instantiate(go, parent);
                });
            });

        }
        public void InstantiateAsync(GameObject go, int delayTimeInMillisecond)
        {

            Task.Run(async () =>
            {
                // Example of long running code.
                await Task.Delay(delayTimeInMillisecond);
                Dispatcher.Instance.Invoke(() =>
                {
                    GameObject.Instantiate(go);
                });
            });

        }

        public void InTest()
        {
            InstantiateAsync(prefab, 100);
        }
    }

}
