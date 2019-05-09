using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public PlayerController thePlayer;

	void Start () {
		thePlayer = FindObjectOfType<PlayerController>();
	}

	public void Respawn () {
		thePlayer.gameObject.SetActive (false);
		thePlayer.transform.position = thePlayer.respawnPosition;
		thePlayer.gameObject.SetActive (true);
	}
}
