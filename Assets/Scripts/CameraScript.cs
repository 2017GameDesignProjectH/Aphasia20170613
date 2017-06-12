using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Transform Player;
    public Transform Boss;
    public float BossPlayerDistance;
    public float minX;
    public float maxX;
    public float midX;
    public float minY;
    public float maxY;
    // public float minZ;
    // public float maxZ;
    public float updateY;
    public float updateZ;
    public float updateX;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        BossPlayerDistance = (Boss.position - Player.position).magnitude;

        if (Player.transform.position.x >= minX && Player.transform.position.x < maxX)
        {
            updateX = midX;
        }
        else if (Player.transform.position.x < minX)
        {
            updateX = midX - (minX - Player.transform.position.x);
        } else
        {
            updateX = midX + (Player.transform.position.x - maxX);
        }

        updateY = Mathf.Clamp(BossPlayerDistance / 3, minY, maxY);

        if (Boss.transform.position.z >= Player.transform.position.z)
        {
            updateZ = Player.transform.position.z - 10;
        } else
        {
            updateZ = Player.transform.position.z - 10 - (Player.transform.position.z - Boss.transform.position.z)/2;
        }

        this.transform.position = new Vector3(updateX, updateY, updateZ);
        // this.transform.LookAt((Boss.position + Player.position*2) / 3);
    }
}
