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
        [SerializeField] Animation anima;
        //[SerializeField] AudioSource audioSrc;
        //public AudioClip[] ListAudio;

        void Start()
        {
            //audioSrc = GetComponent<AudioSource>();
            //anima = alienChild.GetComponent<Animation>();
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
                    anima.Play("Idle");
                if (anima.IsPlaying("Dance"))
                    anima.Stop();
            }
            if (stt == true)
            {
                AlienIntro();
                stt = false;
            }
        }

        /// <summary>
        /// check trạng thái xuất hiện
        /// </summary>
        public bool stt = false;

        public void AlienTalk()
        {
            anima.Play("Talking");
            //audioSrc.Play();
            alienChild.GetComponent<AudioSource>().Stop();
        }

        public void AlienDance()
        {
            anima.Play("Dance");
            alienChild.GetComponent<AudioSource>().Play();
        }

        public void AlienIntro()
        {
            anima.Play("Jump");
            alienChild.GetComponent<AudioSource>().Stop();
        }
    }
}