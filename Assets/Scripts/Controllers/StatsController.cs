using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsController : MonoBehaviour {
    
    public int maxHealth;
    [HideInInspector]
    public float currentHealth;


    public void Initialise() {
        currentHealth = maxHealth;
    }

    public void Damage (int amount) {
        currentHealth -= amount;
        if (currentHealth <= 0) {
            GetComponent<ShipController>().Die();
        }
    }

}
