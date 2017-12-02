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
        // TODO Matus : general interaction, not activate layer
        GameObject layObjs = GameObject.Find("LayeredObjects");
        if (layObjs == null)
            return;

        SceneController scc = layObjs.GetComponent<SceneController>();
        if (scc == null)
            return;

        scc.ActivateLayer(ELayer.Blue);
    }
}
