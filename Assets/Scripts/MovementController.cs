using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    public float thrusterPower;
    public float rotationSpeed;

    private Rigidbody2D rigid2D;
    
    void Start () {
        rigid2D = GetComponent<Rigidbody2D>();
    }

    public void Thrust (Vector2 direction) {
        rigid2D.AddForce(direction.normalized * thrusterPower * Time.fixedDeltaTime);
    }

    public void ThrustRaw (Vector2 direction) {
        rigid2D.AddForce(direction * thrusterPower * Time.fixedDeltaTime);
    }

    public void LookAtTarget (Vector3 targetPosition) {
        targetPosition.z = transform.position.z;
        transform.up = targetPosition - transform.position;
    }

    public Vector2 Vector2Position () {
        return (Vector2) transform.position;
    }

    

}
