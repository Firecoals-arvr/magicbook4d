using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranformObjectToCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	private float speed=1;
	// Update is called once per frame
	void Update () {
		if(transform.localPosition!=Vector3.zero)
		{
			transform.localPosition=Vector3.MoveTowards(gameObject.transform.localPosition,Vector3.zero,speed*Time.deltaTime);
			transform.rotation= Quaternion.Euler(new Vector3(90, 0, 0));
		}
		
	}
}
