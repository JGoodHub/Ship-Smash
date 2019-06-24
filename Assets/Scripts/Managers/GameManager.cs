using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //-----SINGLETON-----

    public static GameManager instance = null;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    //-----VARIABLES-----

    public Transform projectilePool;
    public Transform effectsPool;

    //-----METHODS-----

    //Sets up all other managers
    public void Start() {
        AsteroidManager.instance.Initialise();
        PlayerManager.instance.Initialise();
        CameraManager.instance.Initialise();
    }

}