using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Firecoals.Render
{
    public class TakePhotos : MonoBehaviour
    {
        /// <summary>
        /// texture cho ảnh
        /// </summary>
        private Texture2D tex2d;

        /// <summary>
        /// ảnh cho hiển thị lên preview
        /// </summary>
        public UI2DSprite spriteResult;

        /// <summary>
        /// panel hiển thị ảnh preview và các nút lưu, chia sẻ, xóa
        /// </summary>
        public GameObject previewObject;

        [Header("Buttons on screen")]
        public GameObject menuButtons;

        [Header("Buttons camera")]
        public GameObject cameraButton;
        public GameObject videoRecord;

        [Header("Panel name/information")]
        public GameObject panelInfo;
        public GameObject panelName;

        /// <summary>
        /// chụp ảnh
        /// </summary>
        public void RunTakeScreenShot()
        {
            //StartCoroutine(pts.TakeAPhotoByClick(tex2d));
            ////tex2d = pts.tex2d;

            //spriteResult.sprite2D = Sprite.Create(tex2d, new Rect(0, 0, Screen.width, Screen.height), new Vector2(0.5f, 0.5f));
            //spriteResult.transform.localScale = new Vector3(0.7f, 0.4f, 0f);

            NGUITools.SetActive(menuButtons, false);
            NGUITools.SetActive(cameraButton, false);
            NGUITools.SetActive(videoRecord, false);
            NGUITools.SetActive(panelName, false);
            NGUITools.SetActive(panelInfo, false);
            StartCoroutine(TakeScreenShot());
        }

        private IEnumerator TakeScreenShot()
        {
            yield return new WaitForEndOfFrame();

            tex2d = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            tex2d.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            tex2d.Apply();
            NGUITools.SetActive(previewObject, true);

            spriteResult.sprite2D = Sprite.Create(tex2d, new Rect(0, 0, Screen.width, Screen.height), new Vector2(0, 0));
            spriteResult.transform.localScale = new Vector3(0.7f, 0.4f, 0f);

            Debug.Log("TakeScreenShot done !");
        }

        /// <summary>
        /// tắt ảnh preview
        /// </summary>
        public void ClosePreviewImage()
        {
            NGUITools.SetActive(previewObject, false);
            NGUITools.SetActive(menuButtons, true);
            NGUITools.SetActive(cameraButton, true);
            NGUITools.SetActive(videoRecord, true);
            NGUITools.SetActive(panelName, true);
            NGUITools.SetActive(panelInfo, true);
        }

        /// <summary>
        /// lưu ảnh vừa chụp
        /// </summary>
        public void CallSaveImageToGallery()
        {
            //persitentPath = Application.persistentDataPath + "/SpaceImages";
            //if (!Directory.Exists(persitentPath))
            //{
            //    Directory.CreateDirectory(persitentPath);
            //}

            NGUITools.SetActive(previewObject, false);
            NGUITools.SetActive(menuButtons, true);
            NGUITools.SetActive(cameraButton, true);
            NGUITools.SetActive(videoRecord, true);
            NGUITools.SetActive(panelName, true);
            NGUITools.SetActive(panelInfo, true);

            //pts.SavePhotoToGalleryOnDevice(tex2d);
            byte[] bytes = tex2d.EncodeToJPG();
            string nameBasedOnTime = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);
            //string location = persitentPath + "/Space_" + nameBasedOnTime + ".jpg";
            //File.WriteAllBytes(location, bytes);
            //Debug.Log("Image has saved on path: " + location);

            //lưu ảnh vào gallery
            NativeGallery.SaveImageToGallery(bytes, Application.productName + "Space", nameBasedOnTime);

            //ấn save xong sẽ tắt preview
            ClosePreviewImage();
        }

        //chưa kiểm chứng
        public void SharePhoto()
        {
            StartCoroutine(ShareScreenshot());
            //pts.SharePhotoOnSocialNetwork();
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
