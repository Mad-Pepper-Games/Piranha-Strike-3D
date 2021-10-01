using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITriggerController : MonoBehaviour
{
    public List<BehaviourController> BehaviourControllers = new List<BehaviourController>() ;
   
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IndividualMovementController>() != null)
        {
            foreach (BehaviourController behaviour in BehaviourControllers)
            {
                behaviour.Behave();
            }
        }
    }
}
