using UnityEngine;
using System.Collections.Generic;

// ----------------------------------------------------------------
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public float MovementSpeedMultiplier = 10.0f;

    public bool Moving { get; private set; }

    private float MovementSpeedAct = 0.0f;

    private Vector2 Destination;

    public PositionOnGrid Pos;

    private Dictionary<ESin, int> Score;

    // ----------------------------------------------------------------
    private void Start()
    {
        Moving = false;
        Score = new Dictionary<ESin, int>();
    }

    // ----------------------------------------------------------------
    public void Init(PositionOnGrid pos, List<ESin> unlockedSins)
    {
        Pos = pos;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Destination = rb.position;

        Moving = false;

        foreach (ESin sin in unlockedSins)
        {
            if (Score.ContainsKey(sin))
                continue;

            Score.Add(sin, 0);
        }

        // TODO Matus : remove
        //Debug.Log("Unlocked score categories: " + Score.Count);
    }

    // ----------------------------------------------------------------
    public void MoveTo(Vector2 destination)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        float distance = (rb.position - destination).magnitude;

        MovementSpeedAct = distance;

        Destination = destination;
        Moving = true;
    }

    // ----------------------------------------------------------------
    private void FixedUpdate()
    {
        if (!Moving)
            return;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (Vector2.Distance(rb.position, Destination) > 0.1f)
        {
            rb.MovePosition(Vector2.MoveTowards(rb.position, Destination, MovementSpeedAct * MovementSpeedMultiplier * Time.fixedDeltaTime));
        }
        else
        {
            Moving = false;
        }
    }
}
