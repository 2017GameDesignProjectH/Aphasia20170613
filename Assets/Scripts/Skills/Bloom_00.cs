using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class Bloom_00 : BasicSkillClass {

	public JsonData _skillInfo;

	public float delayTime = 9999f;

	public void SetSkillInfo(JsonData skillInfo){
		this.target = (string)skillInfo ["Target"];
		this.destroyTime = (float)(double)skillInfo ["DestroyTime"];
		SetPositionByTarget ();
		this.transform.position += new Vector3 ((float)(double)skillInfo ["Position"] [0], (float)(double)skillInfo ["Position"] [1], (float)(double)skillInfo ["Position"] [2]);

		_skillInfo = skillInfo;
		this.delayTime = (float)(double)skillInfo ["DelayTime"];

		this.Initialization = true;
	}

	void FixedUpdate(){
		if (Initialization) {
			if (delayTime <= 0f) {
				GameObject obj = (GameObject)Instantiate (Resources.Load ("Prefabs/Skills/Ring_00"));
				obj.SendMessage ("SetSkillInfo", _skillInfo ["Call_Ring_00"]);
				obj.transform.position = this.transform.position;
			}
			delayTime = delayTime - Time.fixedDeltaTime;
			if (destroyTime < 0f) {
				this.gameObject.SetActive (false);
			}
			destroyTime = destroyTime - Time.fixedDeltaTime;
		}
	}


}
