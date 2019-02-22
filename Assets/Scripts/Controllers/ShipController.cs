using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {
    
    private MovementController movementController;
    public MovementController MovementController { get => movementController; }

    private ShootController shootController;
    public ShootController ShootController { get => shootController; }

    private StatsController statsController;
    public StatsController StatsController { get => statsController; }

    public GameObject deathExplosionPrefab;

    public virtual void Initialise () {
        movementController = GetComponent<MovementController>();
        movementController.Initialise();

        shootController = GetComponent<ShootController>();
        
        statsController = GetComponent<StatsController>();
        statsController.Initialise();
    }

    public virtual void Die () {
        GameObject explosionInstance = Instantiate(deathExplosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosionInstance, 1f);
        Destroy(gameObject);
    }

}
