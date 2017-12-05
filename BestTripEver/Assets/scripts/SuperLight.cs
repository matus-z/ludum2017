using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperLight : MonoBehaviour {

	private bool direction = false;

	// Update is called once per frame
	void Update () {
		if (transform.position.z > -1.0f) {
			direction = false;
		}
		if (transform.position.z < -2.0f) {
      direction = true;
    }
		transform.Translate(new Vector3(0, 0, direction ? Time.deltaTime : -Time.deltaTime));
	}
}
