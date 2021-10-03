using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightController: MonoBehaviour
{
    public float Health = 20;
    private float maxHealth;
    public float Timer;
    public int CoinReward = 10;
    private Vector3 startScale;
    public GameObject BossSkin; 
    private void Start()
    {
        maxHealth = Health;
        startScale = BossSkin.transform.localScale;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<IndividualMovementController>())
        {
            Health -= 0.005f;
            Health = Mathf.Clamp(Health, 0, Health);
        }
    }
    private void FixedUpdate()
    {
        UtilityManager.Instance.MaxBossHealth = maxHealth;
        UtilityManager.Instance.BossHealth = Health;
        if (Health < maxHealth)
        {
            Timer += 0.0025f;

            BossSkin.transform.localScale = Vector3.Lerp(Vector3.zero , startScale, Health / maxHealth);

            if(Timer >= 1)
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
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin", 0) + CoinReward);
            Destroy(gameObject); // Ölme Animasyonu vs.
        }
    }
}
