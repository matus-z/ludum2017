using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------
public class Layers : MonoBehaviour
{
    public GameObject Base;
    public GameObject Red;
    public GameObject Green;
    public GameObject Blue;
    public GameObject Yellow;

    private ELayer ActLayer;
    private Dictionary<ELayer, GameObject> LayerDict;

    // ----------------------------------------------------------------
    private void Start ()
    {
        Init();
        DeactivateAll();
        ActivateLayer(ELayer.Base);
    }

    // ----------------------------------------------------------------
    private void Init()
    {
        LayerDict = new Dictionary<ELayer, GameObject>();

        LayerDict.Add(ELayer.Base, Base);
        LayerDict.Add(ELayer.Red, Red);
        LayerDict.Add(ELayer.Green, Green);
        LayerDict.Add(ELayer.Blue, Blue);
        LayerDict.Add(ELayer.Yellow, Yellow);
    }

    // ----------------------------------------------------------------
    // Deactivate all layers
    private void DeactivateAll()
    {
        foreach (KeyValuePair<ELayer, GameObject> LayObj in LayerDict)
            if(LayObj.Value != null)
                LayObj.Value.SetActive(false);
    }

    // ----------------------------------------------------------------
    public void ActivateLayer(ELayer layer)
    {
        ActLayer = layer;

        GameObject lo = null;
        bool isObject = LayerDict.TryGetValue(layer, out lo);
        if (isObject == false || lo == null)
            return;

        // Dactivate all except
        foreach (KeyValuePair<ELayer, GameObject> LayObj in LayerDict)
            if(LayObj.Value != null)
                LayObj.Value.SetActive(layer == LayObj.Key);
    }
}
