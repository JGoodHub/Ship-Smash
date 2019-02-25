using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour {

    //----VARIABLES-----

    public GameObject laserProjectile;

    public Transform[] barrels;

    public float rateOfFire;
    private float fireCooldown;

    //-----METHODS-----

    //Reduce cooldown each frame
    void Update () {
        fireCooldown -= Time.deltaTime;
    }

    //Fire a projectile from each barrel and set its intended target tag
    public void Fire (string targetTag) {
        if (fireCooldown <= 0) {
            foreach (Transform barrel in barrels) {
                GameObject projectileInstance = Instantiate(laserProjectile, barrel.position, barrel.rotation);
                projectileInstance.transform.SetParent(GameManager.instance.projectilePool);

                ProjectileController projectileController = projectileInstance.GetComponent<ProjectileController>();
                projectileController.Initialise(gameObject.tag, targetTag);

                fireCooldown = rateOfFire;
            }
        }
    }


}
