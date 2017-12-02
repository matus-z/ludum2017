using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------
public class SceneController : MonoBehaviour
{
    private List<Layers.ELayer> UnlockedLayers;

    // ----------------------------------------------------------------
	private void Start ()
    {
        UnlockedLayers = new List<Layers.ELayer>();
        UnlockedLayers.Add(Layers.ELayer.Base);
	}

    // ----------------------------------------------------------------
    private void Update()
    {
        ActivateLayer(Layers.ELayer.Red);
    }

    // ----------------------------------------------------------------
    public void ActivateLayer(Layers.ELayer layer)
    {
        UnlockedLayers.Add(layer);
        foreach (Transform child in transform)
            child.gameObject.GetComponent<Layers>().ActivateLayer(layer);
    }
}
