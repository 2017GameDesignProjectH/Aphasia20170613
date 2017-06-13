using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
	public Canvas endUI;
	public bool end = false;

	public float latestTime = 0f;
	public float[] savingPoint;
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

	void Start(){
		Time.timeScale = 1f;
	}

	// Update is called once per frame
	void Update () {
		musicTime = stageMusic.time;
		if (!end && stageMusic.time >= 198f) {
			endUI.gameObject.SetActive (true);
			end = true;
		}
		if (stageMusic.time >= 221f) {
			SceneManager.LoadScene ("Menu");
		}
		if (Input.GetKeyDown (KeyCode.JoystickButton6) || Input.GetKeyDown(KeyCode.Alpha9)) {
			playerSkillController.enabled = !playerSkillController.enabled;
		}
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
		//stageMusic.time = 50f;
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			StageStart ();
		}
	}

	public void Reset(){
		stageMusic.Play ();
		for (int i = 0; i < savingPoint.Length; i++) {
			if (savingPoint [i] > latestTime) {
				stageMusic.time = savingPoint [i - 1];
				break;
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
	}
}
