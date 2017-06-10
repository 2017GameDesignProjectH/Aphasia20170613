using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class Ring_01 : MonoBehaviour {

	public string target;

	public void SetSkillInfo(JsonData skillInfo){
		this.target = (string)skillInfo ["Target"];
		// TODO: Setting Target
		this.transform.position = new Vector3 ((float)(double)skillInfo ["Position"] [0], (float)(double)skillInfo ["Position"] [1], (float)(double)skillInfo ["Position"] [2]);
		this.transform.eulerAngles = new Vector3 ((float)(double)skillInfo ["Rotation"] [0], (float)(double)skillInfo ["Rotation"] [1], (float)(double)skillInfo ["Rotation"] [2]);
		for (int i = 0; i < this.transform.childCount; i++) {
			this.transform.GetChild (i).GetComponent<Rigidbody> ().velocity = new Vector3 (
				Mathf.Sin ((0.5f * ((int)skillInfo ["RingRange"] / 90f) * (i / 4) / (this.transform.childCount / 4) + (float)(i % 4) * 0.5f) * Mathf.PI),
				0f,
				Mathf.Cos ((0.5f * ((int)skillInfo ["RingRange"] / 90f) * (i / 4) / (this.transform.childCount / 4) + (float)(i % 4) * 0.5f) * Mathf.PI)
			) * (float)(double)skillInfo ["Velocity"];
		}
	}
}
