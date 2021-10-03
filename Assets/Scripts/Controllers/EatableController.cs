using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
public class EatableController : MonoBehaviour
{
    public float Health = 20;
    public int CoinReward = 10;

    public UnityEvent OnDamageTaken = new UnityEvent();
    public UnityEvent OnDeath = new UnityEvent();
    public GameObject Skeleton;
    public bool IsAnimationStarted;
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<IndividualMovementController>())
        {
            Health -= 0.005f;
            Health = Mathf.Clamp(Health, 0, Health);
            OnDamageTaken.Invoke();
        }
    }
    private void FixedUpdate()
    {
        if(Health <= 0)
        {
            PlayerPrefs.SetInt("Coin" , PlayerPrefs.GetInt("Coin" , 0) + CoinReward);
            OnDeath.Invoke();
            DeathAnimation(); // Ölme Animasyonu vs.
        }
    }
    private void DeathAnimation()
    {
        if (!IsAnimationStarted)
        {
            GetComponent<Collider>().enabled = false;
            IsAnimationStarted = true;
            transform.DOMoveY(-5, 2f).OnComplete(()=> { 
                GameObject skeleton = Instantiate(Skeleton, transform.position, Quaternion.identity);
                skeleton.transform.parent = transform;
                skeleton.transform.parent = null;
                Destroy(gameObject);
            });
        }
    }
}
