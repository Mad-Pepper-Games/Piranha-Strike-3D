using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIParticleController : MonoBehaviour
{
    public GameObject BloodParticle;

    private EatableController eatableController;
    private EatableController EatableController { get { return (eatableController == null) ? eatableController = GetComponent<EatableController>() : eatableController; } }
    
    private BossFightController bossFightController;
    private BossFightController BossFightController { get { return (bossFightController == null) ? bossFightController = GetComponent<BossFightController>() : bossFightController; } }

    private GameObject BleedParticle;

    private ParticleSystem.EmissionModule emissionModule;
    private float maxHealth;
    private void OnEnable()
    {
        
        if (EatableController)
        {
            maxHealth = EatableController.Health;
            EatableController.OnDamageTaken.AddListener(Bleed);
            EatableController.OnDeath.AddListener(Death);
        }
        else if(BossFightController)
        {
            maxHealth = BossFightController.Health;
            BossFightController.OnDamageTaken.AddListener(Bleed);
            BossFightController.OnDeath.AddListener(Death);
        }
    }
    private void OnDisable()
    {
        if (EatableController)
        {
            EatableController.OnDamageTaken.RemoveListener(Bleed);
            EatableController.OnDeath.RemoveListener(Death);
        }
        else if (BossFightController)
        {
            BossFightController.OnDamageTaken.RemoveListener(Bleed);
            BossFightController.OnDeath.RemoveListener(Death);
        }
    }

    private void Bleed()
    {
        if (!BleedParticle)
        {
            BleedParticle = Instantiate(BloodParticle, transform.position + Vector3.up * 0.85f, Quaternion.identity);
            BleedParticle.transform.parent = transform;
            BleedParticle.transform.parent = null;
            emissionModule = BleedParticle.GetComponent<ParticleSystem>().emission;
        }
        else
        {
            if (EatableController)
            {
                emissionModule.rateOverTime = (maxHealth - EatableController.Health / 4) * 5;
            }
            else
            {
                emissionModule.rateOverTime = (maxHealth - BossFightController.Health / 4) * 5;
            }
        }
    }
    private void Death()
    {
            emissionModule.rateOverTime = 0;
    }
}
