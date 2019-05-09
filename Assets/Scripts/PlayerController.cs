using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	// Player body and forces
	public float moveSpeed;
	public float jumpForce;
	public float moveInput;
	private Rigidbody2D myRigidbody;

	// Controls jumping
	private float jumpTimeCounter;
	public float jumpTime;
	private bool isJumping;

	// Ground check
	public Transform feetPos;
	public float checkRadius;
	public LayerMask whatIsGround;
	private bool isGrounded;

	// Controls player animations
	private Animator myAnim;

	// Player respawns here after death
	public Vector2 respawnPosition;

	void Start () {
		// Get the rigidbody and animator of player
		myRigidbody = GetComponent<Rigidbody2D>();
		myAnim = GetComponent<Animator>();

		// Set initial respawn position to start of level
		respawnPosition = transform.position;
	}
	
	void Update () {
		// Move player left or right (or not at all) based on input
        moveInput = Input.GetAxisRaw ("Horizontal");
		myRigidbody.velocity = new Vector2 (moveInput * moveSpeed, myRigidbody.velocity.y);
		if (moveInput != 0f) 
		{
			// Turn the direction the player is facing based on input
			transform.localScale = new Vector2 (moveInput * 1f, 1f);
		}

		// Allow jumping only if player jumps from ground
		isGrounded = Physics2D.OverlapCircle (feetPos.position, checkRadius, whatIsGround);
		if (Input.GetButtonDown ("Jump") && isGrounded)
		{
			isJumping = true;
			jumpTimeCounter = jumpTime;
			myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, jumpForce);
		}

		// As long as player holds jump button
		if (Input.GetButton ("Jump") && isJumping == true) 
		{
			// If player hasn't run out of jump juice, keep going
			if (jumpTimeCounter > 0) 
			{
				myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, jumpForce);
				jumpTimeCounter -= Time.deltaTime;
			} else { // Player ran out of jump juice
				isJumping = false;
			}
		}

		// Player can only jump once
		if (Input.GetButtonUp ("Jump")) 
		{
			isJumping = false;
		}

		// Set animations based on player values
		myAnim.SetFloat ("Speed", Mathf.Abs (myRigidbody.velocity.x));
		myAnim.SetBool ("Grounded", isGrounded);
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "KillPlane")
		{
			//gameObject.SetActive (false);
			transform.position = respawnPosition;
		}

		if (other.tag == "Checkpoint")
		{
			// Update respawn position
			respawnPosition = other.transform.position;
		}
	}
}
