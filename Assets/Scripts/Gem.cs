﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour {

	public GameObject gemBurst;
	public LevelManager theLevelManager;

	void Start () {
		theLevelManager = FindObjectOfType<LevelManager>();
	}
	
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			Instantiate (gemBurst, transform.position, transform.rotation);
		}
		gameObject.SetActive (false);
		theLevelManager.levelCompleteScreen.SetActive (true);
		GameObject.Find("Player").GetComponent<PlayerController>().enabled = false;
	}
}
