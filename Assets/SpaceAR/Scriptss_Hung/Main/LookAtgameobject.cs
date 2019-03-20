using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtgameobject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    public GameObject target;

	// Update is called once per frame
	void Update () {
        transform.LookAt(target.transform,Vector3.up);
	}
}
