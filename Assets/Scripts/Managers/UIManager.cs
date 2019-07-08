using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    //-----SINGLETON SETUP-----

	public static UIManager instance = null;
	
	void Awake() {
		if (instance == null) {
			instance = this;
		} else {
			Destroy(gameObject);
		}
	}

    //-----VARIABLES-----

    [Header("Hull Elements")]
    public Image hullFillImage;
    public Text hullPercentageText;

    [Header("Shield Elements")]
    public Image shieldFillImage;
    public Text shieldPercentageText;

    [Header("Score and Time Elements")]
    public Text scoreText;
    public Text timeText;

    //-----METHODS-----

    /// <summary>
    /// Sets up the UI for the start of the game
    /// </summary>
    public void Initialise () {
        SetScore(0);
        DisplayTime(0);
    }

    /// <summary>
    /// Update the players UI stats
    /// </summary>
    public void UpdatePlayerStats () {
        StatsController playerStats = PlayerManager.instance.playerController.StatsController;

        hullFillImage.fillAmount = playerStats.CurrentHealth / playerStats.maxHealth;
        hullPercentageText.text = Mathf.Round(hullFillImage.fillAmount * 100) + "%";

        shieldFillImage.fillAmount = playerStats.CurrentShield / playerStats.maxShield;
        shieldPercentageText.text = Mathf.Round(shieldFillImage.fillAmount * 100) + "%";
    }

    /// <summary>
    /// Sets the text of the score element
    /// </summary>
    /// <param name="value">The score value to set the text to</param>
    public void SetScore (int value) {
        scoreText.text = value + "";
    }

    /// <summary>
    /// Display the time in minutes and seconds since the game started
    /// </summary>
    /// <param name="secondSinceLoad">Time in seconds only since the game started</param>
    public void DisplayTime (int secondSinceLoad) {
        string minutes = "" + Mathf.FloorToInt(secondSinceLoad / 60);
        string seconds = "" + secondSinceLoad % 60;

        if (seconds.Length < 2) {
            seconds = "0" + seconds;
        }

        timeText.text = minutes + ":" + seconds;
    }

	
}
