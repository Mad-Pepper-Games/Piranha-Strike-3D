using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FisinhObjectController : MonoBehaviour
{

    private void Start()
    {
        IndividualMovementManager.Instance.Finish = gameObject;
    }
}
