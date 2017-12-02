using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------
public class SceneController : MonoBehaviour
{
    private List<ELayer> UnlockedLayers;

    // ----------------------------------------------------------------
	private void Start ()
    {
        UnlockedLayers = new List<ELayer>();
        UnlockedLayers.Add(ELayer.Base);
	}

    // ----------------------------------------------------------------
    public void ActivateLayer(ELayer layer)
    {
        UnlockedLayers.Add(layer);
        foreach (Transform child in transform)
            child.gameObject.GetComponent<Layers>().ActivateLayer(layer);
    }
}
