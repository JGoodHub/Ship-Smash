using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsController : MonoBehaviour {
    
    //-----VARIABLES-----

    public int maxHealth;
    [HideInInspector]
    public float currentHealth;

    //-----METHODS-----

    //Setup method
    public void Initialise() {
        currentHealth = maxHealth;
    }

    //Deal damage and trigger death in zero health
    public void Damage (int amount) {
        currentHealth -= amount;
        if (currentHealth <= 0) {
            GetComponent<ShipController>().Die();
        }
    }

}
