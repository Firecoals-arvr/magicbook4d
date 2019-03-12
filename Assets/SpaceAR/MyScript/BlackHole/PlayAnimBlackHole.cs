using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimBlackHole : MonoBehaviour
{
    public Animator anim;
    public void PlayAnim()
    {
        anim.GetComponent<Animator>().Play("BlackHoleEat");
    }
}
