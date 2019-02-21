using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour {

    public GameObject laserProjectile;

    public Transform[] barrels;

    public float rateOfFire;
    private float fireCooldown;

    void Update () {
        fireCooldown -= Time.deltaTime;
    }

    public bool CanFire () {
        return fireCooldown <= 0;
    }

    public void Fire (string targetTag) {
        if (CanFire()) {
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
