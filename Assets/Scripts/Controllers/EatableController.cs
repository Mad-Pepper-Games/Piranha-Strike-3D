using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
public class EatableController : MonoBehaviour
{
    public float Health = 20;
    private float startHealth = 20;
    public int CoinReward = 10;

    public UnityEvent OnDamageTaken = new UnityEvent();
    public UnityEvent OnDeath = new UnityEvent();
    public GameObject Skeleton;
    public bool IsAnimationStarted;
    private void Start()
    {
        Health = (1 + UpgradeManager.Instance.FishStarterCount) * 2;
        startHealth = Health;
    }
    private void OnEnable()
    {
        GenericDebugManager.Instance.OnValueChanged.AddListener(SetHealth);
    }

    private void OnDisable()
    {
        GenericDebugManager.Instance.OnValueChanged.RemoveListener(SetHealth);
    }

    private void SetHealth()
    {
        Health = startHealth * (1 + GenericDebugManager.Instance.FloatDictionary["ObstacleHealth"]);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<IndividualMovementController>())
        {
            Health -= 0.004f;
            Health = Mathf.Clamp(Health, 0, Health);
            OnDamageTaken.Invoke();
        }
    }
    private void FixedUpdate()
    {
        if(Health <= 0)
        {
            PlayerPrefs.SetInt("Coin" , PlayerPrefs.GetInt("Coin" , 0) + CoinReward + (int)GenericDebugManager.Instance.FloatDictionary["ObstacleGoldMultiplier"]);
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
