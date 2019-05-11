using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOnRespawn : MonoBehaviour {

	// Controls where object is reset on respawn
	private Vector2 startPosition;
	private Quaternion startRotation;
	private Vector2 startLocalScale;
	
	// Rigidbody for objects that have it
	private Rigidbody2D myRigidbody;

	void Start () {
		// Set initial state of object
		startPosition = transform.position;
		startRotation = transform.rotation;
		startLocalScale = transform.localScale;

		if (GetComponent<Rigidbody2D>() != null)
		{
			myRigidbody = GetComponent<Rigidbody2D>();
		}
	}

	public void ResetObject () {
		// Resets the object back to its original state
		transform.position = startPosition;
		transform.rotation = startRotation;
		transform.localScale = startLocalScale;

		if (myRigidbody != null)
		{
			myRigidbody.velocity = Vector2.zero;
		}
	}
}
