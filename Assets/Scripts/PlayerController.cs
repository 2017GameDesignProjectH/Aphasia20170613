using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour {

    public GameController gameController;
    public GameObject player;
    public GameObject mainCamera;
    public GameObject DieCamera;
    public Animator PlayerAnimator;
    public JumpSensor JumpSensor;
    public float jumpspeed;
    public float movespeed;
    public float rollspeed;
    public float rolltime;
    public float minRollPeriod;
    public float minJumpPeriod;
	public bool rollImmune;

    private float speedtemp;
    private float rollCD;
    private float rolltimeCounter;
    private float jumpCounter;
    private bool isImmune;
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
        if (isImmune)
        {
            return;
        }
        Debug.Log("Die!");
        PlayerAnimator.SetTrigger("Die");
        player.GetComponent<CapsuleCollider>().enabled = false;
        player.GetComponent<Rigidbody>().useGravity = false;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        mainCamera.SetActive(false);
        DieCamera.SetActive(true);
        gameController.GameOver();
        this.enabled = false;
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
        player.GetComponent<Rigidbody>().velocity = new Vector3(player.GetComponent<Rigidbody>().velocity.x,
            this.YVelocity, player.GetComponent<Rigidbody>().velocity.z);
        PlayerAnimator.SetFloat("velocity", player.GetComponent<Rigidbody>().velocity.magnitude);
    }

    // Use this for initialization
    void Start () {
        speedtemp = movespeed;
        isImmune = false;
	}

    // Update is called once per frame
    void Update () {
        // GET INPUT AXIS
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

		if (Input.GetKeyDown (KeyCode.Joystick1Button4) || Input.GetKeyDown(KeyCode.Alpha8)) {
			rollImmune = !rollImmune;
		}
        // 翻滾
        if (rollCD > 0)
        {
            rollCD -= Time.deltaTime;
        } else if ((Input.GetKeyDown(KeyCode.Mouse1)||(Input.GetKeyDown(KeyCode.Joystick1Button5))) && JumpSensor.IsCanJump())
        {
            movespeed = rollspeed;
            PlayerAnimator.SetTrigger("Dash");
            rollCD = minRollPeriod;
            rolltimeCounter = rolltime;
            if (rollImmune)
            {
                isImmune = true;
            }
        }
        // 翻滾持續時間
        if (rolltimeCounter > 0)
        {
            rolltimeCounter -= Time.deltaTime;
        } else
        {
            movespeed = speedtemp;
            isImmune = false;
        }

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

        if (jumpCounter > 0)
        {
            jumpCounter -= Time.deltaTime;
        } else if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button1)) && JumpSensor.IsCanJump())
        {
            YVelocity = jumpspeed;
            PlayerAnimator.SetFloat("jumpspeed", 1);
            jumpCounter = minJumpPeriod;
        } else
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

	public void Reset(){
		player.GetComponent<CapsuleCollider>().enabled = true;
		player.GetComponent<Rigidbody>().useGravity = true;
		player.GetComponent<Rigidbody>().velocity = Vector3.zero;
		mainCamera.SetActive(true);
		DieCamera.GetComponent<DieCameraController> ().Reset ();
		DieCamera.SetActive(false);
		PlayerAnimator.Play ("Idle");
		this.enabled = true;
	}
}
