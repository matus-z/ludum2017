using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------
public class Player : MonoBehaviour
{
    public float MovementSpeed = 10.0f;

    private Rigidbody2D RigidBody;

    private bool moving;

    private Vector2 destination;

    // ----------------------------------------------------------------
    private void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();
    }

    // ----------------------------------------------------------------
    public void Init(int GridX, int GridY)
    {
        //RigidBody.position = puzzle.getDestination(ref gridX, ref gridY, EDirection.No);
    }

    // ----------------------------------------------------------------
    private void Update()
    {
        if (moving)
            return;

        bool isUp = Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W);
        bool isDown= Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S);
        bool isLeft = Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A);
        bool isRight = Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D);

        //if (isUp)
        //{
        //    destination = puzzle.getDestination(ref gridX, ref gridY, EDirection.Up);
        //}
        //else if (isRight)
        //{
        //    destination = puzzle.getDestination(ref gridX, ref gridY, EDirection.Right);
        //}
        //else if (isDown)
        //{
        //    destination = puzzle.getDestination(ref gridX, ref gridY, EDirection.Down);
        //}
        //else if (isLeft)
        //{
        //    destination = puzzle.getDestination(ref gridX, ref gridY, EDirection.Left);
        //}

        moving = isUp || isDown || isLeft || isRight;
    }

    // ----------------------------------------------------------------
    private void FixedUpdate()
    {
        if (!moving)
            return;

        if (Vector2.Distance(RigidBody.position, destination) > 0.1f)
        {
            RigidBody.MovePosition(Vector2.MoveTowards(RigidBody.position, destination, MovementSpeed * Time.fixedDeltaTime));
        }
        else
        {
            moving = false;
        }
    }

    // ----------------------------------------------------------------
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
    }
}
