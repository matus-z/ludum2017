using UnityEngine;
using System.Collections.Generic;

// ----------------------------------------------------------------
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public float MovementSpeedBase = 2.0f;
    public float MovementSpeedMultiplier = 0.3f;

    public bool Moving { get; private set; }

    private float MovementSpeed = 0.0f;

    private Vector2 MovementStart;
    private Vector2 Destination;

    public PositionOnGrid Pos;

    // ----------------------------------------------------------------
    private void Start()
    {
        SetMoving(false);
    }

    // ----------------------------------------------------------------
    public void Init(PositionOnGrid pos)
    {
        Pos = pos;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Destination = rb.position;

        SetMoving(false);
    }

    // ----------------------------------------------------------------
    public void MoveTo(Vector2 destination)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        float distance = (rb.position - destination).magnitude;

        MovementSpeed = MovementSpeedBase + distance * MovementSpeedMultiplier;

        MovementStart = rb.position;
        Destination = destination;
        SetMoving(true);
    }

    // ----------------------------------------------------------------
    private void FixedUpdate()
    {
        SolveAnimations();

        if (!Moving)
            return;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        float distStartToDestination = (MovementStart - Destination).magnitude;
        float distActToDestination = (rb.position - Destination).magnitude;
        float fractionToGoal = distActToDestination / distStartToDestination;

        if (distActToDestination > 0.1f)
        {
            float movementSpeedAct = MovementSpeed;
            float c = Mathf.Abs(fractionToGoal - 0.5f);
            if (fractionToGoal < 0.5f)
                movementSpeedAct = (1.0f - 1.95f * c) * movementSpeedAct;

            rb.MovePosition(Vector2.MoveTowards(rb.position, Destination, movementSpeedAct * Time.fixedDeltaTime));
        }
        else
        {
            SetMoving(false);
        }
    }

    // ----------------------------------------------------------------
    private void SetMoving(bool moving)
    {
        Moving = moving;
        SolveAnimations();
    }

    // ----------------------------------------------------------------
    private void SolveAnimations()
    {
        Animator anim = gameObject.GetComponent<Animator>();
        if (anim == null)
            return;

        if (!Moving)
        {
            anim.Play("Idle");
            return;
        }

        Vector2 startToDestination = MovementStart - Destination;

        if (startToDestination.x > 0)
            anim.Play("MoveRight");
        else if (startToDestination.x < 0)
            anim.Play("MoveLeft");
        else if (startToDestination.y < 0)
            anim.Play("MoveUp");
        else if (startToDestination.y > 0)
            anim.Play("MoveDown");
    }
}
