using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITriggerController : MonoBehaviour
{
    public List<BehaviourController> BehaviourControllers = new List<BehaviourController>() ;
    private bool isTriggered;
    private void OnTriggerEnter(Collider other)
    {
        if (isTriggered) return;
        if (other.GetComponent<IndividualMovementController>() != null)
        {
            isTriggered = true;
            foreach (BehaviourController behaviour in BehaviourControllers)
            {
                behaviour.Behave();
            }
        }
    }
}
