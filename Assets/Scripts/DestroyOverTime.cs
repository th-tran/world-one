using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour {

	public float lifeTime;

	void Update () {
		// Time until self-destruct
		lifeTime = lifeTime - Time.deltaTime;
		if (lifeTime <= 0f)
		{
			// Remove object from the game
			Destroy (gameObject);
		}
	}
}
