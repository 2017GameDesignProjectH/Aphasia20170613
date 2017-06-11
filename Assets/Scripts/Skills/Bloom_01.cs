using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class Bloom_01 : BasicSkillClass {

	public JsonData _skillInfo;
	public float delayTime = 0f;
	public float rotateSpeed;
	public float callDeltaTime = 0f;
	public int callNum = 0;

	public float timer = 0f;

	public void SetSkillInfo(JsonData skillInfo){
		this.target = (string)skillInfo ["Target"];
		this.destroyTime = (float)(double)skillInfo ["DestroyTime"];
		this.SetPositionByTarget ();
		this.transform.position += new Vector3 ((float)(double)skillInfo ["Position"] [0], (float)(double)skillInfo ["Position"] [1], (float)(double)skillInfo ["Position"] [2]);

		this._skillInfo = skillInfo;
		this.rotateSpeed = (float)(double)skillInfo ["RotateSpeed"];
		this.callDeltaTime = (float)(double)skillInfo ["CallDeltaTime"];
		this.callNum = (int)skillInfo ["CallNum"];

		this.Initialization = true;
	}

	void FixedUpdate(){
		if (Initialization) {
			if (delayTime <= 0f) {
				this.transform.Rotate (new Vector3 (0, rotateSpeed, 0) * Time.fixedDeltaTime);
				if (timer <= 0f && callNum > 0) {
					GameObject obj = (GameObject)Instantiate (Resources.Load ("Prefabs/Skills/Ring_00"));
					obj.SendMessage ("SetSkillInfo", _skillInfo ["Call_Ring_00"]);
					obj.transform.position = this.transform.position;
					obj.transform.rotation = this.transform.rotation;
					timer = callDeltaTime;
					callNum = callNum - 1;
				} else {
					timer = timer - Time.fixedDeltaTime;
				}
			} else {
				delayTime = delayTime - Time.fixedDeltaTime;
			}
			if (destroyTime < 0f) {
				this.gameObject.SetActive (false);
			}
			destroyTime = destroyTime - Time.fixedDeltaTime;
		}
	}
}
