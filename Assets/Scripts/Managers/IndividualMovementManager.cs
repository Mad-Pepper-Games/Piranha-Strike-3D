using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualMovementManager : Singleton<IndividualMovementManager>
{
    public GameObject PivotObject;
    public List<GameObject> Individuals = new List<GameObject>();
    public GameObject Finish;
    private bool IsEventInvoked;

    private void Update()
    {
        if (!LevelManager.Instance.IsLevelStarted) return;
        if (Individuals.Count == 0 && IsEventInvoked == false)
        {
            IsEventInvoked = true;
            GameManager.Instance.OnGameFinishes.Invoke(false);        
        }
    }
    private void OnEnable()
    {
        SceneController.Instance.OnSceneLoaded.AddListener(Clear);
        SceneController.Instance.OnSceneLoaded.AddListener(()=>IsEventInvoked = false);
    }

    private void OnDisable()
    {
        SceneController.Instance.OnSceneLoaded.RemoveListener(Clear);

    }

    public void Clear()
    {
        PivotObject = null;
        Individuals.Clear();
    }
}
