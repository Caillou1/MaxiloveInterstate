using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour {

	[SerializeField]
	Toggle[] weapons;

	[SerializeField]
	float[] cooldowns;

	List<Toggle> togglesOn = new List<Toggle>();


	/*
	void Update(){
		if (Input.GetKeyDown (KeyCode.Space)) {
			GetActivatedToggles ();
		}
	}
	*/


	public void GetActivatedToggles(){
		togglesOn.Clear ();

		foreach (Toggle toggle in weapons) {
			if (toggle.isOn) {
				togglesOn.Add (toggle);
			}
		}
		//Debug.Log (togglesOn.Count);
		if (togglesOn.Count != 0) {
			TriggerCooldown (togglesOn, cooldowns[togglesOn.Count - 1]);
		}
	}

	//deactivate weapons and start cooldown
	void TriggerCooldown(List<Toggle> toggles, float time){
		foreach (Toggle toggle in toggles) {
			toggle.interactable = false;
			toggle.isOn = false;
			toggle.GetComponent<WeaponCooldown> ().ActivateCooldown (time);
		}


	}

}
