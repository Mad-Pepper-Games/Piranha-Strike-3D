using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualMovementManager : Singleton<IndividualMovementManager>
{
    public GameObject PivotObject;
    public List<GameObject> Individuals = new List<GameObject>();

    private void OnEnable()
    {
        GameManager.Instance.OnGameFinishes.AddListener(Clear);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameFinishes.RemoveListener(Clear);
    }

    public void Clear(bool state)
    {
        PivotObject = null;
        Individuals.Clear();
    }
}
