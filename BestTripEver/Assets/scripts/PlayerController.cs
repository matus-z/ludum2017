using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------
public class PlayerController : MonoBehaviour
{
	public float MovementSpeed = 10.0f;

    private Rigidbody2D RigidBody;
    private Vector2 Up;

    // ----------------------------------------------------------------
    private void Start ()
    {
		RigidBody = GetComponent<Rigidbody2D>();
        Up = new Vector3(1.0f, 0, 0);
    }

    // ----------------------------------------------------------------
    private void Update ()
    {
    }
    
    // ----------------------------------------------------------------
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, Up.normalized.Rotated(45.0f));
        Gizmos.DrawRay(transform.position, Up.normalized.Rotated(-45.0f));
    }
}
