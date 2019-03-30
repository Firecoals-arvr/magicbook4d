using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Moverandom : MonoBehaviour
{
    public float timer, speed;
    public int newtime;
    public NavMeshAgent nav;
    public Vector3 Target;
    // Use this for initialization
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
       // gameObject.transform.position = new Vector3(Random.Range(-200, 200), gameObject.transform.position.y, Random.Range(-200, 200));
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= newtime)
        {
            Newtarget();
            timer = 0;
        }
        nav.speed = speed;
    }
    void Newtarget()
    {
        float myX = gameObject.transform.position.x;
        float myZ = gameObject.transform.position.z;

        float posX = myX + Random.Range(-50, 50);
        float posZ = myZ + Random.Range(-50, 50);
        Target = new Vector3(posX, gameObject.transform.position.y, posZ);
        nav.SetDestination(Target);
    }
}
