using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	[SerializeField]
	private UnityEvent onStart;

	// Use this for initialization
	void Start () {
		this.onStart.Invoke ();
	}


	public void StartGame(){
		SceneManager.LoadScene ("Demo");
	}

}
