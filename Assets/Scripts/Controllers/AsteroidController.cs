using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour { 

    //-----VARIABLES-----

    private float speed;
   
    //-----METHODS-----

    public void Initialise (AsteroidManager.Size size) {
        switch (size) {
            case AsteroidManager.Size.SMALL:
                transform.localScale = Vector3.one * Random.Range(0.1f, 0.2f);
                speed = Random.Range(-180f, 180f);
            break;
            case AsteroidManager.Size.MEDIUM:
                transform.localScale = Vector3.one * Random.Range(0.2f, 0.4f);
                speed = Random.Range(-45f, 45f);
            break;
            case AsteroidManager.Size.LARGE:
                transform.localScale = Vector3.one * Random.Range(2f, 4f);
                speed = Random.Range(-20f, 20f);
            break;
        }
    }

    void Update () {
        transform.Rotate(Vector3.forward, speed * Time.deltaTime);
    }



}
