using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualColliderControl : MonoBehaviour
{
    public List<Collider> Colliders = new List<Collider>();
    void Start()
    {
        StartCoroutine(enumerator());
    }

    IEnumerator enumerator()
    {
        yield return new WaitForSeconds(0.4f);
        foreach (Collider collider in Colliders)
        {
            collider.enabled = true;
        }
    }
}
