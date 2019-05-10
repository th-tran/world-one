﻿using System.Collections;
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
		healthCount = 0;
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
		UpdateHeartMeter();

		// Re-activate player
		thePlayer.transform.position = thePlayer.respawnPosition;
		thePlayer.isJumping = false;
		thePlayer.isCrouching = false;
		thePlayer.transform.localScale = new Vector2 (1f, 1f);
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
		UpdateHeartMeter();
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
