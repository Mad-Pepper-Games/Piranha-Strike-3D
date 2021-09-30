using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourAnimationController : MonoBehaviour
{

    public BehaviourController BehaviourController;
    public Animator Animator;

    private void Update()
    {
        if (BehaviourController.ListIndex >= BehaviourController.AIBehaviour.Count) return;

        if(BehaviourController.AIBehaviour[BehaviourController.ListIndex] == Behaviour.Idle)
        {
            Animator.SetFloat("Speed",0);
            Animator.SetBool("IsFalling",false);
            Animator.SetBool("IsSwimming",false);
        }else if (BehaviourController.AIBehaviour[BehaviourController.ListIndex] == Behaviour.Walking)
        {
            Animator.SetFloat("Speed", 8 / BehaviourController.ActionDuration[BehaviourController.ListIndex]);
            Animator.SetBool("IsFalling", false);
            Animator.SetBool("IsSwimming", false);
        }else if (BehaviourController.AIBehaviour[BehaviourController.ListIndex] == Behaviour.Falling)
        {
            Animator.SetFloat("Speed", 0);
            Animator.SetBool("IsFalling", true);
            Animator.SetBool("IsSwimming", false);
        }
        else if (BehaviourController.AIBehaviour[BehaviourController.ListIndex] == Behaviour.Swimming)
        {
            Animator.SetFloat("Speed", 0);
            Animator.SetBool("IsFalling", false);
            Animator.SetBool("IsSwimming", true);
        }
    }
}
