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
    
    [Header("Enemy Settings")]
    public GameObject enemyPrefab;
    private HashSet<EnemyShipController> enemies = new HashSet<EnemyShipController>();
    public HashSet<EnemyShipController> Enemies { get => enemies; }


    [Header("Spawn Settings")]
    public AnimationCurve spawnFrequencyOverTime;
    private float spawnTimer = 0;
    public float spawnBorder;
    public bool spawnEnemies;

    [Header("Player Settings")]
    public ShipController playerTarget;
    public float orbitDistance;
    public float orbitHysteresis;
    public float minRange;

    //-----METHODS-----

    /// <summary>
    /// Create a new enemy instance at a random location on the border of the world
    /// </summary>
    private void SpawnScout () {
        if (spawnEnemies) {
            float spawnX = Random.Range(-spawnBorder, spawnBorder);
            float spawnY = Random.Range(-spawnBorder, spawnBorder);

            if (Mathf.Abs(spawnX) > Mathf.Abs(spawnY)) {
                spawnX = spawnBorder * Sign(spawnX);
            } else {
                spawnY = spawnBorder * Sign(spawnY);
            }

            GameObject enemyObjectInstance = Instantiate(enemyPrefab, new Vector3(spawnX, spawnY, 0), Quaternion.identity);
            enemyObjectInstance.transform.SetParent(gameObject.transform);

            EnemyShipController enemyControllerInstance = (EnemyShipController)enemyObjectInstance.GetComponent<ShipController>();
            enemyControllerInstance.Initialise();

            enemies.Add(enemyControllerInstance);
        }        
    }

    /// <summary>
    /// Have each enemy ship fly towards the player and try ti settle in an orbit around them
    /// When at the correct distance begin shooting at the player
    /// </summary>
    void Update () {
        //Spawn new enemy if needed
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0) {
            SpawnScout();
            spawnTimer = 1f / spawnFrequencyOverTime.Evaluate(Time.time / 60f);
        }

        //Manage the enemies on each update
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
                if (distanceToPlayer < orbitDistance + orbitHysteresis && distanceToPlayer > minRange) {
                    enemy.ShootController.Fire("Player");

                    //Thrust sideways in an attempt to evade the player
                    if (distanceToPlayer > orbitDistance - orbitHysteresis) {
                        Vector2 perpendicularDirection = Vector2.Perpendicular(enemy.MovementController.Vector2Position() - playerTarget.MovementController.Vector2Position()).normalized;
                        if (enemy.orbitClockwise) {
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

    /// <summary>
    /// Returns a normalised version of the float passed, indicating its sign
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public int Sign (float num) {
        if (num > 0) {
            return 1;
        } else if (num == 0) {
            return 0;
        } else {
            return -1;
        }
    }

    //-----GIZMOS-----
    [Header("Gizmo Toggles")]
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

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(playerTarget.transform.position, minRange);

            //The spawn border
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(spawnBorder, spawnBorder, 0f));
        }
    }


}
