using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : MonoBehaviour {

	private LevelManager theLevelManager;
	public int livesToGive;

	void Start () {
		theLevelManager = FindObjectOfType<LevelManager>();
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player")
		{
			// Pick up extra life
			theLevelManager.AddLives (livesToGive);
			gameObject.SetActive (false);
		}
	}
}
