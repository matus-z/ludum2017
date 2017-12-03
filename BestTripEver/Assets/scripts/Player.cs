using UnityEngine;

// ----------------------------------------------------------------
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public float MovementSpeed = 10.0f;

    public bool Moving { get; private set; }

    private Vector2 Destination;

    public PositionOnGrid Pos;

    // ----------------------------------------------------------------
    private void Start()
    {
        Moving = false;
    }

    // ----------------------------------------------------------------
    public void Init(PositionOnGrid pos, float offsetX, float offsetY, float tileSize)
    {
        Pos = pos;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.position = new Vector3(offsetX + Pos.X * tileSize, offsetY + Pos.Y * tileSize, 0.0f);
        Destination = rb.position;

        Moving = false;
    }

    // ----------------------------------------------------------------
    public void MoveTo(Vector2 destination)
    {
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
            rb.MovePosition(Vector2.MoveTowards(rb.position, Destination, MovementSpeed * Time.fixedDeltaTime));
        }
        else
        {
            Moving = false;
        }
    }
}
