using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {
    
    private MovementController movementController;
    public MovementController MovementController {
        get { return movementController; }
    }

    private ShootController shootController;
    public ShootController ShootController {
        get { return shootController; }
    }

    private StatsController statsController;
    public StatsController StatsController {
        get { return statsController; }
    }

    public virtual void Initialise () {
        movementController = GetComponent<MovementController>();
        shootController = GetComponent<ShootController>();
        statsController = GetComponent<StatsController>();
    }

}
