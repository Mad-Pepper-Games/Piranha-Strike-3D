using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CinemachineDebugController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;

    private CinemachineTransposer cameraTransposer;
    private CinemachineComposer cameraComposer;

    private Vector3 CurrentFollowOffset;
    private Vector3 CurrentAimOffset;

    private void Start()
    {
        cameraTransposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        cameraComposer = virtualCamera.GetCinemachineComponent<CinemachineComposer>();

        CurrentFollowOffset = cameraTransposer.m_FollowOffset;
        CurrentAimOffset = cameraComposer.m_TrackedObjectOffset;
    }
    private void OnEnable()
    {
        GenericDebugManager.Instance.OnValueChanged.AddListener(SetOffSets);
    }

    private void OnDisable()
    {
        GenericDebugManager.Instance.OnValueChanged.RemoveListener(SetOffSets);
    }

    public void SetOffSets()
    {
        cameraTransposer.m_FollowOffset = CurrentFollowOffset + GenericDebugManager.Instance.Vector3Dictionary["CameraPositionOffset"];
        cameraComposer.m_TrackedObjectOffset = CurrentAimOffset + GenericDebugManager.Instance.Vector3Dictionary["CameraLookAtOffset"];
    }
}
