using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public GameObject moon;
	public GameObject robot;
	public void changeMode()
	{
		if(moon.active==false)
		{
			modeMoon();
			//Debug.Log("1");
		}
		else
		{
			modeRobot();
			//Debug.Log("2");
		}
	}
	public void modeRobot()
	{
		GetComponent<Animation>().Play("modeRobot");
	}
	public void modeMoon()
	{
		GetComponent<Animation>().Play("modeMoon");
	}
}
