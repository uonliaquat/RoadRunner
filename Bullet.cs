using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {


    [SerializeField]
    private Rigidbody myBody;
	
    public void Move(float speed){
        myBody.AddForce(transform.forward.normalized * speed);
        Invoke("DeactivateGameObject", 5f);

    }

    private void Update()
    {
        
    }

    void DeactivateGameObject(){
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Obstacle"){
            print("collided with obstacle");
            DeactivateGameObject();
        }
    }
}
