using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SkeletonTween : MonoBehaviour
{
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
        transform.rotation = Quaternion.Euler(90, 0, 0);
        transform.DOMoveY(startPos.y + 6.5f, 1f).SetEase(Ease.OutBack);
    }
}
