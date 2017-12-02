using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float MaxSpeed = 10.0f;
	Rigidbody2D rb;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {
		Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		rb.AddForce(direction * MaxSpeed, ForceMode2D.Impulse);
		rb.velocity = Vector2.ClampMagnitude(rb.velocity, MaxSpeed);
	}
}
