using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    /// <summary>
    /// class này để chạy animation của blackhole
    /// </summary>
    public class PlayAnimBlackHole : MonoBehaviour
    {
        public Animation anim;
        public void PlayAnim()
        {
            anim.Play("Blackholeeat");
        }
    }
}
