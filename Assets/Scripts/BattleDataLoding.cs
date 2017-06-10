using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;

public class BattleDataLoding : MonoBehaviour {

	public StageInfo stageInfo;

	public void LoadingStage(string stageName){
		GetStageInfo (stageName);
	}

	void GetStageInfo(string stageName){
		string DPath = Application.dataPath;
		int num = DPath.LastIndexOf ("/");
		DPath = DPath.Substring (0, num);
		string stagePath = DPath + "/StageInfo/" + stageName;
		if (File.Exists (stagePath)) {
			StageDataSplit (stagePath);
		} else {
			Debug.Log ("No such file!");
		}
	}

	void StageDataSplit(string stagePath){
		string jsonString;
		JsonData jsonData;
		jsonString = File.ReadAllText (stagePath);
		jsonData = JsonMapper.ToObject (jsonString);
		stageInfo.StageName = (string)jsonData ["Name"];
		stageInfo.Music = (string)jsonData ["Music"];
		//Debug.Log (jsonData ["BossSkills"].Count);
		stageInfo.BossSkills = new BossSkill[jsonData ["BossSkills"].Count];
		for (int i = 0; i < jsonData ["BossSkills"].Count; i++) {
			stageInfo.BossSkills [i] = new BossSkill (
				(float)(double)jsonData ["BossSkills"] [i] ["Time"],
				(string)jsonData ["BossSkills"] [i] ["Skill"],
				jsonData["BossSkills"][i]["SkillInfo"]
			);
		}
	}

}

[System.Serializable]
public class StageInfo{
	public StageInfo (){}
	public StageInfo(string stageName, string music){
		StageName = stageName;
		Music = music;
	}
	public string StageName;
	public string Music;
	public BossSkill[] BossSkills;
}

[System.Serializable]
public class BossSkill{
	public BossSkill(){}
	public BossSkill(float time, string skillName, JsonData skillInfo){
		Time = time;
		SkillName = skillName;
		SkillInfo = skillInfo;
	}
	public float Time;
	public string SkillName;
	public JsonData SkillInfo;
}