using NatShareU;
using System;
using System.Collections;
using UnityEngine;

namespace Firecoals.Render
{
    public class Capture : MonoBehaviour
    {
        public GameObject mainPanel;
        public GameObject reviewPanel;
        public UI2DSprite unitySprite;
        private Texture2D _screenShot;

        public static Capture Instance { get; private set; }
        //private void Start()
        //{
        //    if (Instance != this)
        //    {
        //        Instance = this;
        //    }
        //}

        public IEnumerator TakePhoto()
        {

            NGUITools.SetActive(mainPanel, false);
            NGUITools.SetActive(reviewPanel, true);
            NGUITools.SetActive(this.gameObject, false);
            yield return new WaitForEndOfFrame();

            _screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            _screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            _screenShot.Apply();

            unitySprite.sprite2D = Sprite.Create(_screenShot, new Rect(0, 0, Screen.width, Screen.height), new Vector2(0.5f, 0.5f));

            Debug.Log("TakeScreenShot done !");
        }

        public void Snap()
        {
            StartCoroutine(TakePhoto());
        }

        public void BackScreenShot()
        {
            NGUITools.SetActive(reviewPanel, false);
            NGUITools.SetActive(mainPanel, true);
            NGUITools.SetActive(this.gameObject, true);
            unitySprite.sprite2D = null;
        }

        public void Save()
        {
            unitySprite.sprite2D = null;
            NGUITools.SetActive(reviewPanel, false);
            NGUITools.SetActive(mainPanel, true);
            NGUITools.SetActive(this.gameObject, true);
            if (NativeGallery.CheckPermission() == NativeGallery.Permission.Granted)
            {
                NativeGallery.SaveImageToGallery(_screenShot, "MagicBook 4D", Guid.NewGuid() + ".png",
                    error =>
                    {
                        PopupManager.PopUpDialog(string.Empty,
                            !error.IsNullOrEmpty() ? error : "Lưu ảnh thành công vào thư viện");
                    });

                //NatShare.SaveToCameraRoll(_screenShot, "MagicBook 4D");
            }
            else if (NativeGallery.CheckPermission() == NativeGallery.Permission.Denied)
            {
                PopupManager.PopUpDialog("Cảnh báo", "Bạn cần cho phép app truy cập vào bộ sưu tập để lưu ảnh",
                    default, "Cài đặt", "Không",
                    PopupManager.DialogType.YesNoDialog, NativeGallery.OpenSettings);
            }
            else
            {
                PopupManager.PopUpDialog("Cảnh báo", "Bạn cần cho phép app truy cập vào bộ sưu tập để lưu ảnh",
                    default, "Cho phép", "Hủy", PopupManager.DialogType.YesNoDialog,
                    (() => NativeGallery.RequestPermission()));
            }
        }

        public void Share()
        {
            NatShare.Share(_screenShot, () => PopupManager.PopUpDialog(String.Empty, "Chia sẻ thành công"));
        }
    }
}
