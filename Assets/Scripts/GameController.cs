using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public BattleDataLoding battleDataLoading;
	public AudioSource stageMusic;

	public BossController bossController;

	public float musicTime;

	// Use this for initialization
	void Start () {
		battleDataLoading.LoadingStage ("PIANO.json");
		Debug.Log (battleDataLoading.stageInfo.Music);
		stageMusic.clip = (AudioClip)Resources.Load ("Sounds/" + battleDataLoading.stageInfo.Music);
		stageMusic.Play ();
		bossController.Initializing (battleDataLoading.stageInfo, stageMusic);
		stageMusic.time = 75f;
	}
	
	// Update is called once per frame
	void Update () {
		musicTime = stageMusic.time;
	}

	public void StageStart(){
		battleDataLoading.LoadingStage ("PIANO.json");
		Debug.Log (battleDataLoading.stageInfo.Music);
		stageMusic.clip = (AudioClip)Resources.Load ("Sounds/" + battleDataLoading.stageInfo.Music);
		stageMusic.Play ();
		bossController.Initializing (battleDataLoading.stageInfo, stageMusic);
	}
}
