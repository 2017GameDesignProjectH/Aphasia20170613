using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

	public BossSkill[] bossSkills;
	public AudioSource stageMusic;

	public GameObject skills;

	public bool isActive = false;
	public int skillNum;
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isActive) {
			while (skillNum < bossSkills.Length && bossSkills [skillNum].Time <= stageMusic.time) {
				GameObject obj = (GameObject)Instantiate (Resources.Load ("Prefabs/Skills/" + bossSkills [skillNum].SkillName));
				obj.SendMessage ("SetSkillInfo", bossSkills [skillNum].SkillInfo);
				obj.transform.parent = skills.transform;
				skillNum = skillNum + 1;
			}
		}
	}

	public void Initializing(StageInfo stageInfo, AudioSource music){
		bossSkills = stageInfo.BossSkills;
		this.stageMusic = music;
		isActive = true;
		skillNum = 0;
	}

	public void Reset(){
		skillNum = 0;
		while (skillNum < bossSkills.Length && bossSkills [skillNum].Time <= stageMusic.time) {
			skillNum = skillNum + 1;
		}
		foreach (Transform child in skills.transform) {
			Destroy (child.gameObject);
		}
	}
}
