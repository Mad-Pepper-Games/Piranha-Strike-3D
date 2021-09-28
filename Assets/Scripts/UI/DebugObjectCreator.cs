using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugObjectCreator : MonoBehaviour
{
    public GameObject DebugObject;
    public DebugPanelObjectDictionary DebugPanelObjectDictionary = new DebugPanelObjectDictionary();

    private void Start()
    {
        foreach (KeyValuePair<string, DebugPanelObjectType> PanelObject in DebugPanelObjectDictionary)
        {
            DebugPanelObjectController panelObjectReference = Instantiate(DebugObject, transform).GetComponent<DebugPanelObjectController>();
            panelObjectReference.DebugPanelObjectType = PanelObject.Value;
            panelObjectReference.AttachedValue = PanelObject.Key;
        }
    }
}
[System.Serializable]
public class DebugPanelObjectDictionary : UnitySerializedDictionary<string, DebugPanelObjectType> { }