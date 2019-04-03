using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnableMainMenuInGame : MonoBehaviour {
	[SerializeField] GameObject mainUI;
	[SerializeField] GameObject trialUI;
	void OnEnable () {
		Time.timeScale = 1;
		if (ActiveManager.IsActiveOfflineOk () == false) {
			trialUI.SetActive (true);
			mainUI.SetActive (true);
		} else {
			mainUI.SetActive (true);
		}
	}


}
