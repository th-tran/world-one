using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour {
	// Controls checkpoint animations
	private Animator checkpointAnim;
	public bool checkpointActive;

	void Start () {
		checkpointAnim = GetComponent<Animator>();
		checkpointActive = false;
	}

	void Update () {
		checkpointAnim.SetBool ("Active", checkpointActive);
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player")
		{
			checkpointActive = true;
		}
	}
}
