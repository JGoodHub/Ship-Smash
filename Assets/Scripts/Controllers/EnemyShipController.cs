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

    //Set the call to flip orbit direction going
    public override void Initialise () {
        base.Initialise();
        Invoke("FlipOrbitDirection", Random.Range(1f, 2f));
    }
    
    //Recursively and randomly change the direction the ship orbits the player
    private void FlipOrbitDirection () {
        orbitClockwise = !orbitClockwise;
        Invoke("FlipOrbitDirection", Random.Range(2f, 8f));
    }

    //Deals with the object when its health reaches zero
    public override void Die () {
        EnemyManager.instance.Enemies.Remove(this);
        base.Die();
    }



}
