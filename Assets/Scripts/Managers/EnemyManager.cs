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
    
    private HashSet<EnemyShipController> enemies = new HashSet<EnemyShipController>();
    public HashSet<EnemyShipController> Enemies { get => enemies; }

    public GameObject enemyPrefab;

    public float orbitDistance;
    public float orbitHysteresis;

    public ShipController playerTarget;

    //-----METHODS-----

    //Spawn a new enemy every second
    public void Initialise () {
        InvokeRepeating("SpawnScout", 5f, 1f);
    }

    //Create a new enemy instance at a random location
    private void SpawnScout () {
        GameObject enemyInstance = Instantiate(enemyPrefab, new Vector3(Random.Range(-100f, 100f), Random.Range(-100f, 100f), 0), Quaternion.identity);
        EnemyShipController enemyControllerInstance = (EnemyShipController) enemyInstance.GetComponent<ShipController>();
        enemyControllerInstance.Initialise();

        enemies.Add(enemyControllerInstance);
    }

    //Have each enemy ship fly towards the player and try ti settle in an orbit around them
    //When at the correct distance begin shooting at the player
    void Update () {
        foreach(EnemyShipController enemy in enemies) {
            if (playerTarget != null) {
                enemy.MovementController.LookAtTarget(playerTarget.transform.position);
                float distanceToPlayer = Vector2.Distance(enemy.MovementController.Vector2Position(), playerTarget.MovementController.Vector2Position());

                //Fly towards the player if to far away and reverse if to close
                if (distanceToPlayer > orbitDistance + orbitHysteresis) {
                    enemy.MovementController.Thrust(playerTarget.MovementController.Vector2Position() - enemy.MovementController.Vector2Position());
                } else if (distanceToPlayer < orbitDistance - orbitHysteresis) {
                    enemy.MovementController.Thrust(enemy.MovementController.Vector2Position() - playerTarget.MovementController.Vector2Position());
                }

                //When within the right range open fire
                if (distanceToPlayer < orbitDistance + orbitHysteresis) {
                    enemy.ShootController.Fire("Player");

                    //Thrust sideways in an attempt to evade the player
                    if (distanceToPlayer > orbitDistance - orbitHysteresis) {
                        Vector2 perpendicularDirection = Vector2.Perpendicular(enemy.MovementController.Vector2Position() - playerTarget.MovementController.Vector2Position()).normalized;
                        if (enemy.OrbitClockwise) {
                            enemy.MovementController.ThrustRaw(-perpendicularDirection * 0.5f);
                            //Debug.DrawRay(enemy.MovementController.transform.position, -perpendicularDirection.normalized * 2f, Color.red);
                        } else {
                            enemy.MovementController.ThrustRaw(perpendicularDirection * 0.5f);
                            //Debug.DrawRay(enemy.MovementController.transform.position, perpendicularDirection.normalized * 2f, Color.red);
                        }
                    }
                }
            }
        }
    }

    //-----GIZMOS----- 
    public bool drawGizmos;
    void OnDrawGizmos () {
        if (drawGizmos) {
            //Target orbit range
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(playerTarget.transform.position, orbitDistance);

            //Target orbit range with hysteresis
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(playerTarget.transform.position, orbitDistance + orbitHysteresis);
            Gizmos.DrawWireSphere(playerTarget.transform.position, orbitDistance - orbitHysteresis);
        }
    }


}
