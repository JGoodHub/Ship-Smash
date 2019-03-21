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

    public ShipController playerController;

    //-----METHODS-----

    //Sets up the player
    public void Initialise() {
        playerController.Initialise();
    }

    //Attempt to fire if the user presses the mouse button
    void Update () {
        if (Input.GetMouseButton(0) && playerController.ShootController.CanShipFire()) {
            playerController.ShootController.Fire("Enemy");
        }
    }

    //Thrust based on WASD keys and lok at the cursor
    void FixedUpdate () {
        playerController.MovementController.Thrust (new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
        playerController.MovementController.LookAtTarget(CameraManager.mainCamera.ScreenToWorldPoint(Input.mousePosition));
    }
    
}
