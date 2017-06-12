using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour {

    public GameObject player;
    public GameObject mainCamera;
    public Animator PlayerAnimator;
    public JumpSensor JumpSensor;
    public float jumpspeed;
    public float movespeed;

    private bool isHoriMove;
    private bool isVertiMove;
    private Vector3 horiVelocity;
    private Vector3 vertiVelocity;

    //FOR velocity in jump
    private float YVelocity;
    //FOR Continue Movement
    private float timer;

    void Die()
    {
        Debug.Log("Dieee!");
        /*PlayerAnimator.SetTrigger("Die");
        player.GetComponent<CapsuleCollider>().enabled = false;
        player.GetComponent<Rigidbody>().useGravity = false;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.enabled = false;*/
    }

    void Draw()
    {
        if (this.isHoriMove && this.isVertiMove)
        {
            player.GetComponent<Rigidbody>().velocity = this.horiVelocity + this.vertiVelocity;
        }
        else if (this.isHoriMove)
        {
            player.GetComponent<Rigidbody>().velocity = this.horiVelocity;
        }
        else if (this.isVertiMove)
        {
            player.GetComponent<Rigidbody>().velocity = this.vertiVelocity;
        }

        if (this.isHoriMove || this.isVertiMove)
        {
            // Caculate the player's degree from velocity
            float rotate = Mathf.Atan2(player.GetComponent<Rigidbody>().velocity.x, player.GetComponent<Rigidbody>().velocity.z);
            player.transform.rotation = Quaternion.Euler(0, rotate / Mathf.PI * 180, 0);

            // Let player move continue a little bit
            this.timer = 0.1f;
        }
        if (this.timer >= 0)
        {
            this.timer -= Time.deltaTime;
        }
        else
        {
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        // Reset Y axis velocity to origin
        //player.GetComponent<Rigidbody>().velocity = new Vector3(player.GetComponent<Rigidbody>().velocity.x, this.YVelocity, player.GetComponent<Rigidbody>().velocity.z);
        PlayerAnimator.SetFloat("velocity", player.GetComponent<Rigidbody>().velocity.magnitude);
    }

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update () {
        // GET INPUT AXIS
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        // GET DIRECTION
        Vector3 forward = mainCamera.transform.forward;
        Vector3 right = mainCamera.transform.right;
        //Calibrate Y AXIS 
        forward.y = 0;
        right.y = 0;
        // GET DIRECTION UNIT VECTOR
        forward.Normalize();
        right.Normalize();

        //RESET ROTATION 
        this.isHoriMove = false;
        this.isVertiMove = false;
        this.horiVelocity = Vector3.zero;
        this.vertiVelocity = Vector3.zero;

        this.YVelocity = player.GetComponent<Rigidbody>().velocity.y;

        // check jump
        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Joystick1Button1)) && JumpSensor.IsCanJump())
        {
            YVelocity = jumpspeed;
            PlayerAnimator.SetFloat("jumpspeed", 1);
        }
        else
        {
            PlayerAnimator.SetFloat("jumpspeed", 0);

        }

        if (vertical > 0)
        {
            this.vertiVelocity = forward * movespeed;
            isVertiMove = true;

        }
        else if (vertical < 0)
        {
            this.vertiVelocity = -forward * movespeed;
            isVertiMove = true;
        }

        if (horizontal > 0)
        {
            this.horiVelocity = right * movespeed;
            isHoriMove = true;
        }
        else if (horizontal < 0)
        {
            this.horiVelocity = -right * movespeed;
            isHoriMove = true;
        }

        // perform the result on charactor
        Draw();

    }
}
