using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSensor : MonoBehaviour {

	public GameObject moz;

	void Update(){
		this.transform.position = moz.transform.position;
	}

	void OnTriggerStay(Collider other){
		if (Input.GetKeyDown (KeyCode.JoystickButton0)) {
			if (other.name == "Square") {
				other.gameObject.SetActive (false);
			}
		}
		else if (Input.GetKeyDown (KeyCode.JoystickButton1)) {
			if (other.name == "Cross") {
				other.gameObject.SetActive (false);
			}
		}
		else if (Input.GetKeyDown (KeyCode.JoystickButton2)) {
			if (other.name == "Circle") {
				other.gameObject.SetActive (false);
			}
		}
		else if (Input.GetKeyDown (KeyCode.JoystickButton3)) {
			if (other.name == "Triangle") {
				other.gameObject.SetActive (false);
			}
		}
	}
}
