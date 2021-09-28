using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class GenericDebugManager : Singleton<GenericDebugManager>
{
    public FloatDictionary FloatDictionary = new FloatDictionary();
    public Color32Dictionary Color32Dictionary = new Color32Dictionary();
    public Vector3Dictionary Vector3Dictionary = new Vector3Dictionary();

    public UnityEvent OnValueChanged = new UnityEvent();

    public void ReceiveData(string Key,float input)
    {
        FloatDictionary[Key] = input;
        Debug.Log(input);
        OnValueChanged.Invoke();
    }
    public void ReceiveData(string Key,Vector3 input)
    {
        Vector3Dictionary[Key] = input;
        Debug.Log(input);
        OnValueChanged.Invoke();
    }
    public void ReceiveData(string Key,Color32 input)
    {
        Color32Dictionary[Key] = input;
        Debug.Log(input);
        OnValueChanged.Invoke();
    }
}

[System.Serializable]
public class FloatDictionary : UnitySerializedDictionary<string, float> { }
[System.Serializable]
public class Color32Dictionary : UnitySerializedDictionary<string, Color32> { }
[System.Serializable]
public class Vector3Dictionary : UnitySerializedDictionary<string, Vector3> { }
public abstract class UnitySerializedDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField, HideInInspector]
    private List<TKey> keyData = new List<TKey>();

    [SerializeField, HideInInspector]
    private List<TValue> valueData = new List<TValue>();

    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
        this.Clear();
        for (int i = 0; i < this.keyData.Count && i < this.valueData.Count; i++)
        {
            this[this.keyData[i]] = this.valueData[i];
        }
    }

    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {
        this.keyData.Clear();
        this.valueData.Clear();

        foreach (var item in this)
        {
            this.keyData.Add(item.Key);
            this.valueData.Add(item.Value);
        }
    }
}