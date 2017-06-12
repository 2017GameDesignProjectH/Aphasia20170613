using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public BattleDataLoding battleDataLoading;
	public AudioSource stageMusic;

	public BossController bossController;
	public PlayerController playerController;
	public PlayerSkillController playerSkillController;
	public GameObject moz;

	public GameObject barrier;

	public float musicTime;

	public AudioSource DieMusic;

	public Canvas continueUI;

	public float latestTime = 0f;
	public float[] savingPoint = new float[]{ 0f, 90f };
	public Vector3 rebornPosition = new Vector3(0f,0f,-23f);

    public void GameOver(){
		latestTime = stageMusic.time;
		stageMusic.Stop ();
		DieMusic.clip = (AudioClip)Resources.Load ("Sounds/DeathSound");
		DieMusic.Play ();
		Time.timeScale = 0;
		continueUI.gameObject.SetActive (true);
		continueUI.GetComponent<MenuController> ().Invoke ();
	}

	// Use this for initialization
	/*
	void Start () {
		battleDataLoading.LoadingStage ("PIANO.json");
		Debug.Log (battleDataLoading.stageInfo.Music);
		stageMusic.clip = (AudioClip)Resources.Load ("Sounds/" + battleDataLoading.stageInfo.Music);
		stageMusic.Play ();
		bossController.Initializing (battleDataLoading.stageInfo, stageMusic);
		//stageMusic.time = 130f;
	}
*/

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
		playerSkillController.Initializing (battleDataLoading.stageInfo, stageMusic);
		barrier.gameObject.SetActive (true);
		this.GetComponent<SphereCollider> ().enabled = false;
		//stageMusic.time = 40f;
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			StageStart ();
		}
	}

	public void Reset(){
		for (int i = 0; i < savingPoint.Length; i++) {
			if (savingPoint [i] > latestTime) {
				stageMusic.time = savingPoint [i - 1];
			}
			if (i == savingPoint.Length - 1) {
				stageMusic.time = savingPoint [i];
			}
		}
		bossController.Reset ();
		playerController.Reset ();
		playerSkillController.Reset ();
		continueUI.gameObject.SetActive (false);
		moz.transform.position = rebornPosition;
		Time.timeScale = 1f;
		stageMusic.Play ();
	}
}
