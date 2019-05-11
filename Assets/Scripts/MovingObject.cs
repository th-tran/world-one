using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {

	// Controls behaviour of object to move
	public GameObject objectToMove;
	public Transform startPoint;
	public Transform endPoint;
	public float moveSpeed;

	// Keeps track of the current position to move to
	private Vector2 currentTarget;

	void Start () {
		// Set initial target
		currentTarget = endPoint.position;
	}

	void Update () {
		// Move towards the target
		objectToMove.transform.position = Vector2.MoveTowards (objectToMove.transform.position, currentTarget, moveSpeed * Time.deltaTime);
		if (objectToMove.transform.position == endPoint.position)
		{
			// Move back to start
			currentTarget = startPoint.position;
		}
		if (objectToMove.transform.position == startPoint.position)
		{
			// Move to end again
			currentTarget = endPoint.position;
		}
	}
}
