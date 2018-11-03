using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour {

    private Transform target;
    public float distance  = 6.3f;
    //public float height = 3.5f;
    //public float height_Damping = 3.25f;
    //public float rotation_Damping = 0.27f;


	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("Player").transform;
	}

    private void LateUpdate()
    {
        FollowPlayer();   
    }


    void FollowPlayer(){
        Vector3 camera_position = transform.position;
        camera_position.z = target.position.z - distance;
        camera_position.x = target.position.x;
        transform.position = camera_position;
    }
}
