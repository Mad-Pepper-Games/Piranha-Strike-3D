using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
public class LevelManager : Singleton<LevelManager>
{
    [HideInInspector]
    public UnityEvent OnLevelStarted = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnLevelFinished = new UnityEvent();

    public List<Level> Levels = new List<Level>();
    [HideInInspector]
    public Level CurrentLevel;
    [HideInInspector]
    public bool IsLevelStarted;

    public void StartLevel()
    {
        if (IsLevelStarted) return;
        OnLevelStarted.Invoke();
        IsLevelStarted = true;
    }

    public void FinishLevel()
    {
        if (!IsLevelStarted) return;
        IsLevelStarted = false;
        OnLevelFinished.Invoke();
    }

    public void LoadLastLevel()
    {
        if (PlayerPrefs.HasKey("LastLevel"))
        {
            CurrentLevel = Levels[PlayerPrefs.GetInt("LastLevel")];
        }
        else
        {
            PlayerPrefs.SetInt("LastLevel", 0);
            CurrentLevel = Levels[PlayerPrefs.GetInt("LastLevel")];
        }
        SceneController.Instance.LoadLevel(CurrentLevel);
    }

    public void LoadNextLevel()
    {
        Debug.Log(PlayerPrefs.GetInt("FakeLevels"));
        if (PlayerPrefs.GetInt("FakeLevel") > 3 && PlayerPrefs.GetInt("FakeLevel") % 4 == 0)
        {
            AdManager.Instance.ReadyAds();
        }

        PlayerPrefs.SetInt("LastLevel", PlayerPrefs.GetInt("LastLevel") + 1);
        if(PlayerPrefs.GetInt("LastLevel") >= Levels.Count)
        {
            PlayerPrefs.SetInt("LastLevel", 0);
        }
        LoadLastLevel();
    }

    public void ReloadLevel()
    {
        LoadLastLevel();
    }

    private void Start()
    {
        LoadLastLevel();
    }
}
