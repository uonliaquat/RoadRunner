using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour {
    public GameObject bloodFXPrefab;
    private float speed = 1f;
    private bool isAlive;

    private Rigidbody myBody;
	// Use this for initialization
	void Start () {
        speed = Random.Range(1, 5);
        myBody = GetComponent<Rigidbody>();
        isAlive = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (isAlive)
        {
            myBody.velocity = new Vector3(0f, 0f, -speed);
        }
        if(transform.position.y < -10f){
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
	}

    void Die(){
        isAlive = false;
        myBody.velocity = Vector3.zero;
        GetComponent<Collider>().enabled = false;
        GetComponentInChildren<Animator>().Play("idle");
        transform.rotation = Quaternion.Euler(90f, 0, 0);
        transform.localScale = new Vector3(1f, 1f, 0.2f);
        transform.position = new Vector3(transform.position.x, 0.2f, transform.position.z);
    }

    void DeactivateGameObject(){
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Bullet"){
            Instantiate(bloodFXPrefab, transform.position, Quaternion.identity);
            Invoke("DeactivateGameObject", 3f);

            //IncreaseScore
            GameplayController.instance.IncreaseScore();
            Die();
        }
    }
}
