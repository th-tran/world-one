using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	// Controls the target object that the camera will follow
	public GameObject target;
	private Vector3 targetPosition;
	
	// Controls camera follow behaviour
	public float followAhead;
	public float smoothing;

	void Update () {
		// Set the desired position of the camera
		targetPosition = new Vector3 (target.transform.position.x, transform.position.y, transform.position.z);

		// Move the camera ahead of the player
		if (target.transform.localScale.x > 0f) 
		{
			targetPosition = new Vector3 (targetPosition.x + followAhead, targetPosition.y, targetPosition.z);
		} else {
			targetPosition = new Vector3 (targetPosition.x - followAhead, targetPosition.y, targetPosition.z);
		}

		// Smoothly move the camera from its current position to the desired position
		transform.position = Vector3.Lerp (transform.position, targetPosition, smoothing * Time.deltaTime);
	}
}
