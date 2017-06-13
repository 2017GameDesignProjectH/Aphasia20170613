using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class End : MonoBehaviour {

	public Image whiteScreen;

	public float timer = 0;

	// Update is called once per frame
	void Update () {
		if (timer <= 1) {
			timer += Time.deltaTime;
			whiteScreen.color = new Color (255, 255, 255, timer);
		}

		/* else if(timer>1 && timer <= 3)
        {
            timer += 0.016f;
        }
        else
        {
            timer += 0.016f;
            BlackCover.color = new Color(0, 0, 0, timer-3);
        }*/

	}
}
