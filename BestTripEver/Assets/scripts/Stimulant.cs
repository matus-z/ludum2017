using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Stimulant : MonoBehaviour, IInteractable
{
    public ELayer Layer = ELayer.Base;

    // ----------------------------------------------------------------
    public void InteractWithPlayer(PlayerController player)
    {
        // Unlock Layer on interaction
        GameObject layObjs = GameObject.Find("LayeredObjects");
        if (layObjs == null)
            return;

        SceneController scc = layObjs.GetComponent<SceneController>();
        if (scc == null)
            return;

        scc.ActivateLayer(Layer);
    }
}
