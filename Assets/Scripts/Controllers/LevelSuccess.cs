using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSuccess : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IndividualMovementController>() != null )
        {
            GameManager.Instance.OnGameFinishes.Invoke(true);
            GetComponent<Collider>().enabled = false;

        }
    }

   
}
