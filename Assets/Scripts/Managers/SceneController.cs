using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class SceneController : Singleton<SceneController>
{
    [HideInInspector]
    public UnityEvent OnSceneStartedLoading = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnSceneStartedUnloading = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnSceneLoaded = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnSceneUnloaded = new UnityEvent();

    public void LoadLevel(Level level)
    {
        StartCoroutine(LoadLevelCo(level));
    }
    public IEnumerator LoadLevelCo(Level level)
    {
        if(level.LevelID.Contains("Level"))
            OnSceneStartedLoading.Invoke();

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            var scene = SceneManager.GetSceneAt(i);
            if (scene.name.Contains("Level"))
                yield return UnloadLevelCo(scene);
        }

        yield return SceneManager.LoadSceneAsync(level.LevelID, LoadSceneMode.Additive);

        if (level.LevelID.Contains("Level"))
            OnSceneLoaded.Invoke();
    }
    public void UnloadLevel(Level level)
    {
        StartCoroutine(UnloadLevelCo(level));
    }
    public IEnumerator UnloadLevelCo(Level level)
    {
        if (level.LevelID.Contains("Level"))
            OnSceneStartedUnloading.Invoke();

        yield return SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(level.LevelID).buildIndex);

        if (level.LevelID.Contains("Level"))
            OnSceneUnloaded.Invoke();
    }
    public IEnumerator UnloadLevelCo(Scene scene)
    {
        if (scene.name.Contains("Level"))
            OnSceneStartedUnloading.Invoke();

        yield return SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(scene.name).buildIndex);

            OnSceneUnloaded.Invoke();
    }

    public void LoadUtilityLevel(int index)
    {
        StartCoroutine(LoadUtilityLevelCo(index));
    }
    public IEnumerator LoadUtilityLevelCo(int index)
    {
        yield return SceneManager.LoadSceneAsync(index,LoadSceneMode.Additive);
    }
    private void Start()
    {
        LoadUtilityLevel(1);
    }
}
