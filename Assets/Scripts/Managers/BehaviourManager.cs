using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class BehaviourManager : Singleton<BehaviourManager>
{
    public Dictionary<Behaviour, BehaviourEvent> BehaviourDictionary = new Dictionary<Behaviour, BehaviourEvent>();
    public GameObject BehaviourTarget;
    public BehaviourEvent IdleBehaviourEvent = new BehaviourEvent();
    public BehaviourEvent WalkingBehaviourEvent = new BehaviourEvent();
    public BehaviourEvent FallingBehaviourEvent = new BehaviourEvent();
    public BehaviourEvent FinishEvent = new BehaviourEvent();

    private void OnEnable()
    {
        BehaviourDictionary.Add(Behaviour.Idle, IdleBehaviourEvent);  
        BehaviourDictionary.Add(Behaviour.Walking, WalkingBehaviourEvent);  
        BehaviourDictionary.Add(Behaviour.Falling, FallingBehaviourEvent);

        IdleBehaviourEvent.AddListener(IdleBehaviour);
        WalkingBehaviourEvent.AddListener(WalkingBehaviour);
        FallingBehaviourEvent.AddListener(FallingBehaviour);
    }

    private void OnDisable()
    {
        IdleBehaviourEvent.RemoveListener(IdleBehaviour);
        WalkingBehaviourEvent.RemoveListener(WalkingBehaviour);
        FallingBehaviourEvent.RemoveListener(FallingBehaviour);
    }

    public void IdleBehaviour(GameObject target , float duration)
    {
        Debug.Log(target.name+"Idle");
        StartCoroutine(IdleEnumerator(()=> FinishEvent.Invoke(target , 0) , duration));
    }

    public void WalkingBehaviour(GameObject target, float duration)
    {
        Debug.Log(target.name + "Walking");
        target.transform.position = target.GetComponent<BehaviourPath>().Positions[0];
        Move(target, duration);
    }

    public void Move(GameObject target, float duration)
    {
        BehaviourPath BehaviourPath = target.GetComponent<BehaviourPath>();
        
        target.transform.DOMove(BehaviourPath.Positions[BehaviourPath.PositionIndex], duration / BehaviourPath.Positions.Count).SetEase(Ease.Linear).OnComplete(() => {
            BehaviourPath.PositionIndex++;
            if(BehaviourPath.PositionIndex < BehaviourPath.Positions.Count)
            {
                Move(target, duration);
            }
            else
            {
                FinishEvent.Invoke(target, 0);
            }
        });
            target.transform.DORotateQuaternion(Quaternion.LookRotation((BehaviourPath.Positions[BehaviourPath.PositionIndex] - target.transform.position)), duration / BehaviourPath.Positions.Count).SetEase(Ease.OutExpo);
    }

    public void FallingBehaviour(GameObject target, float duration)
    {
        Debug.Log(target.name + "Falling");

        target.transform.DOMove(target.GetComponent<BehaviourPath>().FallingPosition, duration).SetEase(Ease.OutCubic);
        target.transform.DORotateQuaternion(Quaternion.LookRotation((target.GetComponent<BehaviourPath>().FallingPosition - target.transform.position)), duration).SetEase(Ease.OutExpo);
        FinishEvent.Invoke(target , 0);

    }

    public IEnumerator IdleEnumerator(UnityAction e, float duration)
    {
        yield return new WaitForSeconds(duration);
        e.Invoke();
    }

}

public class BehaviourEvent : UnityEvent<GameObject , float>
{

}
public enum Behaviour
{
    Idle,
    Walking,
    Falling
}
