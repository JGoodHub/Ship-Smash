using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    //-----ENUMS-----

    public enum Size { SMALL, MEDIUM, LARGE };

    //-----VARIABLES-----

    private float speed;
   
    //-----METHODS-----

    public void Initialise (Size size) {
        switch (size) {
            case Size.SMALL:
                transform.localScale = Vector3.one * Random.Range(0.1f, 0.2f);
                speed = Random.Range(-180f, 180f);
            break;
            case Size.MEDIUM:
                transform.localScale = Vector3.one * Random.Range(0.2f, 0.4f);
                speed = Random.Range(-45f, 45f);
            break;
            case Size.LARGE:
                transform.localScale = Vector3.one * Random.Range(2f, 4f);
                speed = Random.Range(-20f, 20f);
            break;
        }
    }

    void Update () {
        transform.Rotate(Vector3.forward, speed * Time.deltaTime);
    }



}
