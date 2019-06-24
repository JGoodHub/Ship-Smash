using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsController : MonoBehaviour {
    
    //-----VARIABLES-----

    public int maxHealth;
    private float currentHealth;
    public float CurrentHealth { get => currentHealth; }

    public int maxShield;
    private float currentShield;
    public float CurrentShield { get => currentShield; }

    public float passiveShieldRegenRate;
    public float shieldDownDuration;
    private float shieldDownTimer = 0;

    public float activeShieldRegenRate;
    public float activeRegenCooldown;
    private float activeRegenTimer = 0;

    //-----METHODS-----

    //Setup method
    public void Initialise() {
        currentHealth = maxHealth;
        currentShield = maxShield;
    }

    void Update () {
        activeRegenTimer -= Time.deltaTime;
        shieldDownTimer -= Time.deltaTime;

        if (currentShield > 0) {
            if (activeRegenTimer <= 0) {
                currentShield += activeShieldRegenRate * Time.deltaTime;
            } else {
                currentShield += passiveShieldRegenRate * Time.deltaTime;
            }
        } else if (shieldDownTimer <= 0) {
            currentShield = maxShield * 0.5f;            
        }

        UIManager.instance.UpdatePlayerStats();
    }

    //Deal damage and trigger death in zero health
    public void Damage (int amount) {
        activeRegenTimer = activeRegenCooldown;

        if (currentShield > 0) {
            currentShield = Mathf.Clamp(currentShield - amount, 0, maxShield);

            if (currentShield <= 0) {
                shieldDownTimer = shieldDownDuration;
            }
        } else {
            currentHealth -= amount;
            if (currentHealth <= 0) {
                GetComponent<DeathHandler>().Die();
            }
        }
    }

}
