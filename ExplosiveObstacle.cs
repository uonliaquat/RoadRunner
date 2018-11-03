using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveObstacle : MonoBehaviour {

    public GameObject explosionPrefab;
    public int damage = 20;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player"){
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            collision.gameObject.GetComponent<PlayerHealth>().ApplyDamage(damage);

            gameObject.SetActive(false);
        }
        if(collision.gameObject.tag == "Bullet"){
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
