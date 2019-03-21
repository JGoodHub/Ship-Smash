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

    [Header("Small Asteroid Settings")]
    public Transform smallAsteroidCollection;
    public GameObject smallAsteroidPrefab;

    [Header("Small Asteroid Settings")]
    public Transform mediumAsteroidCollection;
    public GameObject mediumAsteroidPrefab;

    [Header("Small Asteroid Settings")]
    public Transform largeAsteroidCollection;
    public GameObject largeAsteroidPrefab;

    [Header("Generation Settings")]
    public Vector2 fillSize;
    public int smallAsteroidCount;
    public int mediumAsteroidCount;
    public int largeAsteroidCount;

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
            GenerateAsteroid(size, new Vector3(Random.Range(-fillSize.x / 2, fillSize.x / 2), Random.Range(-fillSize.y / 2, fillSize.y / 2), 0)).transform.SetParent(collection);
        }
    }

    /// <summary>
    /// Factory method that generates and return an asteroid of a specified size at a specified positions
    /// </summary>
    /// <param name="asteroidSize">Size of the asteroid</param>
    /// <param name="position">Position of the asteroid</param>
    /// <returns>The GameObject instance of the asteroid</returns>
    public GameObject GenerateAsteroid (Size asteroidSize, Vector3 position) {
        GameObject asteroidInstance = null;

        switch (asteroidSize) {
            case AsteroidManager.Size.SMALL:
                asteroidInstance = Instantiate(smallAsteroidPrefab, position, Quaternion.identity);
                asteroidInstance.GetComponent<AsteroidController>().Initialise(Size.SMALL);
                break;
            case AsteroidManager.Size.MEDIUM:
                asteroidInstance = Instantiate(mediumAsteroidPrefab, position, Quaternion.identity);
                asteroidInstance.GetComponent<AsteroidController>().Initialise(Size.MEDIUM);
                break;
            case AsteroidManager.Size.LARGE:
                asteroidInstance = Instantiate(largeAsteroidPrefab, position, Quaternion.identity);
                asteroidInstance.GetComponent<AsteroidController>().Initialise(Size.LARGE);
                break;
            default:
                break;
        }
        
        return asteroidInstance;        
    }

    //-----GIZMOS-----   
    [Header("Gizmo Toggles")] 
    public bool drawGizmos;
    void OnDrawGizmos () {
        if (drawGizmos) {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(Vector3.zero, (Vector3) fillSize);
        }
    }

}
