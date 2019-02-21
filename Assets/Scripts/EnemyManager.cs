using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    
    //-----SINGLETON-----

    public static EnemyManager instance = null;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    //-----VARIABLES-----
    
    public EnemyShipController[] enemies;

    public float orbitDistance;
    public float orbitHysteresis;

    public ShipController playerTarget;

    //-----METHODS-----

    public void Initialise () {
        foreach(EnemyShipController enemy in enemies) {
            enemy.Initialise();
        }
    }

    void Update () {
        foreach(EnemyShipController enemy in enemies) {
            enemy.MovementController.LookAtTarget(playerTarget.transform.position);
            float distanceToPlayer = Vector2.Distance(enemy.MovementController.Vector2Position(), playerTarget.MovementController.Vector2Position());

            if (distanceToPlayer > orbitDistance + orbitHysteresis) {
                enemy.MovementController.Thrust(playerTarget.MovementController.Vector2Position() - enemy.MovementController.Vector2Position());
            } else if (distanceToPlayer < orbitDistance - orbitHysteresis) {
                enemy.MovementController.Thrust(enemy.MovementController.Vector2Position() - playerTarget.MovementController.Vector2Position());
            }

            if (distanceToPlayer < orbitDistance + orbitHysteresis) {
                enemy.ShootController.Fire("Player");

                if (distanceToPlayer > orbitDistance - orbitHysteresis) {
                    Vector2 perpendicularDirection = Vector2.Perpendicular(enemy.MovementController.Vector2Position() - playerTarget.MovementController.Vector2Position()).normalized;
                    if (enemy.OrbitClockwise) {
                        enemy.MovementController.ThrustRaw(-perpendicularDirection * 0.5f);
                        Debug.DrawRay(enemy.MovementController.transform.position, -perpendicularDirection.normalized * 2f, Color.red);
                    } else {
                        enemy.MovementController.ThrustRaw(perpendicularDirection * 0.5f);
                        Debug.DrawRay(enemy.MovementController.transform.position, perpendicularDirection.normalized * 2f, Color.red);
                    }
                }
            }
        }

    } 

    //-----GIZMOS----- 
    public bool drawGizmos;
    void OnDrawGizmos () {
        if (drawGizmos) {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(playerTarget.transform.position, orbitDistance);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(playerTarget.transform.position, orbitDistance + orbitHysteresis);
            Gizmos.DrawWireSphere(playerTarget.transform.position, orbitDistance - orbitHysteresis);
        }
    }


}
