using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class Laser_00 : MonoBehaviour {

	public string target;

	public float rotateSpeed = 0f;

	public void SetSkillInfo(JsonData skillInfo){
		this.target = (string)skillInfo ["Target"];
		SetPosition ();
		this.transform.position += new Vector3 ((float)(double)skillInfo ["Position"] [0], (float)(double)skillInfo ["Position"] [1], (float)(double)skillInfo ["Position"] [2]);
		Vector3 RotateAngles = new Vector3 ((float)(double)skillInfo ["Rotation"] [0], (float)(double)skillInfo ["Rotation"] [1], (float)(double)skillInfo ["Rotation"] [2]);
		this.transform.eulerAngles = RotateAngles;
		rotateSpeed = (float)(double)skillInfo ["RotateSpeed"];
	}

	void FixedUpdate(){
		this.transform.Rotate (new Vector3 (0, rotateSpeed, 0) * Time.fixedDeltaTime);
	}

	void SetPosition(){
		if (target == "Boss") {
			this.transform.position = GameObject.Find ("BossController").transform.position;
		} else if (target == "Player") {
			this.transform.position = GameObject.Find ("PlayerController").transform.position;
		}
	}
}
