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
        [SerializeField] Animation[] _alienanima = default;
        //[SerializeField] AudioSource audioSrc;
        //public AudioClip[] ListAudio;

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
                if (anima.IsPlaying("Dance"))
                    anima.Stop();
            }
            //if (stt == true)
            //{
            //    AlienIntro();
            //    stt = false;
            //}
        }

        /// <summary>
        /// check trạng thái xuất hiện
        /// </summary>
        //public bool stt = false;

        public void AlienTalk()
        {
            anima.Play("Talking");
            foreach (var a in _alienanima)
            {
                a.Play("Talking");
            }
            _loadSoundbundle.PlayMusicOfObjects(_talkmusic);
        }

        public void AlienDance()
        {
            //if (!this.transform.parent.gameObject.activeInHierarchy)
            //{
            //    this.transform.parent.gameObject.SetActive(true);
            //}
            alienChild.GetComponent<Animation>().Play("Dance");
            anima.Play("Dance");
            foreach (var a in _alienanima)
            {
                a.Play("Dance");
            }
            _loadSoundbundle.PlayMusicOfObjects(_dancemusic);
        }
    }
}