using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour {

    //-----SINGLETON-----

    public static AsteroidManager instance = null;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    //-----VARIABLES-----

    public Transform playerTransform;

    public Transform smallAsteroidCollection;
    public GameObject smallAsteroidPrefab;
    public int smallAsteroidCount;

    public Transform mediumAsteroidCollection;
    public GameObject mediumAsteroidPrefab;
    public int mediumAsteroidCount;

    public Transform largeAsteroidCollection;
    public GameObject largeAsteroidPrefab;
    public int largeAsteroidCount;


    public Vector2 fillSize;

    //-----METHODS-----

    public void Initialise () {
        FillAsteroidCollection(smallAsteroidCollection, Asteroid.Size.SMALL, smallAsteroidPrefab, smallAsteroidCount);
        FillAsteroidCollection(mediumAsteroidCollection, Asteroid.Size.MEDIUM, mediumAsteroidPrefab, mediumAsteroidCount);
        FillAsteroidCollection(largeAsteroidCollection, Asteroid.Size.LARGE, largeAsteroidPrefab, largeAsteroidCount);

    }

    private void FillAsteroidCollection (Transform collection, Asteroid.Size size, GameObject prefab, int count) {
        for (int i = 0; i < count; i++) {
            GameObject asteroidGameObjectInstance = Instantiate(prefab, new Vector3(Random.Range(-fillSize.x / 2, fillSize.x / 2), Random.Range(-fillSize.y / 2, fillSize.y / 2), 0), Quaternion.identity);
            asteroidGameObjectInstance.transform.SetParent(collection);

            Asteroid asteroidScriptInstance = asteroidGameObjectInstance.GetComponent<Asteroid>();
            asteroidScriptInstance.Initialise(size);
        }
    }

    void Update() {
        
    }

    //-----GIZMOS-----
    public bool drawGizmos;
    void OnDrawGizmos () {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Vector3.zero, (Vector3) fillSize);
    }




}
