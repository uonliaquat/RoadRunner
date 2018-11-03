using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour {

    public float timer = 3f;

    private void Start()
    {
        Invoke("DeactivateGameObject", timer);
    }

    void DeactivateGameObject(){
        gameObject.SetActive(false);
    }

}
