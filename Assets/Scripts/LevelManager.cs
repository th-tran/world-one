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

	void Start () {
		// Get reference to PlayerController
		thePlayer = FindObjectOfType<PlayerController>();

		// Initialize coin UI
		coinText.text = "X " + coinCount;

		// Initialize health
		healthCount = maxHealth;
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
		StartCoroutine("RespawnCo");
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

		// Re-activate player
		thePlayer.transform.position = thePlayer.respawnPosition;
		thePlayer.isJumping = false;
		thePlayer.isCrouching = false;
		thePlayer.gameObject.SetActive (true);
		respawning = false;
	}

	public void AddCoins (int coinsToAdd) {
		// Update coin count
		coinCount += coinsToAdd;
		// Update coin UI
		coinText.text = "X " + coinCount;
	}

	public void HurtPlayer (int damageToTake) {
		// Lose health equal to damage taken
		healthCount -= damageToTake;
	}
}
