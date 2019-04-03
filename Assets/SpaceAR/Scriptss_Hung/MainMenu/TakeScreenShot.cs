using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Firecoals.Space
{
    public class TakeScreenShot : MonoBehaviour
    {
        public UITexture preview;

        private void Start()
        {
            Texture tex = Camera.main.targetTexture;

        }

        private IEnumerator TakePhotoOnScreen()
        {
            yield return new WaitForEndOfFrame();
            Texture2D tex2d = new Texture2D(Screen.width, Screen.height);
            tex2d.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            tex2d.Apply();

            byte[] bytes = tex2d.EncodeToJPG();
            File.WriteAllBytes(Application.dataPath + "preview image.jpg", bytes);

            preview.gameObject.SetActive(true);

            preview.mainTexture = tex2d;
        }

        public void CallTakePhotoMethod()
        {
            StartCoroutine(TakePhotoOnScreen());
        }
    }
}
