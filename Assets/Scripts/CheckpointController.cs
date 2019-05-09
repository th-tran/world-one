using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour {
	// Controls checkpoint animations
	private Animator checkpointAnim;
	private CircleCollider2D checkpointCollider;
	public bool checkpointActive;

	void Start () {
		checkpointAnim = GetComponent<Animator>();
		checkpointCollider = GetComponent<CircleCollider2D>();
		checkpointActive = false;
	}

	void Update () {
		checkpointAnim.SetBool ("Active", checkpointActive);
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player")
		{
			checkpointActive = true;
			checkpointCollider.enabled = false;
		}
	}
}
