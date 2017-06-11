using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class Ring_01 : BasicSkillClass {
	
	public float rotateSpeed;
	public int ringGapNum = 0;

	public void SetSkillInfo(JsonData skillInfo){
		this.target = (string)skillInfo ["Target"];
		this.destroyTime = (float)(double)skillInfo ["DestroyTime"];
		SetPositionByTarget ();
		this.transform.position += new Vector3 ((float)(double)skillInfo ["Position"] [0], (float)(double)skillInfo ["Position"] [1], (float)(double)skillInfo ["Position"] [2]);
		this.transform.eulerAngles += new Vector3 ((float)(double)skillInfo ["Rotation"] [0], (float)(double)skillInfo ["Rotation"] [1], (float)(double)skillInfo ["Rotation"] [2]);

		this.ringGapNum = (int)skillInfo ["RingGapNum"];
		for (int i = 0; i < this.transform.childCount; i++) {
			this.transform.GetChild (i).GetComponent<Note> ().velocity = new Vector3 (
				Mathf.Sin (((2f/ringGapNum) * ((int)skillInfo ["RingRange"] / (360f/ringGapNum)) * (i / ringGapNum) / (this.transform.childCount / ringGapNum) + (float)(i % ringGapNum) * (2f/ringGapNum)) * Mathf.PI),
				0f,
				Mathf.Cos (((2f/ringGapNum) * ((int)skillInfo ["RingRange"] / (360f/ringGapNum)) * (i / ringGapNum) / (this.transform.childCount / ringGapNum) + (float)(i % ringGapNum) * (2f/ringGapNum)) * Mathf.PI)
			) * (float)(double)skillInfo ["Velocity"];
		}
		this.rotateSpeed = (float)(double)skillInfo ["RotateSpeed"];

		this.Initialization = true;
	}

	void FixedUpdate(){
		if (Initialization) {
			this.transform.Rotate (new Vector3 (0, rotateSpeed, 0) * Time.fixedDeltaTime);
			if (destroyTime < 0f) {
				this.gameObject.SetActive (false);
			}
			destroyTime = destroyTime - Time.fixedDeltaTime;
		}
	}
}
