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

    public override void Initialise () {
        base.Initialise();
        Invoke("FlipOrbitDirection", Random.Range(1f, 2f));
    }
    
    private void FlipOrbitDirection () {
        orbitClockwise = !orbitClockwise;
        Invoke("FlipOrbitDirection", Random.Range(2f, 8f));
    }

    public override void Die () {
        EnemyManager.instance.Enemies.Remove(this);
        base.Die();
    }



}
