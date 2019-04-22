using System;
using System.Collections;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Firecoals.Render
{
    public class RecordButton : MonoBehaviour
    {
        private const float MaxRecordingTime = 60f; // seconds
        public UISprite cowntdown;
        private bool cownting;
        private Recording recording;
        public static bool toBeCreatedMP4;
        private void Start()
        {
            recording = GetComponent<Recording>();
            EventDelegate mEventDelegate = new EventDelegate(this, "OnPress");
            EventDelegate.Set(GetComponent<UIButton>().onClick, mEventDelegate);
            Reset();
        }
        /// <summary>
        /// Set countdown fill = 0
        /// </summary>
        private void Reset()
        {
            //Set countdown fill = 0
            cowntdown.fillAmount = 0;
        }

        private float pressTime;
        private void OnPress(bool pressed)
        {
            StartCoroutine(CowntDown());
            if (!pressed)
            {
                var deltaTime = Time.realtimeSinceStartup - pressTime;
                Debug.LogWarning("delta time: " + deltaTime);
                if (deltaTime < 1f)
                {
                    toBeCreatedMP4 = false;
                    Reset();
                    Capture.Instance.Snap();
                    recording.audioInput.Dispose();
                    recording.cameraInput.Dispose();
                    //recording.videoRecorder.Dispose();
                    //TODO Take Screen shot
                    DeleteAllRedudantMp4File();
                    Debug.LogWarning("Snap");
                    //StartCoroutine(GameObject.FindObjectOfType<Capture>().TakePhoto());
                }
                else
                {
                    toBeCreatedMP4 = true;
                    Reset();
                    cownting = false;
                    recording.StopRecording();
                }
            }
            else
            {
                pressTime = Time.realtimeSinceStartup;
                recording.StartRecording();
            }

        }

        private void DeleteAllRedudantMp4File()
        {
            string path = Application.persistentDataPath;

            switch (Application.platform)
            {
                case RuntimePlatform.WindowsEditor:
                    path = Directory.GetCurrentDirectory();
                    goto case RuntimePlatform.WindowsPlayer;
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.IPhonePlayer:
                    {
                        var di = new DirectoryInfo(path);
                        var files = di.GetFiles("*.mp4")
                            .Where(p => p.Extension == ".mp4").ToArray();
                        foreach (FileInfo file in files)
                            try
                            {
                                file.Attributes = FileAttributes.Normal;
                                File.Delete(file.FullName);
                                Debug.Log("<color>deleted " + file.FullName + "</color>");
                            }
                            catch(Exception ex) { Debug.LogError(ex.Message); }
                        break;
                    }
                case RuntimePlatform.Android:
                    {
                        var di = new DirectoryInfo(path);
                        var files = di.GetFiles("*.mp4")
                            .Where(p => p.Extension == ".mp4").ToArray();
                        foreach (FileInfo file in files)
                            try
                            {
                                file.Attributes = FileAttributes.Normal;
                                File.Delete(file.FullName);
                                Debug.Log("<color>deleted " + file.FullName + "</color>");
                            }
                            catch (Exception ex) { Debug.LogError(ex.Message); }
                        break;
                    }
            }
        }
        private IEnumerator CowntDown()
        {
            cownting = true;
            yield return new WaitForSeconds(.2f);
            if (!cownting) yield break;
            float startTime = Time.time;
            float ratio = 0;
            while (cownting && (ratio = (Time.time - startTime) / MaxRecordingTime) < 1.0f)
            {
                cowntdown.fillAmount = ratio;
                yield return null;
            }
            Reset();

        }
    }

}