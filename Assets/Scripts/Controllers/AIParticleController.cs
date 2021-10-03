using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIParticleController : MonoBehaviour
{
    public GameObject BloodParticle;

    private EatableController eatableController;
    private EatableController EatableController { get { return (eatableController == null) ? eatableController = GetComponent<EatableController>() : eatableController; } }

    private GameObject BleedParticle;

    private ParticleSystem.EmissionModule emissionModule;
    private float maxHealth;
    private void OnEnable()
    {
        maxHealth = EatableController.Health;
        EatableController.OnDamageTaken.AddListener(Bleed);
        EatableController.OnDeath.AddListener(Death);
    }
    private void OnDisable()
    {
        EatableController.OnDamageTaken.RemoveListener(Bleed);
        EatableController.OnDeath.RemoveListener(Death);
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
            emissionModule.rateOverTime = (maxHealth - EatableController.Health/4)*5;
        }
    }
    private void Death()
    {
            emissionModule.rateOverTime = 0;
    }
}
