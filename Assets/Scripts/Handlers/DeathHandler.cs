using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour {

    //-----ENUM-----

    public enum AttachedTo { SHIP, ASTEROID }

    //-----VARIABLES-----

    public AttachedTo attachedTo;  

    //-----METHODS-----

    //Setup Method
    public void Initialise () {
    }

    public void Die () {
        switch (attachedTo) {
            case AttachedTo.SHIP:
                GetComponent<ShipController>().Die();
            break;
            case AttachedTo.ASTEROID:
                GetComponent<AsteroidController>().BreakApart();
            break;
        }        
    }

    //-----GIZMOS-----
    //[Header("Gizmo Toggles")]
    //public bool drawGizmos;
    void OnDrawGizmos () {
        //if (drawGizmos) {
        //}
    }

}