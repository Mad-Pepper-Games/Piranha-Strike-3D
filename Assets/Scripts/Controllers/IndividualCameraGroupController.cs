using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualCameraGroupController : MonoBehaviour
{
    [HideInInspector]
    public Cinemachine.CinemachineTargetGroup CinemachineTargetGroup;

    public float Weigth;
    public float Radius;

    private void OnEnable()
    {
        CinemachineTargetGroup = FindObjectOfType<Cinemachine.CinemachineTargetGroup>();
        CinemachineTargetGroup.AddMember(transform, Weigth, Radius);
    }

    private void OnDisable()
    {
        CinemachineTargetGroup.RemoveMember(transform);
    }
}
