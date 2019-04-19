using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    public class BoosterController : MonoBehaviour
    {
        [SerializeField] Animation anima;
        [SerializeField] AudioSource audioSrc;
        public void Lauch()
        {
            if (anima.isPlaying)
            {
                anima.Play("Idle");
                audioSrc.Stop();
            }
            else
            {
                anima.Play("Idle");
                anima.PlayQueued("Lauch");
                audioSrc.Play();
            }
        }
        public void Lauch2()
        {
            anima.Play("Lauch2");
        }
        public void Idle()
        {
            anima.Play("Idle");
            audioSrc.Stop();
        }
        public void Booster4d()
        {
            if (!anima.isPlaying)
            {
                anima.Play("4d");
                audioSrc.Stop();
            }
        }
    }
}
