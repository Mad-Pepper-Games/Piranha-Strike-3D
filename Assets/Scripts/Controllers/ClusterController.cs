using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class ClusterController : MonoBehaviour
{
    public GameObject MovementPivotHolder;
    public GameObject MovementPivotObject;

    [ShowInInspector]
    private Vector3 LastPivotObjectPosition;
    private Vector3 LastDirection;

    private float SpeedValue = 4f;

    public float ClusterAreaRange = 2f;

    private void Start()
    {
        CalculateLastValues();
        IndividualMovementManager.Instance.PivotObject = gameObject;
    }

    public void Move()
    {
        transform.position = Vector3.Lerp(transform.position, transform.position + LastDirection, Time.fixedDeltaTime * (SpeedValue + GenericDebugManager.Instance.FloatDictionary["ClusterSpeedFactor"]));
    }

    public void MovementPivot()
    {
        MovementPivotHolder.transform.forward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
        MovementPivotObject.transform.localPosition = new Vector3(InputManager.Instance.Joystick.Direction.x,0, InputManager.Instance.Joystick.Direction.y);
    }

    private void CalculateLastValues()
    {
        LastPivotObjectPosition = MovementPivotObject.transform.position;
        LastDirection = (LastPivotObjectPosition - transform.position).normalized;
    }

    private void FixedUpdate()
    {
        MovementPivot();
        if (MovementPivotObject.transform.localPosition != Vector3.zero)
        {
            CalculateLastValues();
        }
        Move();
    }
}
