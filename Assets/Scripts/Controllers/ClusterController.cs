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

    public float SpeedValue = 6f;

    public float ClusterAreaRange = 2.5f;

    public List<Vector3> IndividualPositions = new List<Vector3>();

    private bool IsStarted;
    private bool IsFinished;
    private void Start()
    {
        CalculateLastValues();
        IsStarted = true;
        IndividualMovementManager.Instance.PivotObject = MovementPivotObject;
    }
    private void OnEnable()
    {
        IndividualMovementManager.Instance.OnIndividualAdded.AddListener(CreateIndividualPositions);
        IndividualMovementManager.Instance.OnIndividualRemoved.AddListener(CreateIndividualPositions);
        GenericDebugManager.Instance.OnValueChanged.AddListener(CreateIndividualPositions);
        GameManager.Instance.OnGameFinishes.AddListener(StopWhenFinish);
    }
    private void OnDisable()
    {
        IndividualMovementManager.Instance.OnIndividualAdded.RemoveListener(CreateIndividualPositions);
        IndividualMovementManager.Instance.OnIndividualRemoved.RemoveListener(CreateIndividualPositions);
        GenericDebugManager.Instance.OnValueChanged.RemoveListener(CreateIndividualPositions);
        GameManager.Instance.OnGameFinishes.RemoveListener(StopWhenFinish);
    }

    private void StopWhenFinish(bool state)
    {
        IsFinished = true;
    }
    private void CreateIndividualPositions()
    {
        IndividualPositions.Clear();
        foreach (GameObject individual in IndividualMovementManager.Instance.Individuals)
        {
            Vector3 RandomPositionInsideAreaRange = Random.insideUnitSphere * Random.Range(Mathf.Clamp(ClusterAreaRange -GenericDebugManager.Instance.FloatDictionary["ClusterDensityFactor"],0.1f, ClusterAreaRange), ClusterAreaRange);
            RandomPositionInsideAreaRange = new Vector3(RandomPositionInsideAreaRange.x,0, RandomPositionInsideAreaRange.z);
            IndividualPositions.Add(RandomPositionInsideAreaRange);
            individual.GetComponent<IndividualMovementController>().ClusterIndividualPosition = RandomPositionInsideAreaRange;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (IsStarted)
        {
            for (int i = 0; i < IndividualPositions.Count; i++)
            {
                Gizmos.DrawSphere(IndividualPositions[i] + MovementPivotObject.transform.position, 0.2f);
            }
        }
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
            MovementPivotObject.transform.localPosition += new Vector3(InputManager.Instance.Joystick.Direction.x * 0.4f,0, InputManager.Instance.Joystick.Direction.y * 0.4f);
        MovementPivotObject.transform.localPosition = new Vector3(Mathf.Clamp(MovementPivotObject.transform.localPosition.x, -8, 4), 0, Mathf.Clamp(MovementPivotObject.transform.localPosition.z, -3, 3));
    }

    private void CalculateLastValues()
    {
        LastPivotObjectPosition = MovementPivotObject.transform.position;
        LastDirection = (LastPivotObjectPosition - transform.position).normalized;
    }

    private void FixedUpdate()
    {
        if (!LevelManager.Instance.IsLevelStarted) return;
        if (IsFinished) return;
        MovementPivot();
        if (MovementPivotObject.transform.localPosition != Vector3.zero)
        {
            CalculateLastValues();
        }
        foreach (GameObject individual in IndividualMovementManager.Instance.Individuals)
        {
            if(Vector3.Distance(individual.transform.position, MovementPivotObject.transform.position) > 5f + IndividualMovementManager.Instance.Individuals.Count/5)
            {
                goto Leave;
            }
        }
        Move();
    Leave: return;
    }
}
