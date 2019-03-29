using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadTexture : MonoBehaviour {

	// Use this for initialization
	public Shader shaderdefault;
	void Start () {
		// shaderdefault= GetComponent<Renderer>().material.shader;
		// GetComponent<Renderer>().material.shader=Shader.Find("Standard");
		// Invoke("changeShader",5f);
	}
	public void changeShader()
	{
		// GetComponent<Renderer>().material.shader=shaderdefault;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
