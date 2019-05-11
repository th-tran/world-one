using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour {

	// Controls movement
	public float moveSpeed;
	private bool canMove;

	// Rigidbody of slime
	private Rigidbody2D myRigidbody;

	// Player check
	/*public float aggroRadius;
	public LayerMask whatIsPlayer;*/

	void Start () {
		myRigidbody = GetComponent<Rigidbody2D>();
		canMove = false;
	}
	
	void Update () {
		// Move if in range of Player
		//canMove = Physics2D.OverlapCircle (transform.position, aggroRadius, whatIsPlayer);
		if (canMove)
		{
			myRigidbody.velocity = new Vector2 (-moveSpeed, myRigidbody.velocity.y);
		}
	}

	// TODO: Find reliable workaround to camera issue
	void OnBecameVisible () {
		canMove = true;
	}

	void OnEnable () {
		canMove = false;
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "KillPlane")
		{
			// Slime dies if (when) it falls off map
			gameObject.SetActive (false);
		}
	}
}
