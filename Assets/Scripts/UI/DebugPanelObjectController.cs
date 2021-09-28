using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DebugPanelObjectController : MonoBehaviour
{
    public string AttachedValue;
    public GameObject FloatObject;
    public TMP_InputField FloatField;
    public TextMeshProUGUI FloatVariableText;

    public GameObject Vector3Object;
    public TMP_InputField Vector3XField, Vector3YField, Vector3ZField;
    public TextMeshProUGUI Vector3VariableText;

    public GameObject Color32Object;
    public TMP_InputField Color32RField, Color32GField, Color32BField;
    public TextMeshProUGUI Color32VariableText;

    public DebugPanelObjectType DebugPanelObjectType;

    public float DefaultFloat = 1;
    public Vector3 DefaultVector3 = Vector3.one;
    public Color32 DefaultColor = new Color32(1,1,1,1);

    private void Start()
    {
        switch (DebugPanelObjectType)
        {
            case DebugPanelObjectType.Float:
                FloatObject.SetActive(true);
                FloatField.textComponent.SetText(DefaultFloat.ToString());
                FloatVariableText.SetText(AttachedValue);
                break;
            case DebugPanelObjectType.Vector3:
                Vector3Object.SetActive(true);
                Vector3XField.textComponent.SetText(DefaultVector3.x.ToString());
                Vector3YField.textComponent.SetText(DefaultVector3.y.ToString());
                Vector3ZField.textComponent.SetText(DefaultVector3.z.ToString());
                Vector3VariableText.SetText(AttachedValue);

                break;
            case DebugPanelObjectType.Color32:
                Color32Object.SetActive(true);
                Color32RField.textComponent.SetText(DefaultColor.r.ToString());
                Color32GField.textComponent.SetText(DefaultColor.g.ToString());
                Color32BField.textComponent.SetText(DefaultColor.b.ToString());
                Color32VariableText.SetText(AttachedValue);

                break;
            default:
                break;
        }
    }

    private void OnEnable()
    {
        FloatField.onValueChanged.AddListener(delegate { SendData(float.Parse(FloatField.text)); });

        Vector3XField.onValueChanged.AddListener(delegate { SendData(new Vector3(float.Parse(Vector3XField.text), float.Parse(Vector3YField.text), float.Parse(Vector3ZField.text))); });
        Vector3YField.onValueChanged.AddListener(delegate { SendData(new Vector3(float.Parse(Vector3XField.text), float.Parse(Vector3YField.text), float.Parse(Vector3ZField.text))); });
        Vector3ZField.onValueChanged.AddListener(delegate { SendData(new Vector3(float.Parse(Vector3XField.text), float.Parse(Vector3YField.text), float.Parse(Vector3ZField.text))); });

        Color32RField.onValueChanged.AddListener(delegate { SendData(new Color32(byte.Parse(Color32RField.text), byte.Parse(Color32GField.text), byte.Parse(Color32BField.text),1)); });
        Color32GField.onValueChanged.AddListener(delegate { SendData(new Color32(byte.Parse(Color32RField.text), byte.Parse(Color32GField.text), byte.Parse(Color32BField.text), 1)); });
        Color32BField.onValueChanged.AddListener(delegate { SendData(new Color32(byte.Parse(Color32RField.text), byte.Parse(Color32GField.text), byte.Parse(Color32BField.text), 1)); });
    }

    private void SendData(float input)
    {
        GenericDebugManager.Instance.ReceiveData(AttachedValue, input);
    }
    private void SendData(Vector3 input)
    {
        GenericDebugManager.Instance.ReceiveData(AttachedValue, input);
    }
    private void SendData(Color32 input)
    {
        GenericDebugManager.Instance.ReceiveData(AttachedValue, input);
    }
}

public enum DebugPanelObjectType
{
    Float,
    Vector3,
    Color32
}