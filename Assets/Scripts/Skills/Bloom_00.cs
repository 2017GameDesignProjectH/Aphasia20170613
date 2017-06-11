using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class Bloom_00 : MonoBehaviour {

	public JsonData _skillInfo;

	public string target;
	public float delayTime = 9999f;
	public bool Initialize = false;

	public GameObject notice;

	public void SetSkillInfo(JsonData skillInfo){
		_skillInfo = skillInfo;
		this.target = (string)skillInfo ["Target"];
		SetPosition ();
		this.transform.position += new Vector3 ((float)(double)skillInfo ["Position"] [0], (float)(double)skillInfo ["Position"] [1], (float)(double)skillInfo ["Position"] [2]);
		this.delayTime = (float)(double)skillInfo ["DelayTime"];
		this.Initialize = true;
	}

	void FixedUpdate(){
		if (Initialize) {
			if (delayTime <= 0f) {
				GameObject obj = (GameObject)Instantiate (Resources.Load ("Prefabs/Skills/Ring_00"));
				obj.SendMessage ("SetSkillInfo", _skillInfo ["Call_Ring_00"]);
				obj.transform.position = this.transform.position;
				notice.gameObject.SetActive (false);
				Initialize = false;
			}
			delayTime = delayTime - Time.fixedDeltaTime;
		}
	}

	void SetPosition(){
		if (target == "Boss") {
			this.transform.position = GameObject.Find ("BossController").transform.position;
		} else if (target == "Player") {
			this.transform.position = GameObject.Find ("PlayerController").transform.position;
		}
	}

}
