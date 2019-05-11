using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompEnemy : MonoBehaviour {

	private Rigidbody2D playerRigidbody;

	public GameObject deathSplosion;
	public float bounceForce;

	void Start () {
		playerRigidbody = transform.parent.GetComponent<Rigidbody2D>();
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Enemy")
		{
			//Destroy (other.gameObject);
			other.gameObject.SetActive (false);
			//Instantiate (deathSplosion, other.transform.position, other.transform.rotation);
			playerRigidbody.velocity = new Vector2 (playerRigidbody.velocity.x, bounceForce);
		}
	}
}
