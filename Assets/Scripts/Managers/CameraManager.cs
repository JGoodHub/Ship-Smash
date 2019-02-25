using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    //-----SINGLETON-----

    public static CameraManager instance = null;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    //-----VARIABLES-----

    public static Camera mainCamera;

    public Transform focusTarget;

    //-----METHODS-----

    //Setup method
    public void Initialise () {
        mainCamera = Camera.main;         
    }

    //Move the camera position to match its targets
    void Update () {
        if (focusTarget != null) {
            transform.position = new Vector3(focusTarget.position.x, focusTarget.position.y, -10f);
        }
    }



}
