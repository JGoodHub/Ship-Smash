using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {

    //-----VARIABLES-----
    
    private MovementController movementController;
    public MovementController MovementController { get => movementController; }

    private ShootController shootController;
    public ShootController ShootController { get => shootController; }

    private StatsController statsController;
    public StatsController StatsController { get => statsController; }

    public GameObject deathExplosionPrefab;

    //-----METHODS-----

    //Setup method
    public virtual void Initialise () {
        movementController = GetComponent<MovementController>();
        movementController.Initialise();

        shootController = GetComponent<ShootController>();
        
        statsController = GetComponent<StatsController>();
        statsController.Initialise();
    }

    //Create a large explosion upon death
    public virtual void Die () {
        GameObject explosionInstance = Instantiate(deathExplosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosionInstance, 1f);
        Destroy(gameObject);
    }

    //-----GIZMOS-----
    public bool drawGizmos;
    void OnDrawGizmos () {
        if (drawGizmos) {
            
        }
    }

}
