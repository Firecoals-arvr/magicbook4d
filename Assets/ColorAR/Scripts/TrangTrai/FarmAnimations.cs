using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmAnimations : MonoBehaviour
{
	Animator anim;
	// Start is called before the first frame update
	void Start()
	{
		anim.GetComponent<Animator>();
		//anim.SetFloat("Direction", -1.0f);
		anim.Play("di", -1, float.NegativeInfinity);
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			//Animator anim = gameObject.GetComponent<Animator>();
			// Reverse animation play
			anim.SetFloat("Direction", -1);
			anim.Play("di", -1, float.NegativeInfinity);
		}
	}
}
