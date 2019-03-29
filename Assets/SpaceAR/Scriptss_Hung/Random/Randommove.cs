using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randommove : MonoBehaviour {

    // Use this for initialization
    public GameObject people;
    void Start () {
        rigid = gameObject.GetComponent<Rigidbody>();
       
    }
    Rigidbody rigid;	// Update is called once per frame
	void Update () {
        transform.RotateAround(people.transform.position, new Vector3(0, 1, 0), 2 * Time.deltaTime);
    }

}
