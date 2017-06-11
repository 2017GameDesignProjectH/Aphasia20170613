using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class Ring_01 : MonoBehaviour {

	public string target;

	public float rotateSpeed;

	public void SetSkillInfo(JsonData skillInfo){
		this.target = (string)skillInfo ["Target"];
		SetPosition ();
		this.transform.position += new Vector3 ((float)(double)skillInfo ["Position"] [0], (float)(double)skillInfo ["Position"] [1], (float)(double)skillInfo ["Position"] [2]);
		Vector3 RotateAngles = new Vector3 ((float)(double)skillInfo ["Rotation"] [0], (float)(double)skillInfo ["Rotation"] [1], (float)(double)skillInfo ["Rotation"] [2]);
		this.transform.eulerAngles = RotateAngles;
		for (int i = 0; i < this.transform.childCount; i++) {
			this.transform.GetChild (i).GetComponent<Rigidbody> ().velocity = Quaternion.Euler (RotateAngles) * new Vector3 (
				Mathf.Sin ((0.5f * ((int)skillInfo ["RingRange"] / 90f) * (i / 4) / (this.transform.childCount / 4) + (float)(i % 4) * 0.5f) * Mathf.PI),
				0f,
				Mathf.Cos ((0.5f * ((int)skillInfo ["RingRange"] / 90f) * (i / 4) / (this.transform.childCount / 4) + (float)(i % 4) * 0.5f) * Mathf.PI)
			) * (float)(double)skillInfo ["Velocity"];
		}
		rotateSpeed = (float)(double)skillInfo ["RotateSpeed"];
	}

	void FixedUpdate(){
		//this.transform.Rotate (this.transform.up, rotateSpeed * Time.fixedDeltaTime, Space.Self);

	}

	void SetPosition(){
		if (target == "Boss") {
			this.transform.position = GameObject.Find ("BossController").transform.position;
		} else if (target == "Player") {
			this.transform.position = GameObject.Find ("PlayerController").transform.position;
		}
	}
}
