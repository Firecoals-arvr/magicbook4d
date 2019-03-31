using System.Collections;
using System.Collections.Generic;
using Firecoals.Animal;
using UnityEngine;

public class RhinoController : MonoBehaviour
{
    // Start is called before the first frame update
    public string IldeRhino;
    public string AnimalAttack;
    public float timeAttack;
    public float timeWaitPlayAnimation;
    protected Animation anim;

    void Start()
    {
        anim = GetComponent<Animation>();
        StartCoroutine(StarAnimal());
    }
    // <summary>
    /// Animation start Scene
    /// </summary>
    /// <returns></returns>
    private IEnumerator StarAnimal()
    {
        yield return new WaitForSeconds(timeWaitPlayAnimation);
        anim = GetComponent<Animation>();
        anim.PlayQueued(AnimalAttack, QueueMode.PlayNow);
        yield return new WaitForSeconds(timeAttack);
        anim.Stop();
        anim[IldeRhino].speed = 1;
        anim.CrossFade(IldeRhino);
        anim.Play(IldeRhino);
    }

}
