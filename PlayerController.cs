using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : BaseController {

    private Rigidbody myBody;
    public Transform bullet_startPoint;
    public GameObject bullet_prefab;
    public ParticleSystem shootFX;
    private Animator shootSliderAnim;

    [HideInInspector]
    public bool canShoot;


    // Use this for initialization
    void Start () {
        myBody = GetComponent<Rigidbody>();
        GameObject.Find("ShootBtn").GetComponent<Button>().onClick.AddListener(ShootingControl);
        canShoot = true;
        shootSliderAnim = GameObject.Find("Fire Bar").GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        ControlMovementWithKeyBoard();
        ChangeRotation();
	}

    private void FixedUpdate()
    {
        MoveTank();
    }
    void MoveTank(){
        myBody.MovePosition(myBody.position + speed * Time.deltaTime);
        //myBody.velocity = new Vector3(0f, 0f, z_Speed);
    }

    void ControlMovementWithKeyBoard(){
        if(Input.GetKey(KeyCode.LeftArrow)){
            MoveLeft();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight();
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            MoveFast();
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            MoveSlow();
        }
        if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)){
            MoveStraight();
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            MoveNormal();
        }
    }

    void ChangeRotation(){
        if(speed.x > 0){
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, maxAngle, 0f), Time.deltaTime * rotationSpeed);
        }
        else if (speed.x < 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, maxAngle - 40f, 0f), Time.deltaTime * rotationSpeed);
        }
        else{
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, -180, 0f), Time.deltaTime * rotationSpeed);
        }
    }

    public void ShootingControl(){
        if(Time.timeScale != 0){
            if(canShoot){
                GameObject bullet = Instantiate(bullet_prefab, bullet_startPoint.position, Quaternion.identity);
                bullet.GetComponent<Bullet>().Move(2000f);
                shootFX.Play();
                canShoot = false;
                //call the anim
                shootSliderAnim.Play("Fill");
            }
        }
    }
}
