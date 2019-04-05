using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

namespace Firecoals.Space
{
    public class TakeCameraScreenShot : MonoBehaviour
    {
        public UITexture previewTexture;
        //public UI2DSprite previewSprite;
        public GameObject previewObject;

        ////private void TakeScreenshot(int width, int height)
        ////{
        ////    Camera.main.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        ////}

        //public void TakePhotoViaDeviceCamera()
        //{
        //    StartCoroutine(TakePhotoByDeviceCamera());
        //    ShowPreviewImage();
        //}

        //public void ShowPreviewImage()
        //{
        //    StartCoroutine(DelayAFewSecons());
        //    NGUITools.SetActive(previewObject, true);
        //    previewTexture.mainTexture = (Texture)AssetDatabase.LoadAssetAtPath(Application.dataPath + "/hello.png", typeof(Texture));
        //}

        //IEnumerator DelayAFewSecons()
        //{
        //    yield return new WaitForSeconds(0.5f);
        //}

        //Texture _textureFromCamera;
        //IEnumerator TakePhotoByDeviceCamera()
        //{
        //    _textureFromCamera = Camera.main.targetTexture;
        //    //_textureFromCamera = new Texture2D(Camera.main.targetTexture.width, Camera.main.targetTexture.height);
        //    Texture2D tex2d = new Texture2D(_textureFromCamera.width, _textureFromCamera.height);
        //    tex2d.SetPixel(Camera.main.targetTexture.width, Camera.main.targetTexture.height, Color.clear);
        //    tex2d.Apply();
        //    byte[] bytes = tex2d.EncodeToPNG();
        //    File.WriteAllBytes(Application.dataPath + "/hello.png", bytes);
        //    yield return new WaitForSeconds(0.5f);
        //}

        ////Texture2D DuplicateTexture(Texture2D tex)
        ////{
        ////    RenderTexture renderTex = RenderTexture.GetTemporary(
        ////                tex.width,
        ////                tex.height,
        ////                0,
        ////                RenderTextureFormat.Default,
        ////                RenderTextureReadWrite.Linear);
        ////    Graphics.Blit(tex, renderTex);
        ////    RenderTexture previous = RenderTexture.active;
        ////    RenderTexture.active = renderTex;
        ////    Texture2D tex2D = new Texture2D(tex.width, tex.height);
        ////    tex2D.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
        ////    tex2D.Apply();
        ////    RenderTexture.active = previous;
        ////    RenderTexture.ReleaseTemporary(renderTex);
        ////    return tex2D;
        ////}

        private bool takeScreenshotOnNextFrame;

        private void OnPostRender()
        {
            if (takeScreenshotOnNextFrame)
            {
                takeScreenshotOnNextFrame = false;
                RenderTexture renderTexture = Camera.main.targetTexture;

                Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
                Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
                renderResult.ReadPixels(rect, 0, 0);

                byte[] byteArray = renderResult.EncodeToPNG();
                File.WriteAllBytes(Application.dataPath + "/hello.png", byteArray);
                Debug.Log("Image saved!");

                RenderTexture.ReleaseTemporary(renderTexture);
                Camera.main.targetTexture = null;
            }
        }

        public void TakeScreenshot(int width, int height)
        {
            Camera.main.targetTexture = RenderTexture.GetTemporary(width, height, 16);
            takeScreenshotOnNextFrame = true;
        }

        public void PreviewImage()
        {
            NGUITools.SetActive(previewObject, true);

        }
    }
}
