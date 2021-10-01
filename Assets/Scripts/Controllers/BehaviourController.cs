using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourController : MonoBehaviour
{
    public List <Behaviour> AIBehaviour = new List<Behaviour>();
    public int ListIndex;
    public List<float> ActionDuration = new List<float>();

    private void OnEnable()
    {
        BehaviourManager.Instance.FinishEvent.AddListener(BehaviourBrain);
        LevelManager.Instance.OnLevelStarted.AddListener(Behave);
    }

    private void OnDisable()
    {
        BehaviourManager.Instance.FinishEvent.RemoveListener(BehaviourBrain);
        LevelManager.Instance.OnLevelStarted.RemoveListener(Behave);
    }

    public void Behave()
    {
        BehaviourManager.Instance.BehaviourTarget = gameObject;
        BehaviourManager.Instance.BehaviourDictionary[AIBehaviour[ListIndex]].Invoke(gameObject , ActionDuration[ListIndex]);
    }

    public void BehaviourBrain(GameObject target, float duration)
    {
        if (target == gameObject)
        {
            ListIndex++;
            if (ListIndex >= AIBehaviour.Count)
            {
                return;
            }
            Behave();
        }
    }
}
