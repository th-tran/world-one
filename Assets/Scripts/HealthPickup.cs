using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour {

	private LevelManager theLevelManager;
	public int healthToGive;

	void Start () {
		theLevelManager = FindObjectOfType<LevelManager>();
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player")
		{
			// Pick up health
			theLevelManager.GiveHealth (healthToGive);
			gameObject.SetActive (false);
		}
	}
}
