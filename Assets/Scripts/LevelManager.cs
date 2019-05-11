using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	public PlayerController thePlayer;
	public GameObject deathSplosion;
	public float waitForRespawn;

	public int coinCount;
	public Text coinText;

	public Image heart1, heart2, heart3;
	public Sprite heartFull, heartHalf, heartEmpty;
	
	public int maxHealth;
	public int healthCount;

	private bool respawning;

	public ResetOnRespawn[] objectsToReset;

	public bool invincible;

	public int startingLives;
	public int currentLives;
	public Text livesText;

	void Start () {
		// Get reference to PlayerController
		thePlayer = FindObjectOfType<PlayerController>();

		// Initialize health
		healthCount = maxHealth;

		// Initialize coins
		coinText.text = "X " + coinCount;

		// Initialize lives
		currentLives = startingLives;
		livesText.text = "X " + currentLives;

		// Initialize all objects to reset on respawn
		objectsToReset = FindObjectsOfType<ResetOnRespawn>();
	}

	void Update () {
		if (healthCount <= 0 && !respawning)
		{
			Respawn();
			respawning = true;
		}
	}

	public void Respawn () {
		// Separate respawn into its own event while time continues
		healthCount = 0;
		currentLives -= 1;
		livesText.text = "X " + currentLives;
		if (currentLives > 0)
		{	// Next time for sure
			StartCoroutine("RespawnCo");
		} else {
			// Big oof
			thePlayer.gameObject.SetActive (false);
		}
	}

	public IEnumerator RespawnCo () {
		// Deactivate player
		thePlayer.gameObject.SetActive (false);

		// YOU DIED
		Instantiate (deathSplosion, thePlayer.transform.position, thePlayer.transform.rotation);

		// Pause between death and respawn
		yield return new WaitForSeconds (waitForRespawn);

		// Refresh health to full
		healthCount = maxHealth;
		UpdateHeartMeter();

		// Re-activate player
		thePlayer.transform.position = thePlayer.respawnPosition;
		thePlayer.isJumping = false;
		thePlayer.isCrouching = false;
		thePlayer.transform.localScale = new Vector2 (1f, 1f);
		thePlayer.gameObject.SetActive (true);
		respawning = false;

		// Lose all coins
		coinCount = 0;
		coinText.text = "X " + coinCount;

		// BITE ZA DUSTU
		for (int i = 0; i < objectsToReset.Length; i++)
		{
			objectsToReset[i].gameObject.SetActive (true);
			objectsToReset[i].ResetObject();
		}
	}

	public void AddCoins (int coinsToAdd) {
		// Update coin count
		coinCount += coinsToAdd;
		// Update coin UI
		coinText.text = "X " + coinCount;
	}

	public void AddLives (int livesToAdd) {
		currentLives += livesToAdd;
		livesText.text = "X " + currentLives;
	}

	public void HurtPlayer (int damageToTake) {
		// Lose health equal to damage taken
		if (!invincible)
		{
			healthCount -= damageToTake;
			UpdateHeartMeter();
			thePlayer.Knockback();
		}
	}

	public void UpdateHeartMeter () {
		switch (healthCount)
		{
			case 6: heart1.sprite = heart2.sprite = heart3.sprite = heartFull;
					return;
			case 5: heart1.sprite = heart2.sprite = heartFull;
					heart3.sprite = heartHalf;
					return;
			case 4: heart1.sprite = heart2.sprite = heartFull;
					heart3.sprite = heartEmpty;
					return;
			case 3: heart1.sprite = heartFull;
					heart2.sprite = heartHalf;
					heart3.sprite = heartEmpty;
					return;
			case 2: heart1.sprite = heartFull;
					heart2.sprite = heart3.sprite = heartEmpty;
					return;
			case 1: heart1.sprite = heartHalf;
					heart2.sprite = heart3.sprite = heartEmpty;
					return;
			case 0: heart1.sprite = heart2.sprite = heart3.sprite = heartEmpty;
					return;
			default: heart1.sprite = heart2.sprite = heart3.sprite = heartEmpty;
					 return;
		}
	}
}
