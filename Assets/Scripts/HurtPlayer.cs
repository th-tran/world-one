using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour {

	private LevelManager theLevelManager;
	public int damageToGive;

	void Start () {
		theLevelManager = FindObjectOfType<LevelManager>();
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player")
		{
			theLevelManager.HurtPlayer(damageToGive);
		}
	}
}
