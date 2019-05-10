using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailController : MonoBehaviour {

	public Transform leftPoint;
	public Transform rightPoint;
	
	public float moveSpeed;
	public bool movingRight;

	private Rigidbody2D myRigidbody;

	void Start () {
		myRigidbody = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		if (movingRight && transform.position.x > rightPoint.position.x)
		{
			movingRight = false;
			transform.eulerAngles = new Vector3 (0f, 0f, 0f);
		}
		if (!movingRight && transform.position.x < leftPoint.position.x)
		{
			movingRight = true;
			transform.eulerAngles = new Vector3 (0f, 180f, 0f);
		}
		if (movingRight)
		{
			myRigidbody.velocity = new Vector2 (moveSpeed, myRigidbody.velocity.y);
		} else {
			myRigidbody.velocity = new Vector2 (-moveSpeed, myRigidbody.velocity.y);
		}
	}
}
