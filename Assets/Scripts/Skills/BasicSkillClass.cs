using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class BasicSkillClass : MonoBehaviour {

	public string target = "None";

	public bool Initialization = false;
	public float destroyTime = 0f;

	public void SetPositionByTarget(){
		if (target == "Boss") {
			this.transform.position = GameObject.Find ("BossController").transform.position;
		} else if (target == "Player") {
			this.transform.position = GameObject.Find ("PlayerController").transform.position;
		}
	}
}
