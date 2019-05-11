using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	// Player body and forces
	public float moveSpeed;
	public float jumpForce;
	public float moveInput;
	private Rigidbody2D myRigidbody;
	private BoxCollider2D myBoxCollider;
	private SpriteRenderer sprite;

	// Controls jumping
	private float jumpTimeCounter;
	public float jumpTime;
	public bool isJumping;

	// Controls crouching
	private bool downPressed;
	public bool isCrouching;

	// Ground check
	public Transform feetPos;
	public float checkRadius;
	public LayerMask whatIsGround;
	private bool isGrounded;

	// Controls player animations
	private Animator myAnim;

	// Player respawns here after death
	public Vector2 respawnPosition;

	private LevelManager theLevelManager;

	public GameObject stompBox;

	public float knockbackForce;
	public float knockbackLength;
	private float knockbackCounter;

	public float invincibilityLength;
	private float invincibilityCounter;

	void Start () {
		// Get the rigidbody and animator of player
		myRigidbody = GetComponent<Rigidbody2D>();
		myBoxCollider = GetComponent<BoxCollider2D>();
		myAnim = GetComponent<Animator>();

		// Set initial respawn position to start of level
		respawnPosition = transform.position;

		// Get reference to level manager
		theLevelManager = FindObjectOfType<LevelManager>();

		isJumping = false;
		isCrouching = false;
	}
	
	void Update () {
		// Player movement is disabled while in knockback
		if (knockbackCounter <= 0)
		{
			// Move player left or right (or not at all) based on input
			moveInput = Input.GetAxisRaw ("Horizontal");
			myRigidbody.velocity = new Vector2 (moveInput * moveSpeed, myRigidbody.velocity.y);

			// Turn the direction the player is facing based on input
			if (moveInput > 0f)
			{
				transform.localScale = new Vector2 (1f, 1f);
			} else if (moveInput < 0f) {
				transform.localScale = new Vector2 (-1f, 1f);
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

			// Check if player is crouching
			downPressed = (Input.GetButton ("Vertical") && (Input.GetAxisRaw ("Vertical") < 0f));
			if (downPressed)
			{
				isCrouching = true;
				if (isGrounded)
				{
					myRigidbody.velocity = new Vector2 (0f, myRigidbody.velocity.y);
				}
				ResizePlayer(0.7f);
			} else {
				isCrouching = false;
				ResizePlayer(0.9f);
			}
		}

		if (knockbackCounter > 0)
		{
			knockbackCounter -= Time.deltaTime;
			if (transform.localScale.x > 0)
			{
				myRigidbody.velocity = new Vector2 (-knockbackForce, knockbackForce);
			} else {
				myRigidbody.velocity = new Vector2 (knockbackForce, knockbackForce);
			}
		}

		if (invincibilityCounter > 0)
		{
			invincibilityCounter -= Time.deltaTime;
		}

		if (invincibilityCounter <= 0)
		{
			theLevelManager.invincible = false;
		}

		if (myRigidbody.velocity.y < 0)
		{
			stompBox.SetActive (true);
		} else {
			stompBox.SetActive (false);
		}

		// Set animations based on player values
		myAnim.SetFloat ("Speed", Mathf.Abs (myRigidbody.velocity.x));
		myAnim.SetBool ("Grounded", isGrounded);
		myAnim.SetBool ("Crouching", isCrouching);
	}

	void ResizePlayer (float newY) {
		Vector2 size = myBoxCollider.size;
		size.y = newY;
		myBoxCollider.size = size;
	}

	public void Knockback () {
		knockbackCounter = knockbackLength;
		invincibilityCounter = invincibilityLength;
		theLevelManager.invincible = true;
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "KillPlane")
		{
			// Player dies and respawns
			theLevelManager.Respawn();
			theLevelManager.UpdateHeartMeter();
		}

		if (other.tag == "Checkpoint")
		{
			// Update respawn position
			respawnPosition = other.transform.position;
		}
	}

	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag == "MovingPlatform")
		{
			// Things become "attached" to the moving platform until they leave contact
			transform.parent = other.transform;
		}
	}

	void OnCollisionExit2D (Collision2D other) {
		if (other.gameObject.tag == "MovingPlatform")
		{
			// Remove attachment to moving platform
			transform.parent = null;
		}
	}
}
