using System.Collections;
using UnityEngine;

namespace Firecoals.Render
{
    public class RecordButton : MonoBehaviour
    {
        private const float MaxRecordingTime = 60f; // seconds
        public UISprite cowntdown;
        private bool cownting;
        private Recording recording;
        private void Start()
        {
            recording = GameObject.FindObjectOfType<Recording>();
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
                    Reset();
                    recording.audioInput.Dispose();
                    recording.cameraInput.Dispose();
                    //TODO Take Screen shot
                    
                    Capture.Instance.Snap();
                    Debug.LogWarning("Snap");
                    //StartCoroutine(GameObject.FindObjectOfType<Capture>().TakePhoto());
                }
                else
                {

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