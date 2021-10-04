using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CatchFishAnimation : MonoBehaviour
{
    public GameObject CatchFish;

    private bool IsCatched;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IndividualMovementController>() && !IsCatched)
        {
            CatchFish.transform.DOLocalMoveY(2f, 2f).SetEase(Ease.OutCubic);
            IsCatched = true;
        }
    }

    private void OnEnable()
    {
        GenericDebugManager.Instance.OnValueChanged.AddListener(ChangeSize);
    }

    private void OnDisable()
    {
        GenericDebugManager.Instance.OnValueChanged.RemoveListener(ChangeSize);
    }

    private void ChangeSize()
    {
        transform.localScale = Vector3.one * 3.5f + Vector3.one * GenericDebugManager.Instance.FloatDictionary["ObstacleDetectionRange"];
    }
}
