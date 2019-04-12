using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    public class BoosterController : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            anima = GetComponent<Animation>();
        }
        Animation anima;
        void Update()
        {

        }
        public void Lauch()
        {
            if (anima.isPlaying)
            {
                anima.Play("Idle");
                GetComponent<AudioSource>().Stop();
            }
            else
            {
                anima.Play("Idle");
                anima.PlayQueued("Lauch");
                GetComponent<AudioSource>().Play();
            }
        }
        public void Lauch2()
        {
            anima.Play("Lauch2");
        }
        public void Idle()
        {
            anima.Play("Idle");
            GetComponent<AudioSource>().Stop();
        }
        public void Booster4d()
        {
            if (!anima.isPlaying)
            {
                anima.Play("4d");
                GetComponent<AudioSource>().Stop();
            }
        }
    }
}
