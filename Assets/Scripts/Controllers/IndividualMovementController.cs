using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class IndividualMovementController : MonoBehaviour
{
    private float speedValue = 5.2f;
    private float rotateValue = 4f;
    [ShowInInspector]
    private Vector3 centerPoint;

    public List<GameObject> AttackTargets = new List<GameObject>();

    private float distanceToAttackTarget = 9999;

    private GameObject Target;

    public Vector3 ClusterIndividualPosition;

    void OnEnable()
    {
        if (!IndividualMovementManager.Instance.Individuals.Contains(gameObject))
        {
            IndividualMovementManager.Instance.Individuals.Add(gameObject);
            IndividualMovementManager.Instance.OnIndividualAdded.Invoke();
        }
    }
    void OnDisable()
    {
        if (IndividualMovementManager.Instance.Individuals.Contains(gameObject))
        {
            IndividualMovementManager.Instance.Individuals.Remove(gameObject);
            IndividualMovementManager.Instance.OnIndividualRemoved.Invoke();
        }
    }

    public void Move()
    {
        transform.position = Vector3.Lerp(transform.position, transform.position + (transform.forward * (speedValue + (UpgradeManager.Instance.SpeedUpgrade/5) + GenericDebugManager.Instance.FloatDictionary["IndividualSpeedFactor"])), Time.fixedDeltaTime );
    }

    public void RotateToClusterTargetPosition()
    {
        transform.rotation = Quaternion.LerpUnclamped(transform.rotation, Quaternion.LookRotation((ClusterIndividualPositionTarget() - transform.position).normalized), Time.fixedDeltaTime * (rotateValue + GenericDebugManager.Instance.FloatDictionary["IndividualRotationFactor"]));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EatableController>() || other.GetComponent<BossFightController>())
        {
            if (!AttackTargets.Contains(other.gameObject))
            {
                AttackTargets.Add(other.gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<EatableController>() || other.GetComponent<BossFightController>())
        {
            if (AttackTargets.Contains(other.gameObject))
            {
                AttackTargets.Remove(other.gameObject);
            }
        }
    }

    public void AttackMovement()
    {
        distanceToAttackTarget = 9999;
        foreach (GameObject target in AttackTargets)
        {
            if(target == null)
            {
                AttackTargets.Remove(target);
                AttackMovement();
                return;
            }
            if(distanceToAttackTarget > Vector3.Distance(target.transform.position, transform.position))
            {
                distanceToAttackTarget = Vector3.Distance(target.transform.position, transform.position);
                Target = target;
            }
            else
            {
                continue;
            }
        }
        if(Target != null)
            centerPoint = Target.transform.position;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation((centerPoint - transform.position).normalized), Time.fixedDeltaTime  * 1.25f * (rotateValue + GenericDebugManager.Instance.FloatDictionary["IndividualRotationFactor"]));
    }

    private Vector3 ClusterIndividualPositionTarget()
    {
        Vector3 localVector3 = Vector3.zero;

        if (IndividualMovementManager.Instance.PivotObject)
            localVector3 = IndividualMovementManager.Instance.PivotObject.transform.position + ClusterIndividualPosition;

        return localVector3;
    }


    private void FixedUpdate()
    {
        if (!LevelManager.Instance.IsLevelStarted) return;
       

        if(AttackTargets.Count > 0)
        {
            AttackMovement();
        }
        else
        {
            ClusterIndividualPositionTarget();
            RotateToClusterTargetPosition();
        }
        Move();
    }
}
