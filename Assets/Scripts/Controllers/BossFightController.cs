using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossFightController: MonoBehaviour
{
    public float Health = 20;
    private float startHealth;
    private float maxHealth;
    public float Timer;
    public int CoinReward = 10;
    private Vector3 startScale;
    public GameObject BossSkin;

    public UnityEvent OnDamageTaken = new UnityEvent();
    public UnityEvent OnDeath = new UnityEvent();

    public bool IsAnimationStarted;
    private void Start()
    {
        startHealth = Health;
        Health = startHealth * (1 + GenericDebugManager.Instance.FloatDictionary["ObstacleHealth"]);
        maxHealth = Health;
        startScale = BossSkin.transform.localScale;
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
            Health -= 0.002f;
            Health = Mathf.Clamp(Health, 0, Health);
            OnDamageTaken.Invoke();
        }
    }
    private void FixedUpdate()
    {
        UtilityManager.Instance.MaxBossHealth = maxHealth;
        UtilityManager.Instance.BossHealth = Health;
        if (Health < maxHealth)
        {
            Timer += 0.003f;

            BossSkin.transform.localScale = Vector3.Lerp(Vector3.zero , startScale, Health / maxHealth);

            if(Timer >= 1 - Mathf.Clamp01(GenericDebugManager.Instance.FloatDictionary["ObstacleBossAttackRate"]))
            {
                if(IndividualMovementManager.Instance.Individuals.Count != 0)
                {
                    Destroy(IndividualMovementManager.Instance.Individuals[0]);
                    Timer = 0;
                }
               
            }

        }

        if (Health <= 0)
        {
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin", 0) + CoinReward  + (int)GenericDebugManager.Instance.FloatDictionary["ObstacleGoldMultiplier"]);
            OnDeath.Invoke();
            Destroy(gameObject); // Ölme Animasyonu vs.
        }
    }
}
