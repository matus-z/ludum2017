using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------
public class PlayerController : MonoBehaviour
{
	public float MovementSpeed = 10.0f;

    private Rigidbody2D RigidBody;
    private Vector2 Up;
    public int gridX;
    public int gridY;
    private bool moving;
    private Vector2 destination;


    public Puzzle puzzle;

    // ----------------------------------------------------------------
    private void Start ()
    {
		RigidBody = GetComponent<Rigidbody2D>();
        Up = new Vector3(1.0f, 0, 0);
        RigidBody.position = puzzle.getDestination(ref gridX, ref gridY, ExtensionMethods.Direction.No);
    }

    private void Update() {
        if (!moving) {
            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                destination = puzzle.getDestination(ref gridX, ref gridY, ExtensionMethods.Direction.Up);
                moving = true;
            } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                destination = puzzle.getDestination(ref gridX, ref gridY, ExtensionMethods.Direction.Right);
                moving = true;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                destination = puzzle.getDestination(ref gridX, ref gridY, ExtensionMethods.Direction.Down);
                moving = true;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                destination = puzzle.getDestination(ref gridX, ref gridY, ExtensionMethods.Direction.Left);
                moving = true;
            }

        }
    }

    // ----------------------------------------------------------------
    private void FixedUpdate ()
    {
        if (moving) {
            if (Vector2.Distance(RigidBody.position, destination) > 0.1f) {
                RigidBody.MovePosition(Vector2.MoveTowards(RigidBody.position, destination, MovementSpeed * Time.fixedDeltaTime));
            } else {
                moving = false;
            }
        }
    }

    // ----------------------------------------------------------------
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, Up.normalized.Rotated(45.0f));
        Gizmos.DrawRay(transform.position, Up.normalized.Rotated(-45.0f));
    }
}
