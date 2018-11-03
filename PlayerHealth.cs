using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public int healthValue = 100;
    private Slider healthSlider;
    private GameObject UI_Holder;
	// Use this for initialization
	void Start () {
        healthSlider = GameObject.Find("Health Bar").GetComponent<Slider>();
        healthSlider.value = healthValue;
        UI_Holder = GameObject.Find("UI Holder");
	}
	
    public void ApplyDamage(int damageAmount){
        healthValue -= damageAmount;
        if(healthValue <  0){
            healthValue = 0;
        }
        healthSlider.value = healthValue;
        if(healthValue == 0){
            UI_Holder.SetActive(false);
            GameplayController.instance.GameOver();
        }
    }
}
