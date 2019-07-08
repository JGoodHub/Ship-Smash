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

    private int score = 0;
    public int timeInSeconds = 0;

    public bool enableKeyTrggeredError;

    //-----METHODS-----

    //Sets up all other managers
    public void Start() {
        AsteroidManager.instance.Initialise();
        PlayerManager.instance.Initialise();
        CameraManager.instance.Initialise();
        UIManager.instance.Initialise();
    }

    /// <summary>
    /// Runs every frame
    /// </summary>
    void Update () {
        timeInSeconds = Mathf.RoundToInt(Time.timeSinceLevelLoad);
        UIManager.instance.DisplayTime(timeInSeconds);

        if (enableKeyTrggeredError == true && Input.GetKeyDown(KeyCode.Q)) {
            Debug.LogError("Pause");
        }

    }

    /// <summary>
    /// Increase the players score
    /// </summary>
    /// <param name="amount">The amount to increase the players score by</param>
    public void IncreaseScore (int amount) {
        score += amount;

        UIManager.instance.SetScore(score);
    }

}