using UnityEngine;

public class PlayerExample : MonoBehaviour
{

    public float moveSpeed;
    public GameObject AlienChild;
    public Joystick joystick;
    public GameObject ufo;
    Animation anima;
    AudioSource Audio;
    public AudioClip[] ListAudio;

    void Start()
    {
        Audio = GetComponent<AudioSource>();
        anima = AlienChild.GetComponent<Animation>();
    }

    void Update()
    {
        Vector3 moveVector = (transform.right * joystick.Horizontal + transform.forward * joystick.Vertical).normalized;
        transform.Translate(moveVector * moveSpeed * Time.deltaTime * transform.localScale.x);
        if (moveVector != Vector3.zero)
        {
            AlienChild.transform.rotation = Quaternion.LookRotation(moveVector * moveSpeed * Time.deltaTime);
            anima.Play("Walking");
            AlienChild.GetComponent<AudioSource>().Stop();
        }
        else
        {
            if (anima.IsPlaying("Walking"))
                anima.Stop();
            if (anima.isPlaying == false)
                anima.Play("Idle");
        }
        if (stt == true)
        {
            AlienIntro();
            stt = false;
        }
    }

    // trang thai xuat hien
    public bool stt = false;

    public void AlienTalk()
    {
        anima.Play("Talking");
        Audio.Play();
        AlienChild.GetComponent<AudioSource>().Stop();
    }

    public void AlienDance()
    {
        anima.Play("Dance");
        AlienChild.GetComponent<AudioSource>().Play();
    }

    public void AlienIntro()
    {
        anima.Play("Jump");
        AlienChild.GetComponent<AudioSource>().Stop();
    }

    public void CallUFO()
    {
        ufo.GetComponent<Animation>().Play("AlienReturnToUFO");
    }
}