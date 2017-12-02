using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float MaxSpeed = 10.0f;
    public float PickRadius = 3.0f;
    public float PickAngle = 45.0f;

    private Rigidbody2D rb;
    private Vector2 Up;
    private Vector2 LastPos;

    private GameObject PickedObj;

    private void Start ()
    {
		rb = GetComponent<Rigidbody2D>();
        Up = new Vector3(1.0f, 0, 0);

        PickedObj = null;
        LastPos = transform.position;
    }

    private void Update ()
    {
        Vector2 movementDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        rb.AddForce(movementDir * MaxSpeed, ForceMode2D.Impulse);
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, MaxSpeed);

        if (movementDir.magnitude > 0)
            Up = movementDir.normalized;

        if (Input.GetKeyDown("space"))
        {
            if (PickedObj)
                DropObject();
            else
                PickObject();
        }

        Vector2 deltaPos = transform.position;
        deltaPos -= LastPos;

        if (PickedObj)
        {
            Vector2 pickedPos2 = PickedObj.transform.position;
            PickedObj.transform.position = pickedPos2 + deltaPos;
        }

        LastPos = transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, Up.normalized.Rotated(45.0f));
        Gizmos.DrawRay(transform.position, Up.normalized.Rotated(-45.0f));

        if (PickedObj)
            Gizmos.DrawLine(transform.position, PickedObj.gameObject.transform.position);
    }

    private void PickObject()
    {
        if (PickedObj)
            return;

        foreach (Collider2D col in Physics2D.OverlapCircleAll(transform.position, PickRadius))
        {
            if (col.attachedRigidbody == null)
                continue;

            // Only pick a pickable
            if (col.gameObject.GetComponent<Pickable>() == null)
                continue;

            // Do not pick yourself
            if (col.attachedRigidbody.gameObject == gameObject)
                continue;

            Vector2 vecToObj = col.transform.position - transform.position;

            // Only pick withing angle range
            float angle = Vector2.Angle(col.attachedRigidbody.GetRelativeVector(vecToObj), Up);
            if (angle > PickAngle)
                continue;

            if (PickedObj == null)
            {
                PickedObj = col.gameObject;
                continue;
            }

            // Pick the nearest one
            Vector2 vecToPicked = transform.position - PickedObj.transform.position;
            float distance = vecToObj.magnitude;
            float distanceToPicked = vecToPicked.magnitude;

            if (distance < distanceToPicked)
                PickedObj = col.gameObject;
        }
    }

    private void DropObject()
    {
        PickedObj = null;
    }
}
