using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour {

	public float moveSpeed;
	private bool canMove;
	private Rigidbody2D myRigidbody;

	void Start () {
		myRigidbody = GetComponent<Rigidbody2D>();
		canMove = false;
	}
	
	void Update () {
		if (canMove)
		{
			myRigidbody.velocity = new Vector2 (-moveSpeed, myRigidbody.velocity.y);
		}
	}

	void OnBecameVisible () {
		canMove = true;
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "KillPlane")
		{
			//Destroy (gameObject);
			gameObject.SetActive (false);
		}
	}
}
