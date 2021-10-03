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
            CatchFish.transform.DOMoveY(5f, 2f).SetEase(Ease.OutCubic);
            IsCatched = true;
        }
    }
}
