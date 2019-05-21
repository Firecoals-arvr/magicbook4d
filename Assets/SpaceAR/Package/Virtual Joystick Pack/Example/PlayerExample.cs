using Firecoals.AssetBundles.Sound;
using UnityEngine;

namespace Firecoals.Space
{
    /// <summary>
    /// chạy các animation của alien
    /// </summary>
    public class PlayerExample : MonoBehaviour
    {
        public float moveSpeed;
        public GameObject alienChild;
        public Joystick joystick;

        Animation anima;
        public Animation[] _alienanima;

        private LoadSoundbundles _loadSoundbundle;

        /// <summary>
        /// tag của âm thanh trong assetbundles
        /// </summary>
        public string _talkmusic;
        public string _dancemusic;

        void Start()
        {
            anima = alienChild.GetComponent<Animation>();
            _loadSoundbundle = GameObject.FindObjectOfType<LoadSoundbundles>();
        }

        void Update()
        {
            Vector3 moveVector = (transform.right * joystick.Horizontal + transform.forward * joystick.Vertical).normalized;
            transform.Translate(moveVector * moveSpeed * Time.deltaTime * transform.localScale.x);
            if (moveVector != Vector3.zero)
            {
                alienChild.transform.rotation = Quaternion.LookRotation(moveVector * moveSpeed * Time.deltaTime);
                anima.Play("Walking");
            }
            else
            {
                if (anima.IsPlaying("Walking"))
                    anima.Stop();
                if (anima.isPlaying == false)
                    anima.Play("Idle1");
            }
        }

        public void AlienTalk()
        {
            anima.Play("Talking");
            foreach (var a in _alienanima)
            {
                a.Play("Talking");
            }
            FirecoalsSoundManager.StopAll();
            _loadSoundbundle.PlayMusicOfObjects(_talkmusic);
        }

        public void AlienDance()
        {
            anima.Play("Dance");
            foreach (var a in _alienanima)
            {
                a.Play("Dance");
            }
            FirecoalsSoundManager.StopAll();
            _loadSoundbundle.PlayMusicOfObjects(_dancemusic);
        }
    }
}