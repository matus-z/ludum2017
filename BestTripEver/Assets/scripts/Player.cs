using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------\
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public float MovementSpeed = 10.0f;

    private bool Moving;

    private Vector2 Destination;

    // ----------------------------------------------------------------
    private void Start()
    {
    }

    // ----------------------------------------------------------------
    public void Init(PositionOnGrid pos, float offsetX, float offsetY, float tileSize)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.position = new Vector3(offsetX + pos.X * tileSize, offsetY + pos.Y * tileSize, 0.0f);
    }

    // ----------------------------------------------------------------
    private void Update()
    {
        if (Moving)
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

        Moving = isUp || isDown || isLeft || isRight;
    }

    // ----------------------------------------------------------------
    private void FixedUpdate()
    {
        if (!Moving)
            return;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (Vector2.Distance(rb.position, Destination) > 0.1f)
        {
            rb.MovePosition(Vector2.MoveTowards(rb.position, Destination, MovementSpeed * Time.fixedDeltaTime));
        }
        else
        {
            Moving = false;
        }
    }
}
