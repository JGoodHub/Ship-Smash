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

    //-----ENUMS-----

    public enum Size { SMALL, MEDIUM, LARGE };

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

    //Create asteroid of varying sizes
    public void Initialise () {
        FillAsteroidCollection(smallAsteroidCollection, Size.SMALL, smallAsteroidPrefab, smallAsteroidCount);
        FillAsteroidCollection(mediumAsteroidCollection, Size.MEDIUM, mediumAsteroidPrefab, mediumAsteroidCount);
        FillAsteroidCollection(largeAsteroidCollection, Size.LARGE, largeAsteroidPrefab, largeAsteroidCount);
    }

    //Fill the asteroid fields based on the given parameters
    private void FillAsteroidCollection (Transform collection, Size size, GameObject prefab, int count) {
        for (int i = 0; i < count; i++) {
            GameObject asteroidGameObjectInstance = Instantiate(prefab, new Vector3(Random.Range(-fillSize.x / 2, fillSize.x / 2), Random.Range(-fillSize.y / 2, fillSize.y / 2), 0), Quaternion.identity);
            asteroidGameObjectInstance.transform.SetParent(collection);

            AsteroidController asteroidScriptInstance = asteroidGameObjectInstance.GetComponent<AsteroidController>();
            asteroidScriptInstance.Initialise(size);
        }
    }

    //-----GIZMOS-----
    
    public bool drawGizmos;
    void OnDrawGizmos () {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Vector3.zero, (Vector3) fillSize);
    }

}
