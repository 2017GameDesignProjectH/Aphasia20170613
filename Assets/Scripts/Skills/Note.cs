using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour {

	public Vector3 velocity = Vector3.zero;
	
	// Update is called once per frame
	void FixedUpdate () {
		this.transform.localPosition = this.transform.localPosition + velocity * Time.fixedDeltaTime;
	}

}
