using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsController : MonoBehaviour {
    
    //-----VARIABLES-----

    public int maxHealth;
    private float currentHealth;
    public float CurrentHealth { get; }

    //-----METHODS-----

    //Setup method
    public void Initialise() {
        currentHealth = maxHealth;
    }

    //Deal damage and trigger death in zero health
    public void Damage (int amount) {
        currentHealth -= amount;
        if (currentHealth <= 0) {
            GetComponent<DeathHandler>().Die();
        }
    }

}
