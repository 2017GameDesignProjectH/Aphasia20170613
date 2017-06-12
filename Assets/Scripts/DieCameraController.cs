using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DieCameraController : MonoBehaviour {

    public Image BlackCover;
    private float timer = 0;
	
	// Update is called once per frame
	void Update () {
        if (timer <= 1)
        {
            timer += 0.016f;
            BlackCover.color = new Color(0, 0, 0, 1-timer);
        }/* else if(timer>1 && timer <= 3)
        {
            timer += 0.016f;
        }
        else
        {
            timer += 0.016f;
            BlackCover.color = new Color(0, 0, 0, timer-3);
        }*/

    }

	public void Reset(){
		BlackCover.color = new Color (0, 0, 0, 0);
		timer = 0f;
	}
}
