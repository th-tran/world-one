﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public PlayerController thePlayer;
	public float waitForRespawn;

	void Start () {
		thePlayer = FindObjectOfType<PlayerController>();
	}

	public void Respawn () {
		// Separate respawn into its own event while time continues
		StartCoroutine("RespawnCo");
	}

	public IEnumerator RespawnCo () {
		// Deactivate player
		thePlayer.gameObject.SetActive (false);

		// Pause between death and respawn
		yield return new WaitForSeconds (waitForRespawn);

		// Re-activate player
		thePlayer.transform.position = thePlayer.respawnPosition;
		thePlayer.gameObject.SetActive (true);
	}
}
