using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Firecoals.Render
{
    public class TakeThePhoto : MonoBehaviour
    {
        //public Texture2D tex2d;
        //public UI2DSprite spriteResult;

        public void StartTakePhoto(Texture2D tex2d)
        {
            StartCoroutine(TakeAPhotoByClick(tex2d));
        }

        public IEnumerator TakeAPhotoByClick(Texture2D tex2d)
        {
            yield return new WaitForEndOfFrame();
            //spriteResult = FindObjectOfType<UI2DSprite>();
            tex2d = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            tex2d.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            tex2d.Apply();

            //spriteResult.transform.localScale = new Vector3(0.7f, 0.4f, 0f);
            //NGUITools.SetActive(spriteResult.gameObject, false);
            Debug.Log("TakeScreenShot done !");
        }

        public void SavePhotoToGalleryOnDevice(Texture2D tex2d)
        {
            byte[] bytes = tex2d.EncodeToJPG();
            string nameBasedOnTime = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);

            //lưu ảnh vào gallery
            NativeGallery.SaveImageToGallery(bytes, Application.productName + "Space", nameBasedOnTime);
        }

        public void SharePhotoOnSocialNetwork()
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

        public virtual void ClosePreviewImage()
        {
        }
    }
}
