using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillController : MonoBehaviour {

	public GameObject boss;
	public GameObject shell;

	public PlayerSkill[] playerSkills;
	public AudioSource stageMusic;

	public PlayerController playerController;
	public GameObject moz;
	public GameObject camera;

	public GameObject skills;

	public bool isActive = false;
	public bool isMode = false;
	public int waveNum;

	public int skillCount;
	public int skillIndex;

	public float skillNoticeTime = 2.5f;
	public float skillEndTime = 1.0f;

	// Update is called once per frame
	void FixedUpdate () {
		if (isActive) {
			if (isMode) {
				playerController.enabled = false;
				moz.GetComponent<Rigidbody> ().velocity = Vector3.zero;
			} else {
				playerController.enabled = true;
			}
			while (!isMode && waveNum < playerSkills.Length && playerSkills [waveNum].PrepareTime <= stageMusic.time) {
				isMode = true;
				skillCount = playerSkills [waveNum].SkillList.Count;
				skillIndex = 0;
				shell.SetActive (true);
				shell.transform.localScale = new Vector3(12f, 12f, 0f);
				

			}

			while (isMode && skillIndex < skillCount && (float)(double)playerSkills [waveNum].SkillList [skillIndex] ["Time"] <= stageMusic.time + skillNoticeTime) {
				GameObject obj = (GameObject)Instantiate (Resources.Load ("Prefabs/Shapes/" + (string)playerSkills [waveNum].SkillList [skillIndex] ["Shape"]));
				obj.gameObject.name = (string)playerSkills [waveNum].SkillList [skillIndex] ["Shape"];
				obj.transform.parent = skills.transform;
				obj.transform.position = boss.transform.position + new Vector3(0f,2f,0f);
				obj.GetComponent<Shapes> ().Set (skillNoticeTime, moz.transform);

				skillIndex = skillIndex + 1;
				shell.transform.localScale = new Vector3 (12f * (1f - (float)skillIndex / (float)skillCount), 12f * (1f - (float)skillIndex / (float)skillCount), 0f);
			}

			if (isMode && skillIndex == skillCount) {
				if ((float)(double)playerSkills [waveNum].SkillList [skillIndex - 1] ["Time"] <= stageMusic.time - skillEndTime) {
					foreach (Transform child in skills.transform) {
						Destroy (child.gameObject);
					}
					if (playerSkills [waveNum].Mode == "Back") {
						shell.SetActive (false);
						isMode = false;
					}
					if (playerSkills [waveNum].Mode == "Continue") {
						skillIndex = 0;
						skillCount = playerSkills [waveNum + 1].SkillList.Count;
						shell.transform.localScale = new Vector3 (12f, 12f, 0f);
					}
					if (playerSkills [waveNum].Mode == "End") {
						Time.timeScale = 0f;
					}

					waveNum = waveNum + 1;
				}
			}
		}
	}

	public void Initializing(StageInfo stageInfo, AudioSource music){
		playerSkills = stageInfo.PlayerSkills;
		this.stageMusic = music;
		isActive = true;
		waveNum = 0;
	}

	public void Reset(){
		foreach (Transform child in skills.transform) {
			Destroy (child.gameObject);
		}
		waveNum = 0;
		while (waveNum < playerSkills.Length && playerSkills [waveNum].PrepareTime <= stageMusic.time) {
			waveNum = waveNum + 1;
		}
		isMode = false;
		shell.SetActive (false); // shell >> prefabs >> parent >> destroy
	}
}
