using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public float jumpSpeed;
	public float moveInput;
	private Rigidbody2D myRigidbody;

	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;

	public bool isGrounded;

	private Animator myAnim;

	void Start () {
		myRigidbody = GetComponent<Rigidbody2D>();
		myAnim = GetComponent<Animator>();
	}
	
	void FixedUpdate () {
        moveInput = Input.GetAxisRaw ("Horizontal");
		myRigidbody.velocity = new Vector2 (moveInput * moveSpeed, myRigidbody.velocity.y);
		if (moveInput != 0f) {
			transform.localScale = new Vector2 (moveInput * 1f, 1f);
		}

		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
		if (Input.GetButtonDown ("Jump") && isGrounded)
		{
			myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, jumpSpeed);
		}

		myAnim.SetFloat ("Speed", Mathf.Abs (myRigidbody.velocity.x));
		myAnim.SetBool ("Grounded", isGrounded);
	}
}
