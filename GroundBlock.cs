using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBlock : MonoBehaviour {

    public float halfLength = 100f;
    private Transform player;
    private float endOffSet = 10f;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        MoveGround();
	}

    void MoveGround(){
        if(transform.position.z + halfLength < player.transform.position.z - endOffSet){
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + halfLength * 4);
            GameplayController.obstacleCount = 0;
            //GameplayController.obstacleParent.SetActive(false);
        }
    }
}
