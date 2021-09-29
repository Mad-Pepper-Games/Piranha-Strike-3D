using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class IndividualMovementController : MonoBehaviour
{
    private float speedValue = 1.5f;
    private float rotateValue = 0.9f;
    [ShowInInspector]
    private Vector3 centerPoint;

    public List<GameObject> AttackTargets = new List<GameObject>();

    private float distanceToAttackTarget;
    void OnEnable()
    {
        if (!IndividualMovementManager.Instance.Individuals.Contains(gameObject))
            IndividualMovementManager.Instance.Individuals.Add(gameObject);
    }

    public void Move()
    {
        transform.position = Vector3.Lerp(transform.position, transform.position + (transform.forward * (speedValue + GenericDebugManager.Instance.FloatDictionary["IndividualSpeedFactor"])), Time.fixedDeltaTime );
    }

    public void CalculateCenterPoint()
    {
        centerPoint = Vector3.zero;

        foreach (GameObject individual in IndividualMovementManager.Instance.Individuals)
        {
            centerPoint = centerPoint + individual.transform.position;
        }
        centerPoint = centerPoint / (IndividualMovementManager.Instance.Individuals.Count + 1);
    }

    public void RotateToCenter()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation((InitialTarget() - transform.position).normalized), Time.fixedDeltaTime * (rotateValue + GenericDebugManager.Instance.FloatDictionary["IndividualRotationFactor"]));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EatableController>())
        {
            if (!AttackTargets.Contains(other.gameObject))
            {
                AttackTargets.Add(other.gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<EatableController>())
        {
            if (AttackTargets.Contains(other.gameObject))
            {
                AttackTargets.Remove(other.gameObject);
            }
        }
    }

    public void AttackMovement()
    {
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
            }
            else
            {
                continue;
            }
        }
    }

    public void StandartMovement()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 1f))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation((-hit.point + transform.position).normalized), Time.fixedDeltaTime * (rotateValue * 2 + GenericDebugManager.Instance.FloatDictionary["IndividualRotationFactor"]));
        }
        else
        {
            if (Vector3.Distance(transform.position, InitialTarget()) > 2f || transform.position.x > InitialTarget().x + 1 || transform.position.x < InitialTarget().x - 1)
            {
                RotateToCenter();
            }
            else
            {
                foreach (GameObject individual in IndividualMovementManager.Instance.Individuals)
                {
                    if (Vector3.Distance(individual.transform.position, transform.position) < 0.75f)
                    {
                        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation((-individual.transform.position + transform.position).normalized), Time.fixedDeltaTime * (rotateValue + GenericDebugManager.Instance.FloatDictionary["IndividualRotationFactor"]));
                        RotateToCenter();
                    }
                    else
                    {
                        RotateToCenter();
                    }
                }
            }
        }
    }

    private Vector3 InitialTarget()
    {
        Vector3 vector3=Vector3.zero;
        vector3 = Vector3.Lerp(IndividualMovementManager.Instance.PivotObject.transform.position, centerPoint, Mathf.Clamp01(1 - (Vector3.Distance(transform.position, IndividualMovementManager.Instance.PivotObject.transform.position)) * 2 * GenericDebugManager.Instance.FloatDictionary["ClusterScatterFactor"]));
        return vector3;
    }


    private void FixedUpdate()
    {
        if (!LevelManager.Instance.IsLevelStarted) return;
        CalculateCenterPoint();

        if(AttackTargets.Count > 0)
        {
            AttackMovement();
        }
        else
        {
            StandartMovement();
        }
        Move();
    }
}
