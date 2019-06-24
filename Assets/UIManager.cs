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


    //-----METHODS-----

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

	
}
