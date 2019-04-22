using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    public class BoosterController : MonoBehaviour
    {
        [SerializeField] Animation anima;

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
