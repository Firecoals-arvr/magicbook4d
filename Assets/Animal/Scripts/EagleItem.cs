using UnityEngine;
using System.Collections;
using Firecoals.Animal;

public class EagleItem : DefaultController
{
    public GameObject leg;
    public GameObject item;
    public GameObject imageTarget;
    public bool caught = false;
    public GameObject mesh;
    void Update()
    {
        //if (CanEat == false)
        //{
        //   caught = false;
        //   mesh.active = true;
        //   transform.parent = item.transform;
        //   transform.localPosition = new Vector3(-4, 0, -52);
        //   transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        //}
        //if (caught == true)
        //{
        //    mesh.active = false;
        //}


    }
    void OnTriggerEnter(Collider coll)
    {
        //if (coll.gameObject.tag == "Player")
        //{
        //    Debug.Log("catched");
        //    transform.parent = coll.gameObject.transform;
        //    transform.localPosition = Vector3.zero;
        //    gameObject.GetComponent<Rigidbody>().isKinematic = true;
        //    caught = true;
        //}
    }
}
