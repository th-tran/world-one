using System.Collections;
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

		// Placeholder wincon
		theLevelManager.levelCompleteScreen.SetActive (true);
		GameObject player = GameObject.Find("Player");
		player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		player.GetComponent<PlayerController>().enabled = false;
	}
}
