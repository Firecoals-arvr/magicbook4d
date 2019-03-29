using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingController : MonoBehaviour {

	// Use this for initialization
	private GameObject ParentEnd;
	void Start () {
		ParentEnd=gameObject.transform.Find("/CameraOther").gameObject;
		ParentTarget=this.gameObject.transform.parent.gameObject;
		//currentPosition=this.gameObject.transform.position;
		destina=Vector3.zero;
	}
	float distance=10f;
	// Update is called once per frame
	void Update () 
	{
			if(this.transform.localPosition!=destina)
			{
				this.transform.localPosition=Vector3.MoveTowards(this.transform.position,destina,distance*Time.deltaTime);
			}
	}
	//[SerializeField]
	//private GameObject Target;
	//[SerializeField]
	private GameObject ParentTarget;
	private Vector3 destina;
	private Vector3 currentPosition;
	public void ChangeParentObject()
	{
		if(this.transform.parent==ParentEnd.transform)
		{
			Debug.Log(ParentTarget.transform.name);
			this.transform.parent=ParentTarget.transform;
			destina=new Vector3(0,0,0);
			//this.gameObject.layer=0;
			Debug.Log(this.transform.position+"-"+destina+"-"+distance+"-"+this.transform.localPosition);
		}
		else
		{
			Debug.Log("#Camera");
			//currentPosition=this.gameObject.transform.position;
			this.transform.parent=ParentEnd.transform;
			//this.gameObject.layer=5;
			destina=new Vector3(0,0,0);
		}
	}
}
