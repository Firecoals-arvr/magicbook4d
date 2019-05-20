using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnMeteor : MonoBehaviour
{
    public Animator anim;
    bool check=true;
    //public void Start()
    //{
    //    anim = GetComponent<Animator>();
    //}
    public void Play()
    {
        if (check)
        {
            anim.Play("Fly");
            check = !check;
        }
            anim.Play("Idle");
            check = !check;
    }


}
