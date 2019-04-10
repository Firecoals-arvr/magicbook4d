using NatCorder;
using NatCorder.Clocks;
using NatCorder.Inputs;
using NatShareU;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Firecoals.Render
{
    public class Recording : MonoBehaviour
    {
        [Header("Recording")] public int videoWidth = 1280;
        public int videoHeight = 720;
        public Camera arCamera;
        [Header("Microphone")] public bool recordMicrophone = true;
        public AudioSource microphoneSource;

        private MP4Recorder videoRecorder;
        private IClock recordingClock;
        public CameraInput cameraInput { get; private set; }
        public AudioInput audioInput { get; private set; }

        public void StartRecording()
        {
            // Start recording
            recordingClock = new RealtimeClock();
            videoRecorder = new MP4Recorder(
                videoWidth,
                videoHeight,
                60,
                recordMicrophone ? AudioSettings.outputSampleRate : 0,
                recordMicrophone ? (int)AudioSettings.speakerMode : 0,
                OnReplay
            );
            // Create recording inputs
            cameraInput = new CameraInput(videoRecorder, recordingClock, arCamera);
            
            if (recordMicrophone)
            {
                StartMicrophone();
                audioInput = new AudioInput(videoRecorder, recordingClock, microphoneSource, true);
            }
        }

        private void StartMicrophone()
        {
#if !UNITY_WEBGL || UNITY_EDITOR // No `Microphone` API on WebGL :(
            // Create a microphone clip
            microphoneSource.clip = Microphone.Start(null, true, 60, 48000);
            while (Microphone.GetPosition(null) <= 0) ;
            // Play through audio source
            microphoneSource.timeSamples = Microphone.GetPosition(null);
            microphoneSource.loop = true;
            microphoneSource.Play();
#endif
        }

        public void StopRecording()
        {
            // Stop the recording inputs
            if (recordMicrophone)
            {
                StopMicrophone();
                audioInput.Dispose();
            }

            cameraInput.Dispose();
            // Stop recording
            videoRecorder.Dispose();
        }

        private void StopMicrophone()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            Microphone.End(null);
            microphoneSource.Stop();
#endif
        }

        private void OnReplay(string path)
        {
            Debug.Log("Saved recording to: " + path);
            PopupManager.PopUpDialog("", "Saved recording to: " + path);
            
            // Playback the video
#if UNITY_EDITOR
            EditorUtility.OpenWithDefaultApp(path);
#elif UNITY_IOS
            Handheld.PlayFullScreenMovie("file://" + path);
#elif UNITY_ANDROID
            Handheld.PlayFullScreenMovie(path);
            NatShare.SaveToCameraRoll(path, "MagicBook 4D", false);
#endif
        }
    }

}