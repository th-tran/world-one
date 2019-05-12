using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailController : MonoBehaviour {
	
	// Controls movement
	public float moveSpeed;
	public bool movingRight;
	public Transform groundDetection;
	private RaycastHit2D groundInfo;

	// Rigidbody of snail
	private Rigidbody2D myRigidbody;

	void Start () {
		myRigidbody = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		groundInfo = Physics2D.Raycast (groundDetection.position, Vector2.down, 2f);
		if (!groundInfo.collider)
		{
			if (movingRight)
			{
				transform.eulerAngles = new Vector3 (0f, 0f, 0f);
				movingRight = false;
			} else {
				transform.eulerAngles = new Vector3 (0f, 180f, 0f);
				movingRight = true;
			}
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
