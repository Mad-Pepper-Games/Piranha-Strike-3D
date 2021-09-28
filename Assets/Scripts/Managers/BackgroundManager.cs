using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class BackgroundManager : Singleton<BackgroundManager>
{
    public Material BackgroundMaterial;

    private void OnEnable()
    {
        SceneController.Instance.OnSceneLoaded.AddListener(SetColor);
    }
    private void OnDisable()
    {
        SceneController.Instance.OnSceneLoaded.RemoveListener(SetColor);
    }
    public void SetColor()
    {
        BackgroundMaterial.SetColor("_MiddleColor", LevelManager.Instance.CurrentLevel.BackgroundColor);
        RenderSettings.skybox = BackgroundMaterial;
    }
}
