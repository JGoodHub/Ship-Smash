using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    //-----VARIABLES-----

    public float thrusterPower;
    public float rotationSpeed;

    private Rigidbody2D rigid2D;

    //-----METHODS-----
    
    //Setup method
    public void Initialise () {
        rigid2D = GetComponent<Rigidbody2D>();
    }

    //Apply normalised thrust in the passed direction
    public void Thrust (Vector2 direction) {
        rigid2D.AddForce(direction.normalized * thrusterPower * Time.fixedDeltaTime);
    }

    //Apply raw thrust in the passed direction
    public void ThrustRaw (Vector2 direction) {
        rigid2D.AddForce(direction * thrusterPower * Time.fixedDeltaTime);
    }

    //Snap to look at the passed target position
    public void LookAtTarget (Vector3 targetPosition) {
        targetPosition.z = transform.position.z;
        transform.up = targetPosition - transform.position;
    }

    //Shorthand method for converting position to vector2
    public Vector2 Vector2Position () {
        return (Vector2) transform.position;
    }

    

}
