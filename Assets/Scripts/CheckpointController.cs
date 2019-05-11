using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour {
	// Controls checkpoint animations
	private Animator checkpointAnim;
	private CircleCollider2D checkpointCollider;

	void Start () {
		// Get checkpoint components and set to false
		checkpointAnim = GetComponent<Animator>();
		checkpointCollider = GetComponent<CircleCollider2D>();
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player")
		{
			// Activate checkpoint and disable further interaction
			checkpointAnim.SetBool ("Active", true);
			checkpointCollider.enabled = false;
		}
	}
}
