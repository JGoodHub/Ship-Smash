using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    //-----VARIABLES-----

    public float thrusterPower;
    public float rotationSpeed;

    private Rigidbody2D rigid2D;

    //-----METHODS-----
    
    /// <summary>
    /// Setup the movement controller
    /// </summary>
    public void Initialise () {
        rigid2D = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Apply normalised thrust in the passed direction
    /// </summary>
    /// <param name="direction">The direct of thrust</param>
    public void Thrust (Vector2 direction) {
        rigid2D.AddForce(direction.normalized * thrusterPower * Time.fixedDeltaTime);
    }

    /// <summary>
    /// Apply raw thrust in the passed direction
    /// </summary>
    /// <param name="direction">The direct of thrust</param>
    public void ThrustRaw (Vector2 direction) {
        rigid2D.AddForce(direction * thrusterPower * Time.fixedDeltaTime);
    }

    /// <summary>
    /// Snap to look at the passed target position
    /// </summary>
    /// <param name="targetPosition"></param>
    public void LookAtTarget (Vector3 targetPosition) {
        targetPosition.z = transform.position.z;
        transform.up = targetPosition - transform.position;
    }
 
    /// <summary>
    /// Shorthand method for converting position to vector2
    /// </summary>
    /// <returns>Transform position as a Vector2</returns>
    public Vector2 Vector2Position () {
        return (Vector2) transform.position;
    }

    

}
