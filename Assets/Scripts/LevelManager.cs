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

	void Start () {
		// Get reference to PlayerController
		thePlayer = FindObjectOfType<PlayerController>();

		// Initialize coin UI
		coinText.text = "X " + coinCount;
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

		// Re-activate player
		thePlayer.transform.position = thePlayer.respawnPosition;
		thePlayer.isJumping = false;
		thePlayer.isCrouching = false;
		thePlayer.gameObject.SetActive (true);
	}

	public void AddCoins (int coinsToAdd) {
		// Update coin count
		coinCount += coinsToAdd;
		// Update coin UI
		coinText.text = "X " + coinCount;
	}
}
