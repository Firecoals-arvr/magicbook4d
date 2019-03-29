using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCameraController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		gameObject.transform.localPosition=new Vector3(gameObject.transform.localPosition.x,gameObject.transform.localPosition.y,Camera.main.GetComponent<Camera>().farClipPlane-(Camera.main.GetComponent<Camera>().farClipPlane/100));
		gameObject.transform.localScale=new Vector3(Camera.main.GetComponent<Camera>().farClipPlane/2,1,Camera.main.GetComponent<Camera>().farClipPlane*0.375f);
	}
}
