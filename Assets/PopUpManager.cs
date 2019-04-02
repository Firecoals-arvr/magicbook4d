using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour {

	[SerializeField] Text textPopUp;
	[SerializeField] GameObject popUp;
	[SerializeField] Button xButton;
	public static PopUpManager instance;
	void Start(){
		if(instance==null){
			instance = this;

		}
	}
	public void ShowPopup(bool hihi,bool haha,string message){
		popUp.SetActive (hihi);
		xButton.gameObject.SetActive (haha);
		textPopUp.text = message;
	}
}
