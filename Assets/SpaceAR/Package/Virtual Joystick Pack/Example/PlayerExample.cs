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
        private AudioSource audioSrc;
        Animation anima;
#pragma warning disable IDE0044 // Add readonly modifier
        [SerializeField] Animation[] _alienanima;
#pragma warning restore IDE0044 // Add readonly modifier
        //[SerializeField] AudioSource audioSrc;
        //public AudioClip[] ListAudio;

        void Start()
        {
            audioSrc = GetComponent<AudioSource>();
            anima = alienChild.GetComponent<Animation>();
        }

        void Update()
        {
            Vector3 moveVector = (transform.right * joystick.Horizontal + transform.forward * joystick.Vertical).normalized;
            transform.Translate(moveVector * moveSpeed * Time.deltaTime * transform.localScale.x);
            if (moveVector != Vector3.zero)
            {
                alienChild.transform.rotation = Quaternion.LookRotation(moveVector * moveSpeed * Time.deltaTime);
                anima.Play("Walking");
                alienChild.GetComponent<AudioSource>().Stop();
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
            audioSrc.Play();
            alienChild.GetComponent<AudioSource>().Stop();
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
            alienChild.GetComponent<AudioSource>().Play();
        }
    }
}