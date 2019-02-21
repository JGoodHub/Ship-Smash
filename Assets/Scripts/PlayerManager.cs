using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    //-----SINGLETON-----

    public static PlayerManager instance = null;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    //-----VARIABLES-----

    public ShipController player;

    //-----METHODS-----

    public void Initialise() {
        player.Initialise();
    }

    void Update () {
        if (Input.GetMouseButton(0)) {
            player.ShootController.Fire("Enemy");
        }
    }

    void FixedUpdate () {
        player.MovementController.Thrust (new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
        player.MovementController.LookAtTarget(CameraManager.mainCamera.ScreenToWorldPoint(Input.mousePosition));
    }


}
