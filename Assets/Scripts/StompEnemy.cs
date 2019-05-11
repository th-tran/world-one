using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompEnemy : MonoBehaviour {

	// Rigidbody of player
	private Rigidbody2D playerRigidbody;

	// Controls bounce
	public float bounceForce;

	void Start () {
		playerRigidbody = transform.parent.GetComponent<Rigidbody2D>();
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Enemy")
		{
			// One-shot enemies for now
			other.gameObject.SetActive (false);
			// Player bounces off enemy
			playerRigidbody.velocity = new Vector2 (playerRigidbody.velocity.x, bounceForce);
		}
	}
}
