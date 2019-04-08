using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Firecoals.Space
{
    public class TakePhotos : MonoBehaviour
    {
        /// <summary>
        /// texture cho ảnh
        /// </summary>
        private Texture2D tex2d;

        /// <summary>
        /// ảnh tạm
        /// </summary>
        public UI2DSprite sprTemp;

        /// <summary>
        /// ảnh cho hiển thị lên preview
        /// </summary>
        public UI2DSprite spriteResult;

        /// <summary>
        /// panel hiển thị ảnh preview và các nút lưu, chia sẻ, xóa
        /// </summary>
        public GameObject previewObject;

        [Header("Buttons on screen")]
        public GameObject[] menuButtons;

        string persitentPath;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// chụp ảnh
        /// </summary>
        public void RunTakeScreenShot()
        {
            foreach (var a in menuButtons)
            {
                NGUITools.SetActive(a, false);
            }
            StartCoroutine(TakeScreenShot());
        }

        private IEnumerator TakeScreenShot()
        {
            yield return new WaitForEndOfFrame();
            tex2d = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            tex2d.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            tex2d.Apply();
            sprTemp.sprite2D = Sprite.Create(tex2d, new Rect(0, 0, Screen.width, Screen.height), new Vector2(0, 0));
            NGUITools.SetActive(sprTemp.gameObject, false);
            spriteResult.sprite2D = sprTemp.sprite2D;
            spriteResult.transform.localScale = new Vector3(12f, 8f, 0f);
            NGUITools.SetActive(previewObject, true);
            Debug.Log("TakeScreenShot done !");
        }

        /// <summary>
        /// tắt ảnh preview
        /// </summary>
        public void ClosePreviewImage()
        {
            NGUITools.SetActive(previewObject, false);
            foreach (var a in menuButtons)
            {
                NGUITools.SetActive(a, true);
            }
        }

        /// <summary>
        /// lưu ảnh vừa chụp
        /// </summary>
        public void CallSaveImageToGallery()
        {
            persitentPath = Application.persistentDataPath + "/SpaceImages";
            if (!Directory.Exists(persitentPath))
            {
                Directory.CreateDirectory(persitentPath);
            }
            foreach (var a in menuButtons)
            {
                NGUITools.SetActive(a, true);
            }
            byte[] bytes = tex2d.EncodeToJPG();
            string nameBasedOnTime = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);
            string location = persitentPath + "/Space_" + nameBasedOnTime + ".jpg";
            //string location = persitentPath + "hello.jpg";
            File.WriteAllBytes(location, bytes);
            Debug.Log("Image has saved on path: " + location);

            //ấn save xong sẽ tắt preview
            ClosePreviewImage();
        }

        //chưa kiểm chứng
        public void SharePhoto()
        {
            StartCoroutine(ShareScreenshot());
        }

        public IEnumerator ShareScreenshot()
        {
            // wait for graphics to render
            yield return new WaitForEndOfFrame();

            // prepare texture with Screen and save it
            Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, true);
            texture.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height), 0, 0);
            texture.Apply();

            // save to persistentDataPath File
            byte[] data = texture.EncodeToPNG();
            string destination = Path.Combine(Application.persistentDataPath,
                                              System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".png");
            File.WriteAllBytes(destination, data);

            if (!Application.isEditor)
            {
#if UNITY_ANDROID
                string body = "Body of message to be shared";
                string subject = "Subject of message";

                AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
                AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
                intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
                AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
                AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + destination);
                intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
                intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), body);
                intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
                intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
                AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

                // run intent from the current Activity
                currentActivity.Call("startActivity", intentObject);
#endif
            }
        }
    }
}
