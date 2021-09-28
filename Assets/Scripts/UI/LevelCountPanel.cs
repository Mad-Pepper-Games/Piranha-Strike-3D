using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCountPanel : BasePanel
{
    public TMPro.TextMeshProUGUI FakeLevelText;

    private void OnEnable()
    {
        if (!Managers.Instance) return;
        GameManager.Instance.OnGameFinishes.AddListener(UpdateLevelValue);
        LevelManager.Instance.OnLevelStarted.AddListener(SetLevelValue);
        LevelManager.Instance.OnLevelFinished.AddListener(Deactivate);
    }

    private void OnDisable()
    {
        if (!Managers.Instance) return;
        GameManager.Instance.OnGameFinishes.RemoveListener(UpdateLevelValue);
        LevelManager.Instance.OnLevelStarted.RemoveListener(SetLevelValue);
        LevelManager.Instance.OnLevelFinished.RemoveListener(Deactivate);
    }

    public void UpdateLevelValue(bool gameState)
    {
        if (!gameState) return;
        if (PlayerPrefs.HasKey("FakeLevel"))
        {
            PlayerPrefs.SetInt("FakeLevel", PlayerPrefs.GetInt("FakeLevel") +1);
        }
        else
        {
            PlayerPrefs.SetInt("FakeLevel", 1);
        }
    }

    public void SetLevelValue()
    {
        if (!PlayerPrefs.HasKey("FakeLevel"))
        {
            PlayerPrefs.SetInt("FakeLevel", 1);
        }
        FakeLevelText.SetText("Level " + PlayerPrefs.GetInt("FakeLevel"));
        Activate();
    }
}
