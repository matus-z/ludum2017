using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------
public class PlayerController : MonoBehaviour
{
	public float MaxSpeed = 10.0f;
    public float InteractRadius = 3.0f;
    public float InteractAngle = 45.0f;

    private Rigidbody2D rb;
    private Vector2 Up;
    private Vector2 LastPos;

    private GameObject PickedObj;

    // ----------------------------------------------------------------
    private void Start ()
    {
		rb = GetComponent<Rigidbody2D>();
        Up = new Vector3(1.0f, 0, 0);

        PickedObj = null;
        LastPos = transform.position;
    }

    // ----------------------------------------------------------------
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
                InteractWithObjects();
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

    // ----------------------------------------------------------------
    public void PickObject(GameObject go)
    {
        if (go)
            PickedObj = go;
    }

    // ----------------------------------------------------------------
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, Up.normalized.Rotated(45.0f));
        Gizmos.DrawRay(transform.position, Up.normalized.Rotated(-45.0f));

        if (PickedObj)
            Gizmos.DrawLine(transform.position, PickedObj.gameObject.transform.position);
    }

    // ----------------------------------------------------------------
    private void InteractWithObjects()
    {
        if (PickedObj)
            return;

        GameObject objFound = FindNearestInteractable();
        if (objFound == null)
            return;

        IInteractable interactableComponent = objFound.GetComponent(typeof(IInteractable)) as IInteractable;
        if (interactableComponent == null)
            return;

        interactableComponent.InteractWithPlayer(this);
    }
    
    // ----------------------------------------------------------------
    private void DropObject()
    {
        PickedObj = null;
    }

    // ----------------------------------------------------------------
    private GameObject FindNearestInteractable()
    {
        GameObject res = null;

        foreach (Collider2D col in Physics2D.OverlapCircleAll(transform.position, InteractRadius))
        {
            if (col.attachedRigidbody == null)
                continue;

            // Only interact with an interactable
            IInteractable interactableComponent = col.gameObject.GetComponent(typeof(IInteractable)) as IInteractable;
            if (interactableComponent == null)
                continue;

            // Do not interact with yourself
            if (col.gameObject == gameObject)
                continue;

            Vector2 vecToObj = col.transform.position - transform.position;

            // Only interact withing angle range
            float angle = Vector2.Angle(col.attachedRigidbody.GetRelativeVector(vecToObj), Up);
            if (angle > InteractAngle)
                continue;

            // If no object fount yet take this one
            if (res == null)
            {
                res = col.gameObject;
                continue;
            }

            // Else take one if closer
            Vector2 vecToPicked = transform.position - res.transform.position;
            float distance = vecToObj.magnitude;
            float distanceToPicked = vecToPicked.magnitude;

            if (distance < distanceToPicked)
                res = col.gameObject;
        }

        return res;
    }
}
