using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceSlider : MonoBehaviour
{
    
    public Slider slider;

    float dist;
    float max;

    private void Start()
    {
       
    }

    private void Update()
    {
        if (!LevelManager.Instance.IsLevelStarted) return;
        dist = Vector3.Distance(IndividualMovementManager.Instance.Finish.transform.position, IndividualMovementManager.Instance.PivotObject.transform.position);

        slider.value = max - dist;
    }

    public void MaxDistance()
    {
        max = Vector3.Distance(IndividualMovementManager.Instance.Finish.transform.position, IndividualMovementManager.Instance.PivotObject.transform.position);
        slider.maxValue = max;
    }

    private void OnEnable()
    {
        LevelManager.Instance.OnLevelStarted.AddListener(MaxDistance);
    }

    private void OnDisable()
    {
        LevelManager.Instance.OnLevelStarted.RemoveListener(MaxDistance);
    }
}
