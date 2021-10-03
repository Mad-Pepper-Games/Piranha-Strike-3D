using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthAndUIController : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<IndividualMovementController>() != null)
        {
            UtilityManager.Instance.BossEvent.Invoke();

           
        }


    }
}
