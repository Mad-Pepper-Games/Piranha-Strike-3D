using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatableController : MonoBehaviour
{
    public float Health = 20;
    public int CoinReward = 10;
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
        if(Health <= 0)
        {
            PlayerPrefs.SetInt("Coin" , PlayerPrefs.GetInt("Coin" , 0) + CoinReward);
            Destroy(gameObject); // Ölme Animasyonu vs.
        }
    }
}
