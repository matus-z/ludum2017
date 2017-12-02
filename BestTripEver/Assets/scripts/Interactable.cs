using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Interactable : MonoBehaviour, IInteractable
{
    public void InteractWithPlayer(PlayerController player)
    {
        // TODO Matus : general interaction
    }
}
