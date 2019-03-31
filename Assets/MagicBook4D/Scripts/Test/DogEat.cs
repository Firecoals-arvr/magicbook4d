using System.Collections;
using System.Collections.Generic;
using Firecoals.Animal;
using UnityEngine;

public class DogEat : DefaultController
{

    public GameObject Item;
    public GameObject ImageTarget;
    public bool eatloop = false;
    public Vector3 foodpos;
    public Vector3 inpos;
    public Transform feet;
    public bool caught = false;
    public Transform targetitemtransform;
    bool change;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (CanEat==false)
        {
            transform.parent = Item.transform;
            transform.localPosition = foodpos;
            transform.localRotation = Quaternion.Euler(Vector3.zero);
            caught = false;
        }



    }
    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player" && caught == false)
        {
            CanEat = true;
            Debug.Log("catched");
            StartCoroutine(CaughtTiming(coll.gameObject));
            Debug.Log("catched1");
            transform.localPosition = inpos;
            transform.localRotation = Quaternion.Euler(Vector3.zero);

        }
        if (coll.gameObject.tag == "Place" && caught == true)
        {
            Debug.Log("catched");
            StartCoroutine(CaughtTiming(coll.gameObject));
            Debug.Log("catched1");
            transform.localPosition = inpos;
            transform.localRotation = Quaternion.Euler(Vector3.zero);
        }
    }
    IEnumerator CaughtTiming(GameObject coll)
    {
        Debug.Log("phase1");
        transform.parent = coll.gameObject.transform;
        yield return new WaitForSeconds(1);
        if (caught == true)
        {
            caught = false;
        }
        else
            caught = true;
        Debug.Log("phase2");
    }
}
