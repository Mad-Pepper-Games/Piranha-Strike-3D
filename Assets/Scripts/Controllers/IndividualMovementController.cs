using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class IndividualMovementController : MonoBehaviour
{
    private float SpeedValue = 1.5f;
    private float RotateValue = 1.5f;
    [ShowInInspector]
    private Vector3 CenterPoint;

    void Start()
    {
        if (!IndividualMovementManager.Instance.Individuals.Contains(gameObject))
            IndividualMovementManager.Instance.Individuals.Add(gameObject);
    }

    public void Move()
    {
        transform.position = Vector3.Lerp(transform.position, transform.position + (transform.forward * (SpeedValue + GenericDebugManager.Instance.FloatDictionary["IndividualSpeedFactor"])), Time.fixedDeltaTime );
    }

    public void CalculateCenterPoint()
    {
        CenterPoint = Vector3.zero;

        foreach (GameObject individual in IndividualMovementManager.Instance.Individuals)
        {
            CenterPoint = CenterPoint + individual.transform.position;
        }
        CenterPoint = CenterPoint / (IndividualMovementManager.Instance.Individuals.Count + 1);
    }

    public void RotateToCenter()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation((InitialTarget() - transform.position).normalized), Time.fixedDeltaTime * (RotateValue + GenericDebugManager.Instance.FloatDictionary["IndividualRotationFactor"]));
    }

    public void RotateAwayFromOthers()
    {
        if(transform.position.x > 2 || transform.position.x < -2)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation((new Vector3(0,0,transform.position.z) - transform.position).normalized), Time.fixedDeltaTime * (RotateValue * 2 + GenericDebugManager.Instance.FloatDictionary["IndividualRotationFactor"]));
        }
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 0.4f))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation((-hit.point + transform.position).normalized), Time.fixedDeltaTime * (RotateValue*2 + GenericDebugManager.Instance.FloatDictionary["IndividualRotationFactor"]));
        }
        else
        {
            if (Vector3.Distance(transform.position, IndividualMovementManager.Instance.PivotObject.transform.position) < 0.15f)
            {

                foreach (GameObject individual in IndividualMovementManager.Instance.Individuals)
                {
                    if (Vector3.Distance(individual.transform.position, transform.position) < 0.65f)
                    {
                        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation((-individual.transform.position + transform.position).normalized), Time.fixedDeltaTime * (RotateValue + GenericDebugManager.Instance.FloatDictionary["IndividualRotationFactor"]));
                        RotateToCenter();
                    }
                    else
                    {
                        RotateToCenter();
                    }
                }

            }
            else
            {
                RotateToCenter();
            }
        }
    }

    private Vector3 InitialTarget()
    {
        Vector3 vector3=Vector3.zero;
        vector3 = Vector3.Lerp(IndividualMovementManager.Instance.PivotObject.transform.position, CenterPoint, Mathf.Clamp01(1 - (Vector3.Distance(transform.position, IndividualMovementManager.Instance.PivotObject.transform.position)) * 2 * GenericDebugManager.Instance.FloatDictionary["ClusterScatterFactor"]));
        return vector3;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(InitialTarget(), 0.1f);
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 0.4f);
    }

    private void FixedUpdate()
    {
        if (!LevelManager.Instance.IsLevelStarted) return;

        CalculateCenterPoint();
        RotateAwayFromOthers();
        Move();
    }
}
