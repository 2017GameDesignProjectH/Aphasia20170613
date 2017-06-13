using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Transform Player;
    public Transform Boss;
    public float BossPlayerDistance;
    public float minY;
    public float maxY;
    public float minDis;
    public float maxDis;

    public Vector3 midAngle;
    public Vector3 currentAngle;
    public float updateY;
    public float updateZ;
    public float updateX;

    // Use this for initialization
    void Start () {
        midAngle = Player.position - Boss.position;
        midAngle.y = 0;
        midAngle = midAngle.normalized;
    }
	
	// Update is called once per frame
	void Update () {
        BossPlayerDistance = (Boss.position - Player.position).magnitude;
        currentAngle = Player.position - Boss.position;
        currentAngle.y = 0;
        currentAngle = currentAngle.normalized;
        float angle = Vector3.Dot(midAngle, currentAngle);
        Vector3 cross = Vector3.Cross(midAngle, currentAngle);
        cross = cross.normalized;
        double theta = Mathf.Acos(angle);
        double degree = theta*Mathf.Rad2Deg;
        float pos = cross.y;
        // Debug.Log(cross);
        // Debug.Log(degree);
        // Debug.Log(pos);

        if (degree > 18)
        {
           float d = pos * (float)(degree - 18);
           midAngle = Quaternion.Euler(0, d, 0) * midAngle;
        }

        /*
        float rotate = Input.GetAxis("RightJoystick");
        Debug.Log(rotate);
        if (rotate!=0)
        {
           midAngle = Quaternion.Euler(0, rotate, 0) * midAngle;
           rotate = 0;
        }*/

        Vector3 midposition = Boss.transform.position + midAngle * Mathf.Clamp(BossPlayerDistance * 1.25f, minDis, maxDis);
        updateY = Mathf.Clamp(BossPlayerDistance / 4, minY, maxY);
        this.transform.position = midposition;
        this.transform.position = new Vector3(this.transform.position.x, updateY, this.transform.position.z);
        this.transform.LookAt((Boss.position + Player.position * 2) / 3);

        /*
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
        
        */
    }
}
