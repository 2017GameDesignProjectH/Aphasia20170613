using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class Laser_02 : BasicSkillClass {

	public float rotateSpeed = 0f;
	public float rotateTime = 0f;

	public void SetSkillInfo(JsonData skillInfo){
		this.target = (string)skillInfo ["Target"];
		this.destroyTime = (float)(double)skillInfo ["DestroyTime"];
		this.SetPositionByTarget ();
		this.transform.position += new Vector3 ((float)(double)skillInfo ["Position"] [0], (float)(double)skillInfo ["Position"] [1], (float)(double)skillInfo ["Position"] [2]);
		this.transform.eulerAngles += new Vector3 ((float)(double)skillInfo ["Rotation"] [0], (float)(double)skillInfo ["Rotation"] [1], (float)(double)skillInfo ["Rotation"] [2]);

		this.rotateSpeed = (float)(double)skillInfo ["RotateSpeed"];
		this.rotateTime = (float)(double)skillInfo ["RotateTime"];

		this.Initialization = true;
	}

	void FixedUpdate(){
		if (Initialization) {
			if (rotateTime > 0f) {
				this.transform.Rotate (new Vector3 (0, rotateSpeed, 0) * Time.fixedDeltaTime);
				rotateTime = rotateTime - Time.fixedDeltaTime;
			}
			if (destroyTime < 0f) {
				this.gameObject.SetActive (false);
			}
			destroyTime = destroyTime - Time.fixedDeltaTime;
		}
	}
}
