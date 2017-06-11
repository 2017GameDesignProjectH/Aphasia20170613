using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class Laser_01 : BasicSkillClass {
	
	public float rotateSpeed1 = 0f;
	public float rotateTime1 = 0f;
	public float rotateSpeed2 = 0f;
	public float rotateTime2 = 0f;

	public void SetSkillInfo(JsonData skillInfo){
		this.target = (string)skillInfo ["Target"];
		this.destroyTime = (float)(double)skillInfo ["DestroyTime"];
		this.SetPositionByTarget ();
		this.transform.position += new Vector3 ((float)(double)skillInfo ["Position"] [0], (float)(double)skillInfo ["Position"] [1], (float)(double)skillInfo ["Position"] [2]);
		this.transform.eulerAngles += new Vector3 ((float)(double)skillInfo ["Rotation"] [0], (float)(double)skillInfo ["Rotation"] [1], (float)(double)skillInfo ["Rotation"] [2]);

		this.rotateSpeed1 = (float)(double)skillInfo ["RotateSpeed1"];
		this.rotateSpeed2 = (float)(double)skillInfo ["RotateSpeed2"];
		this.rotateTime1 = (float)(double)skillInfo ["RotateTime1"];
		this.rotateTime2 = (float)(double)skillInfo ["RotateTime2"];

		this.Initialization = true;
	}

	void FixedUpdate(){
		if (Initialization) {
			if (rotateTime1 > 0f) {
				this.transform.Rotate (new Vector3 (0, rotateSpeed1, 0) * Time.fixedDeltaTime);
				rotateTime1 = rotateTime1 - Time.fixedDeltaTime;
			} else if (rotateTime2 > 0f){
				this.transform.Rotate (new Vector3 (0, rotateSpeed2, 0) * Time.fixedDeltaTime);
				rotateTime2 = rotateTime2 - Time.fixedDeltaTime;
			}

			if (destroyTime < 0f) {
				this.gameObject.SetActive (false);
			}
			destroyTime = destroyTime - Time.fixedDeltaTime;
		}
	}
}
