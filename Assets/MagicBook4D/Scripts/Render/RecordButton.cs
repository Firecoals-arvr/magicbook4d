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
        private void Reset()
        {
            //Set countdown fill = 0
            cowntdown.fillAmount = 0;
            Debug.Log("reset");
        }

        private void OnPress(bool pressed)
        {
            //Debug.Log("Pressed: " + pressed.ToString());
            
            StartCoroutine(CowntDown(pressed));

            if (!pressed)
            {
                Reset();
                cownting = false;
                recording.StopRecording();
            }
            else
            {
                recording.StartRecording();
            }

        }

        private IEnumerator CowntDown(bool pressed)
        {

            cownting = true;
            yield return new WaitForSeconds(0.2f);
            if (!cownting) yield break;
            float startTime = Time.time;
            float ratio = 0;
            while (cownting && (ratio = (Time.time - startTime) / MaxRecordingTime) < 1.0f)
            {
                cowntdown.fillAmount = ratio;
                yield return null;
            }
            //Reset();

        }
    }

}