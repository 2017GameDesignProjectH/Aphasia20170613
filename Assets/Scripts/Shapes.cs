using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shapes : MonoBehaviour {

	public float _maxTime;
	public float _timer;
	public Transform _target;
	public Vector3 _dir;

	// Update is called once per frame
	void FixedUpdate () {
		this.transform.position += _dir * Time.fixedDeltaTime / _maxTime;
		_timer = _timer - Time.fixedDeltaTime;
	}

	public void Set(float timer, Transform target){
		_maxTime = timer + 0.5f;
		_timer = timer;
		_target = target;
		_dir = target.position - this.transform.position;
	}
}
