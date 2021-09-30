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

    public float SpeedValue = 2f;

    public float ClusterAreaRange = 2f;

    private void Start()
    {
        CalculateLastValues();
        IndividualMovementManager.Instance.PivotObject = MovementPivotObject;
    }

    public void Move()
    {
        //transform.position = Vector3.Lerp(transform.position, transform.position + LastDirection, Time.fixedDeltaTime * (SpeedValue + GenericDebugManager.Instance.FloatDictionary["ClusterSpeedFactor"]));
        transform.position = transform.position + transform.forward * 0.01f * (SpeedValue + (UpgradeManager.Instance.SpeedUpgrade / 2) + GenericDebugManager.Instance.FloatDictionary["ClusterSpeedFactor"]);
    }

    public void MovementPivot()
    {
        MovementPivotHolder.transform.forward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);

        if(InputManager.Instance.Joystick.Direction != Vector2.zero)
            MovementPivotObject.transform.localPosition += new Vector3(InputManager.Instance.Joystick.Direction.x * 0.1f,0, InputManager.Instance.Joystick.Direction.y * 0.1f);
        MovementPivotObject.transform.localPosition = new Vector3(Mathf.Clamp(MovementPivotObject.transform.localPosition.x, -3, 3), 0, Mathf.Clamp(MovementPivotObject.transform.localPosition.z, -3, 3));
    }

    private void CalculateLastValues()
    {
        LastPivotObjectPosition = MovementPivotObject.transform.position;
        LastDirection = (LastPivotObjectPosition - transform.position).normalized;
    }

    private void FixedUpdate()
    {
        if (!LevelManager.Instance.IsLevelStarted) return;

        MovementPivot();
        if (MovementPivotObject.transform.localPosition != Vector3.zero)
        {
            CalculateLastValues();
        }
        foreach (GameObject individual in IndividualMovementManager.Instance.Individuals)
        {
            if(Vector3.Distance(individual.transform.position,transform.position) > 3f)
            {
                goto Leave;
            }
        }
        Move();
    Leave: return;
    }
}
