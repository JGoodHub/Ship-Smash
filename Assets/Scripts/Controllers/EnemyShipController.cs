using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipController : ShipController {
    
    //-----VARIABLES-----

    private bool orbitClockwise = true;
    public bool OrbitClockwise {
        get { return orbitClockwise; }
    }

    //-----METHODS-----

    /// <summary>
    /// Setup the enemy ship controller
    /// </summary>
    public override void Initialise () {
        base.Initialise();
        Invoke("FlipOrbitDirection", Random.Range(1f, 2f));
    }

    /// <summary>
    /// Recursively and randomly change the direction the ship orbits the player
    /// </summary>
    private void FlipOrbitDirection () {
        orbitClockwise = !orbitClockwise;
        Invoke("FlipOrbitDirection", Random.Range(2f, 8f));
    }

    /// <summary>
    /// Deals with the object when its health reaches zero
    /// </summary>
    public override void Die () {
        EnemyManager.instance.Enemies.Remove(this);
        base.Die();
    }

}
