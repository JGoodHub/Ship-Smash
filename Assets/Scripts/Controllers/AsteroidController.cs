using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour { 

    //-----VARIABLES-----

    private float speed;
    private AsteroidManager.Size asteroidSize;

    private StatsController asteroidStats;
   
    //-----METHODS-----

    /// <summary>
    /// Setup the controller
    /// </summary>
    /// <param name="size">Size of the asteroid</param>
    public void Initialise (AsteroidManager.Size size) {
        asteroidSize = size;
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

                asteroidStats = GetComponent<StatsController>();
                asteroidStats.Initialise();
                break;
        }        
    }

    /// <summary>
    /// Rotate the asteroid by the set amount each frame
    /// </summary>
    void Update () {
        transform.Rotate(Vector3.forward, speed * Time.deltaTime);
    }


    /// <summary>
    /// Break the large asteroid into a shower of smaller ones
    /// </summary>
    public void BreakApart () {
        if (asteroidSize == AsteroidManager.Size.LARGE) {
            for (int i = 0; i < Random.Range(5, 10); i++) {
                Vector3 childAsteroidPosition = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized * Random.Range(0, 3f);
                GameObject asteroidObjectInstance = AsteroidManager.instance.GenerateAsteroid(AsteroidManager.Size.MEDIUM, childAsteroidPosition + transform.position);
                asteroidObjectInstance.transform.SetParent(AsteroidManager.instance.mediumAsteroidCollection);

                asteroidObjectInstance.GetComponent<AsteroidController>().Drift(childAsteroidPosition.normalized * Random.Range(0.5f, 1f));
            }

            for (int i = 0; i < Random.Range(10, 20); i++) {
                Vector3 childAsteroidPosition = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized * Random.Range(0, 3f);
                GameObject asteroidObjectInstance = AsteroidManager.instance.GenerateAsteroid(AsteroidManager.Size.SMALL, childAsteroidPosition + transform.position);
                asteroidObjectInstance.transform.SetParent(AsteroidManager.instance.mediumAsteroidCollection);

                asteroidObjectInstance.GetComponent<AsteroidController>().Drift(childAsteroidPosition.normalized * Random.Range(1f, 2f));
            }

            GameObject replacementAsteroidObjectInstance = AsteroidManager.instance.GenerateAsteroid(AsteroidManager.Size.MEDIUM, transform.position);
            replacementAsteroidObjectInstance.transform.SetParent(AsteroidManager.instance.mediumAsteroidCollection);
            Destroy(gameObject);
        }
    }


    /// <summary>
    /// Trigger the drift coroutine
    /// </summary>
    /// <param name="velocity">Velcity of the asteroid</param>
    public void Drift (Vector3 velocity) {
        StartCoroutine(DriftCoroutine(velocity));
    }


    /// <summary>
    /// Translate the asteroid by its velocity each frame
    /// </summary>
    /// <param name="velocity">Velcity of the asteroid</param>
    /// <returns>Null</returns>
    IEnumerator DriftCoroutine(Vector3 velocity) {
        while (velocity.Equals(Vector3.zero) == false) {
            transform.position += (velocity * Time.deltaTime);
            //velocity *= 0.75f;
            yield return null;
        }        
    }



}
