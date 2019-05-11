using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailController : MonoBehaviour {

	// Controls the boundaries of snail movement
	public Transform leftPoint;
	public Transform rightPoint;
	
	// Controls movement
	public float moveSpeed;
	public bool movingRight;

	// Rigidbody of snail
	private Rigidbody2D myRigidbody;

	void Start () {
		myRigidbody = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		if (movingRight && transform.position.x > rightPoint.position.x)
		{
			// Snail was moving right and passed the right point, so turn around
			movingRight = false;
			transform.eulerAngles = new Vector3 (0f, 0f, 0f);
		}
		if (!movingRight && transform.position.x < leftPoint.position.x)
		{
			// Vice versa
			movingRight = true;
			transform.eulerAngles = new Vector3 (0f, 180f, 0f);
		}
		if (movingRight)
		{
			// Move right
			myRigidbody.velocity = new Vector2 (moveSpeed, myRigidbody.velocity.y);
		} else {
			// Move left
			myRigidbody.velocity = new Vector2 (-moveSpeed, myRigidbody.velocity.y);
		}
	}
}
