using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : BasePanel
{
    public Slider Slider;
    private float maxHealth;
    private float Health;

    private void Start()
    {
       
        Slider.maxValue = maxHealth;

    }

    private void Update()
    {
        maxHealth = UtilityManager.Instance.MaxBossHealth;
        Slider.maxValue = maxHealth;

        Health = UtilityManager.Instance.BossHealth;
        Slider.value = Health;
        if (Health <= 0)
        {
            Deactivate();
        }

    }

    private void OnEnable()
    {
        UtilityManager.Instance.BossEvent.AddListener(Activate);

    }

    private void OnDisable()
    {
        UtilityManager.Instance.BossEvent.RemoveListener(Deactivate);
        
    }
}
