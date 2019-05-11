using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	// Controls player behaviour
	public PlayerController thePlayer;
	public GameObject deathSplosion;
	public float waitForDeath;
	public float waitForRespawn;

	// Controls coin behaviour
	public int coinCount;
	private int coinBonusLifeCount;
	public int bonusLifeThreshold;
	public Text coinText;
	public AudioSource coinSound;

	// Health UI
	public Image heart1, heart2, heart3;
	public Sprite heartFull, heartHalf, heartEmpty;
	
	// Controls health behaviour
	public int maxHealth;
	public int healthCount;

	// Controls player states
	public bool invincible;
	private bool respawning;

	// All objects to reset on respawn
	public ResetOnRespawn[] objectsToReset;

	// Controls lives behaviour
	public int startingLives;
	public int currentLives;
	public Text livesText;

	// Overlay screens
	public GameObject gameOverScreen;
	public GameObject levelCompleteScreen;

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
		// Check if player is dead
		if (healthCount <= 0 && !respawning)
		{
			Respawn();
			respawning = true;
		}
		// Check if player has enough coins for bonus
		if (coinBonusLifeCount >= bonusLifeThreshold)
		{
			currentLives += 1;
			livesText.text = "X " + currentLives;
			coinBonusLifeCount -= bonusLifeThreshold;
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
			StartCoroutine("GameOverCo");
		}
	}

	public IEnumerator RespawnCo () {
		// ZA WARUDO
		Time.timeScale = 0f;
		yield return new WaitForSecondsRealtime (waitForDeath);
		Time.timeScale = 1f;

		// YOU DIED
		Instantiate (deathSplosion, thePlayer.transform.position, thePlayer.transform.rotation);

		// Deactivate player
		thePlayer.gameObject.SetActive (false);

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
		coinBonusLifeCount = 0;
		coinText.text = "X " + coinCount;

		// BITE ZA DUSTU
		for (int i = 0; i < objectsToReset.Length; i++)
		{
			objectsToReset[i].gameObject.SetActive (true);
			objectsToReset[i].ResetObject();
		}
	}

	public IEnumerator GameOverCo () {
		// Freeze and delay before death
		Time.timeScale = 0f;
		yield return new WaitForSecondsRealtime (waitForDeath);
		Time.timeScale = 1f;

		// Player dies
		Instantiate (deathSplosion, thePlayer.transform.position, thePlayer.transform.rotation);
		thePlayer.gameObject.SetActive (false);

		// Game over
		gameOverScreen.SetActive (true);
	}

	public void AddCoins (int coinsToAdd) {
		// Update coin count and UI
		coinCount += coinsToAdd;
		coinBonusLifeCount += coinsToAdd;
		coinText.text = "X " + coinCount;
		coinSound.Play();
	}

	public void AddLives (int livesToAdd) {
		// Update lives count and UI
		currentLives += livesToAdd;
		livesText.text = "X " + currentLives;
		coinSound.Play();
	}

	public void HurtPlayer (int damageToTake) {
		// Lose health equal to damage taken
		if (!invincible)
		{
			healthCount -= damageToTake;
			UpdateHeartMeter();
			if (healthCount > 0)
			{
				thePlayer.Knockback();
				thePlayer.hurtSound.Play();
			}
		}
	}

	public void GiveHealth (int healthToGive) {
		healthCount += healthToGive;
		if (healthCount > maxHealth)
		{
			// Setting health cap for now
			healthCount = maxHealth;
		}
		UpdateHeartMeter();
		coinSound.Play();
	}

	public void UpdateHeartMeter () {
		// Updates the health UI based on health count
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
