using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourController : MonoBehaviour
{
    public List <Behaviour> AIBehaviour = new List<Behaviour>();
    private int listIndex;
    public List<float> ActionDuration = new List<float>();

    private void OnEnable()
    {
        BehaviourManager.Instance.FinishEvent.AddListener(BehaviourBrain);
        Behave();
    }

    private void OnDisable()
    {
        BehaviourManager.Instance.FinishEvent.RemoveListener(BehaviourBrain);

    }

    public void Behave()
    {
        BehaviourManager.Instance.BehaviourTarget = gameObject;
        BehaviourManager.Instance.BehaviourDictionary[AIBehaviour[listIndex]].Invoke(gameObject , ActionDuration[listIndex]);
    }

    public void BehaviourBrain(GameObject target, float duration)
    {
        if (target == gameObject)
        {
            listIndex++;
            if (listIndex >= AIBehaviour.Count)
            {
                return;
            }
            Behave();
        }
    }
}
